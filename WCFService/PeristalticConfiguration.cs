using Core.Infrastructure;
using Queue.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService
{
    public class PeristalticConfiguration : IPeristalticConfiguration
    {
        public IBindContext Context { get; set; }
        public string ConnectionString { get; set; }
        public PeristalticConfiguration(string[] codes)
        {
            ConnectionString = codes[0].Decryptogram(codes[1].Decryptogram());
        }
        public IBindContext Configuration()
        {
            Context.BindDatabase(1);
            return Context;
        }
    }
}
