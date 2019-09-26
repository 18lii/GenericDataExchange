
namespace AdvancedDependencyContainer.Interface
{
    /*
     * 依赖组件绑定上下文接口，可编码绑定&配置文件绑定，
     * 配置文件绑定分为app.config文件自定义节配置与XML文件配置
     * 使用配置文件方式时，务必确保按指定结构进行编写，否则容易造成程序无法启动或崩溃
     * 
     * ******Ojbect Create By Shine Lee 2019-09-17******
     * 
     */
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
        /// 依赖绑定初始化方法，参数<see cref="string"/> sectionName为app.config配置文件中的自定义section节
        /// <para>！！！若未正确配置，请勿使用，否则可能引起程序崩溃！！！</para>
        /// </summary>
        /// <param name="sectionName"></param>
        void UseAppConfiguration(string sectionName);
        /// <summary>
        /// 依赖绑定初始化方法，参数<see cref="string"/> fileName 为配置文件名，
        /// 配置文件必须位于应用程序根目录，
        /// 若文件不存在，将自动生成配置文件模板。
        /// <para>！！！若未正确配置，请勿使用，否则可能引起程序崩溃！！！</para>
        /// </summary>
        /// <param name="path"></param>
        void UseXmlConfiguration(string fileName);
    }
}
