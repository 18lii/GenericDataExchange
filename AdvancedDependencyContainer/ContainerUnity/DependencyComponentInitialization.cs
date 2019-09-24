using AdvancedDependencyContainer.Configurations;
using AdvancedDependencyContainer.Dependency;
using AdvancedDependencyContainer.Event;
using AdvancedDependencyContainer.Helper;
using AdvancedDependencyContainer.Interface;
using System.Configuration;
using System.Reflection;

namespace AdvancedDependencyContainer.ContainerUnity
{
    /***************关于以配置文件方法进行依赖绑定的说明****************
     * 
     * 当方法Initialization参数useFile为true时，将应用app.config自定义section节
     * 用于依赖注入绑定，若要使用此功能，app.config需配置以下项
     * configSections中务必配置section元素
     * name：dependencyConfiguration
     * type：AdvancedDependencyContainer.Configurations.DependencyConfigurationSection, AdvancedDependencyContainer
     * dependencyConfiguration节的结构为：
     * <dependencyConfiguration>
     *  <binds>
     *    <assembly provider="程序集名称">
     *      <add keyLocation="接口所在命名空间" key="接口名称" valueLocation="实现类所在命名空间" value="实现类名称" />
     *      ...
     *    </assembly>
     *    ...
     *  </binds>
     *  
     *  ******Object Create by Shine Lee 2019-09-19******
     */

    /// <summary>
    /// 依赖注入组件初始化类
    /// </summary>
    public class DependencyComponentInitialization
    {
        private readonly IDependencyConfiguration _dependencyConfiguration;
        public DependencyComponentInitialization(IDependencyConfiguration dependencyConfiguration)
        {
            _dependencyConfiguration = dependencyConfiguration;
            _dependencyConfiguration.DependencyBindContext = new DependencyBindContext
            {
                IoCKernel = new IoCKernel()
            };
        }
        /// <summary>
        /// 依赖绑定初始化方法，当参数<see cref="bool"/> useFile 为<see cref="true"/>时，
        /// 将应用app.config配置文件中的自定义section-> dependencyConfiguration节作为绑定依据
        /// 若未正确配置，将抛出异常
        /// </summary>
        /// <param name="useFile"></param>
        public void Initialization(bool useFile = false)
        {
            //通过配置文件绑定依赖组件
            if (useFile)
            {
                var section = ConfigurationManager.GetSection("dependencyConfiguration").CastTo<DependencyConfigurationSection>();
                if(section != null && section.Binds.Count > 0)
                {
                    foreach (AssemblyElementCollection item in section.Binds)
                    {
                        if (string.IsNullOrEmpty(item.Provider))
                        {
                            continue;
                        }
                        else
                        {
                            var assembly = Assembly.Load(item.Provider);
                            foreach (AssemblyElement element in item)
                            {
                                if (string.IsNullOrEmpty(element.KeyLocation)
                                    || string.IsNullOrEmpty(element.Key)
                                    || string.IsNullOrEmpty(element.ValueLocation)
                                    || string.IsNullOrEmpty(element.Value)) continue;
                                var keyLocation = item.Provider + "." + element.KeyLocation + "." + element.Key;
                                var valLocation = item.Provider + "." + element.ValueLocation + "." + element.Value;
                                try
                                {
                                    _dependencyConfiguration.DependencyBindContext.IoCKernel.Bind(assembly.GetType(keyLocation)).To(assembly.GetType(valLocation));
                                }
                                catch
                                {
                                    continue;
                                }
                                
                            }
                        }
                        
                    }
                }
            }
            //绑定用户定义依赖
            var configuration = _dependencyConfiguration.BindDependency();
            //注册控制反转事件
            DependencyEventHandle.Register(configuration.IoCKernel.Resolve);
        }
    }
}
