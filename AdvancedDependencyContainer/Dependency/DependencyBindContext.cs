using AdvancedDependencyContainer.Interface;

namespace AdvancedDependencyContainer.Dependency
{
    internal class DependencyBindContext : IDependencyBindContext
    {
        public IIoCKernel IoCKernel { get; set; }
    }
}
