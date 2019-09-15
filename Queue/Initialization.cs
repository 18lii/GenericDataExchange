using Queue.Events;
using Queue.Interface;
using Queue.Peristaltic;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    public class Initialization
    {
        private readonly BindContext _bindContext;
        private readonly IPeristalticConfiguration _configuration;
        public Initialization(IBindContext bindContext, IPeristalticConfiguration configuration)
        {
            _bindContext = (BindContext)bindContext;
            _configuration = configuration;
        }
        /// <summary>
        /// 调用此静态方法用以启动消息队列
        /// </summary>
        /// <param name="e"></param>
        public void PeristalticStart()
        {
            //初始化上下文类
            _configuration.Context =_bindContext;
            
            //配置启动项
            var context = (BindContext)_configuration.Configuration();
            
            //注册事件
            GenericEventHandle.Register(new QueueAttach(context.Troops, context.LoaderSignal, context.ResultSignal).Add);//入列事件，外部
            GenericEventHandle.Register(guid =>
            {
                object result = null;
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
            //启动队列线程
            new Thread(new QueueDispatch(context.LoaderSignal, context.Troops, context.Actions, context.ExecuterDefault).Dispatch)
            {
                Name = "GSP-DISPATCH:#default"
            }.Start();
        }
    }
}
