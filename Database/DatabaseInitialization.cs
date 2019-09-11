using Core.Interface;
using Database.Entities;
using Database.Infrastructure;
using Database.Interface;
using Database.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DatabaseInitialization
    {
        private readonly IIoCKernel _kernel;
        public DatabaseInitialization(IIoCKernel kernel)
        {
            _kernel = kernel;
        }
        public void Initialization()
        {
            _kernel.Bind<IWorkContext>().To<WorkContext>();
            _kernel.Bind<ISqlAdapterDatabaseUtil>().To<SqlAdapterDatabaseUtil>();
            _kernel.Bind<ISqlServerDatabaseUtil>().To<SqlServerDatabaseUtil>();
            
        }
    }
}
