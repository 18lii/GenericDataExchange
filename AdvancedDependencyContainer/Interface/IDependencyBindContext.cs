
namespace AdvancedDependencyContainer.Interface
{
    /// <summary>
    /// 依赖绑定上下文接口
    /// </summary>
    public interface IDependencyBindContext
    {
        /// <summary>
        /// IoC内核属性，用于绑定依赖组件
        /// </summary>
        IIoCKernel IoCKernel { get; set; }
        /// <summary>
        /// 依赖绑定初始化方法，
        /// 应用app.config配置文件中的自定义section节作为绑定依据
        /// 若未正确配置，请勿使用，否则可能引起程序崩溃
        /// </summary>
        /// <param name="sectionName"></param>
        void UseAppConfiguration(string sectionName);
        /// <summary>
        /// 依赖绑定初始化方法，参数<see cref="string"/> path 为配置文件路径(含文件名)，
        /// 若文件不存在，将自动生成配置文件模板，
        /// 若未正确配置，请勿使用，否则可能引起程序崩溃
        /// </summary>
        /// <param name="path"></param>
        void UseXmlConfiguration(string path);
    }
}
