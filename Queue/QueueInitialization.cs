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
        private readonly IBindContext _bindContext;
        private readonly IGenericEventHandle _eventHandle;
        private readonly IWorkContext _workContext;
        public QueueInitialization(IBindContext bindContext, IGenericEventHandle eventHandle, IWorkContext workContext)
        {
            _bindContext = bindContext;
            _eventHandle = eventHandle;
            _workContext = workContext;
        }
        /// <summary>
        /// 调用此静态方法用以启动消息队列
        /// </summary>
        /// <param name="e"></param>
        public void PeristalticStart(IPeristalticConfiguration configuration)
        {
            //初始化上下文类
            _workContext.ConnectionString = configuration.ConnectionString;
            configuration.Context =_bindContext;

            //配置启动项
            var context = (PeristalticContext)configuration.Configuration();
            //注册事件
            _eventHandle.Register(new QueueAttach(context.Troops, context.LoaderSignal).Add);//入列事件，外部
            _eventHandle.Register(e =>
            {
                IGenericResult result = default;
                context.ResultSignal.TryAdd(e, new WaitHandle[2] { new AutoResetEvent(false), new ManualResetEvent(false) });
                Parallel.ForEach(context.ResultSignal, signal =>
                {
                    if (signal.Key == e)
                    {
                        if (WaitHandle.WaitAny(signal.Value) == 1)
                        {
                            if (context.Result.TryRemove(e, out var v))
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
