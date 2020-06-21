using Sequencer.Events;
using Sequencer.Interface;
using Sequencer.Peristaltic;
using System.Threading;

namespace Sequencer
{
    /// <summary>
    /// 定序器初始化类
    /// </summary>
    public class SequencerInitialization
    {
        private readonly BindContext _bindContext;
        private readonly IPeristalticConfiguration _configuration;
        public SequencerInitialization(IPeristalticConfiguration configuration)
        {
            _bindContext = new BindContext();
            _configuration = configuration;
        }
        /// <summary>
        /// 调用此方法用以启动消息队列
        /// </summary>
        /// <param name="e"></param>
        public void PeristalticStart()
        {
            //初始化上下文类
            _configuration.Context =_bindContext;
            
            //配置启动项
            var context = (BindContext)_configuration.Configuration();

            //注册事件
            SequenceEventProvider.AccessEvent += context.Attacher.Add; //入列事件，外部
            SequenceEventProvider.AppendEvent += context.Resulter.Add;//处理结果回调函数注册事件
            SequenceEventProvider.RemoveEvent += context.Resulter.Remove;//处理结果回调函数移除事件
            //启动队列线程
            new Thread(new QueueDispatch(context.LoaderSignal, context.Troops, context.Actions, context.ExecuterDefault).Dispatch)
            {
                Name = "GSP-DISPATCH:#default"
            }.Start();
        }
    }
}
