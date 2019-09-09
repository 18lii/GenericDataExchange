using Core.Dependency;
using Database.Infrastructure;
using Database.Interface;
using Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConsole
{
    public class PeristalticConfiguration : IPeristalticConfiguration
    {
        public IBindContext Context { get; set; }
        public string ConnectionString { get; set; }
        public PeristalticConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public IBindContext Configuration()
        {
            Context.BindDatabase(1);
            return Context;
        }
    }
}
