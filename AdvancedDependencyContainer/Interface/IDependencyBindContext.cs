
namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// 依赖绑定上下文接口
    /// </summary>
    public interface IDependencyBindContext
    {
        IIoCKernel IoCKernel { get; set; }
    }
}
