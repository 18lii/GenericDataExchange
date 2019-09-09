using Core.Events;
using Core.Interface;
using Database.Interface;
using Queue.Entities;
using Queue.EventContext;
using Queue.Interface;
using Queue.Peristaltic;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Queue
{
    /**
     * Shine
     * 2019-9-5  
     * 线程绑定器
     * 使用<see cref="IBindContext"/>公共接口，于外部配置线程所必须的类型绑定及事件处理方法绑定
     * 于外部初始化方法中一次性调用<see cref="Initialization.PeristalticStart(IPeristalticConfiguration)"/>
     * 以启动线程
     * 
     **/

    /// <summary>
    /// 线程初始化类，
    /// </summary>
    internal class PeristalticContext : IBindContext
    {
        public WaitHandle[] LoaderSignal { get; }
        public ConcurrentDictionary<Guid, WaitHandle[]> ResultSignal { get; }
        public ConcurrentQueue<QueueModel> Troops { get; }
        public ConcurrentDictionary<string, int> ExecuterDefault { get; set; }
        public ConcurrentDictionary<string, Action<WaitHandle[], ConcurrentQueue<QueueModel>>> Actions { get; }
        public ConcurrentDictionary<string, bool> Register { get; }
        public ConcurrentDictionary<Guid, IGenericResult> Result { get; }
        public IContext DbContext { get; }
        public PeristalticContext(IContext dbContext)
        {
            LoaderSignal = new WaitHandle[2] { new AutoResetEvent(false), new ManualResetEvent(false)};
            Troops = new ConcurrentQueue<QueueModel>();
            ResultSignal = new ConcurrentDictionary<Guid, WaitHandle[]>();
            ExecuterDefault = new ConcurrentDictionary<string, int>();
            Actions = new ConcurrentDictionary<string, Action<WaitHandle[], ConcurrentQueue<QueueModel>>>();
            Register = new ConcurrentDictionary<string, bool>();
            Result = new ConcurrentDictionary<Guid, IGenericResult>();
            DbContext = dbContext;
            Action<WaitHandle[], ConcurrentQueue<QueueModel>> result = ((signal, queue) =>//结果线程委托
            {
                new QueueExecuter<IGenericEventArg<IFactoryContext>>
                (
                    signal,
                    queue,
                    new ActionEventWorker<IGenericEventArg<IFactoryContext>>
                    (
                        new GenericEventHandle<IGenericEventArg<IFactoryContext>>().Register
                        (
                            r =>
                            {
                                Result.TryAdd(r.Id, (IGenericResult)r.AttachData);
                                ResultSignal.TryAdd(r.Id, new WaitHandle[2] 
                                {
                                    new AutoResetEvent(false),
                                    new ManualResetEvent(false)
                                });
                                ((ManualResetEvent)ResultSignal[r.Id][1]).Set();
                            }
                        )
                    ).Action
                ).Execute();
            });
            Actions.TryAdd("Result", result);
            ExecuterDefault.TryAdd("Result", 1);
        }
        public void BindDatabase(int d = 1)
        {
            Action<WaitHandle[], ConcurrentQueue<QueueModel>> action = (signal, queue) =>
            {
                new QueueExecuter<IGenericEventArg<IFactoryContext>>
                (
                    signal,
                    queue,
                    new FunctionEventWorker<IFactoryContext, IGenericResult>
                    (
                        new GenericEventHandle<IGenericEventArg<IFactoryContext>, IGenericResult>().Register
                        (
                            DbContext.Activing
                        ),
                        LoaderSignal, 
                        Troops
                    ).Action
                ).Execute();
            };
            Actions.TryAdd("DatabaseService", action);
            ExecuterDefault.TryAdd("DatabaseService", d);
        }
        //数据库异步线程
        public void BindDatabaseAsync(AsyncCallback a, int d = 1)
        {
            Action<WaitHandle[], ConcurrentQueue<QueueModel>> action = (signal, queue) =>//处理线程委托
            {
                new QueueExecuter<IGenericEventArg<IFactoryContext>>
                (
                    signal, //信号
                    queue, //队列
                    new FunctionEventWorker<IFactoryContext, IGenericResult>//内部处理事件
                    (
                        new GenericEventHandle<IGenericEventArg<IFactoryContext>, IGenericResult>().Register//核心事件
                        (
                            DbContext.Activing//数据库工厂处理方法
                        ),
                        a//数据库操作回调函数
                    ).ActionAsync//处理线程委托方法
                ).Execute();//处理线程核心方法
            };
            Actions.TryAdd("DatabaseServiceAsync", action);
            ExecuterDefault.TryAdd("DatabaseServiceAsync", d);
        }
    }
}
