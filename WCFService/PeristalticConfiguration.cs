using Database.Helper;
using Database.Interface;
using Queue.Interface;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;

namespace WCFService
{
    /// <summary>
    /// 队列绑定配置类，用于配置队列处理线程接受类型及返回类型
    /// </summary>
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
            var name1 = "SqlClient";
            var name2 = "AdoClient";
            Context.Bind<Tuple<CmdOperate, ConcurrentDictionary<string, Hashtable>>, Tuple<bool, object>>(name1, _commandContext.Activing);
            Context.Bind<Tuple<AptOperate, string[], DataSet[]>, Tuple<bool, object>>(name2, _adapterContext.Activing);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("QueueExecute1");
            config.AppSettings.Settings.Remove("QueueExecute2");
            config.AppSettings.Settings.Add("QueueExecute1", name1);
            config.AppSettings.Settings.Add("QueueExecute2", name2);
            config.Save();
            var path = config.FilePath;
            ConfigurationManager.RefreshSection("appSettings");
            string _sqlClientName = ConfigurationManager.AppSettings["QueueExecute1"];
            string _adoClientName = ConfigurationManager.AppSettings["QueueExecute2"];
            return Context;
        }
    }
}
