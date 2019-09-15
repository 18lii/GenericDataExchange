using Core.Interface;
using Database.Infrastructure;
using Database.Interface;
using Database.Util;

namespace Database
{
    public class Initialization
    {
        private readonly IIoCKernel _kernel;
        public Initialization(IIoCKernel kernel)
        {
            _kernel = kernel;
        }
        public void BindToCore()
        {
            _kernel.Bind<ICommandContext>().To<CommandContext>();
            _kernel.Bind<IAdapterContext>().To<AdapterContext>();
            _kernel.Bind<ISqlAdapterDatabaseUtil>().To<SqlAdapterDatabaseUtil>();
            _kernel.Bind<ISqlCommandDatabaseUtil>().To<SqlCommandDatabaseUtil>();
        }
    }
}
