using Queue.Events;
using Queue.Interface;
using Queue.Peristaltic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Queue
{
    public class Initialization
    {
        private readonly BindContext _bindContext;
        private readonly IPeristalticConfiguration _configuration;
        public Initialization(IPeristalticConfiguration configuration)
        {
            _bindContext = new BindContext();
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
                Debug.WriteLine("返回委托被调用");
                object result = null;
                if(context.ResultSignal.TryRemove(guid, out var signal))
                {
                    while (WaitHandle.WaitAny(signal, 20000) == 0)
                    {
                        Debug.WriteLine("进入循环体调用");
                        //context.ResultSignal.TryRemove(guid, out var temp);
                        if (context.Result.TryRemove(guid, out var v))
                        {
                            result = v;
                        }
                    }
                }
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
