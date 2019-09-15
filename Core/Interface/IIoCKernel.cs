
namespace Core.Interface
{
    /// <summary>
    /// IoC内核接口，含绑定方法与注入方法
    /// </summary>
    public interface IIoCKernel
    {
        IIoCKernel Bind<T>();
        IIoCKernel To<U>() where U : class;
        V Resolve<V>(object parameter = null) where V : class;
    }
}
