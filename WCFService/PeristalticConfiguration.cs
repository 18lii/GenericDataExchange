using Database.Helper;
using Database.Interface;
using Queue.Interface;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data;

namespace WCFService
{
    public class PeristalticConfiguration : IPeristalticConfiguration
    {
        public IBindContext Context { get; set; }
        public readonly ICommandContext _commandContext;
        public readonly IAdapterContext _adapterContext;
        public PeristalticConfiguration(ICommandContext commandContext, IAdapterContext adapterContext, string connectionString)
        {
            commandContext.ConnectionString = connectionString;
            adapterContext.ConnectionString = connectionString;
            _commandContext = commandContext;
            _adapterContext = adapterContext;
        }
        public IBindContext Configuration()
        {
            Context.Bind<Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>, Tuple<bool, object>>("", _commandContext.Activing);
            Context.Bind<Tuple<AptOperate, string[], DataSet[]>, Tuple<bool, object>>("", _adapterContext.Activing);
            return Context;
        }
    }
}
