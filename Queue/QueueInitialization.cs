using Core.Events;
using Core.Interface;
using Database.Interface;
using Queue.Interface;
using Queue.Peristaltic;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    public class QueueInitialization
    {
        private readonly PeristalticContext _bindContext;
        public QueueInitialization(IBindContext bindContext, IWorkContext workContext)
        {
            _bindContext = (PeristalticContext)bindContext;
            _bindContext.DbContext = workContext;
        }
        /// <summary>
        /// 调用此静态方法用以启动消息队列
        /// </summary>
        /// <param name="e"></param>
        public void PeristalticStart(IPeristalticConfiguration configuration)
        {
            //初始化上下文类
            _bindContext.DbContext.ConnectionString = configuration.ConnectionString;
            configuration.Context =_bindContext;
            

            //配置启动项
            var context = (PeristalticContext)configuration.Configuration();
            //注册事件
            GenericEventHandle.Register(new QueueAttach(context.Troops, context.LoaderSignal).Add);//入列事件，外部
            GenericEventHandle.Register(guid =>
            {
                IGenericResult result = default;
                context.ResultSignal.TryAdd(guid, new WaitHandle[2] { new AutoResetEvent(false), new ManualResetEvent(false) });
                Parallel.ForEach(context.ResultSignal, signal =>
                {
                    if (signal.Key == guid)
                    {
                        if (WaitHandle.WaitAny(signal.Value) == 1)
                        {
                            if (context.Result.TryRemove(guid, out var v))
                            {
                                result = v;
                            }
                        }
                    }
                });
                return result;
            });
            //启动调度线程
            new Thread(new QueueDispatch(context.LoaderSignal, context.Troops, context.Actions, context.ExecuterDefault).Dispatch)
            {
                Name = "GSP-DISPATCH:#default"
            }.Start();
        }
    }
}
