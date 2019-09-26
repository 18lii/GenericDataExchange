using AdvancedDependencyContainer.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace AdvancedDependencyContainer.Configurations
{
    /*
     * 本文件用于定义以app.config方式配置依赖组件绑定
     * app.config中于此相关的节点应按此结构进行组织
     * 用于依赖注入绑定
     * 
     * app.config需配置以下项
     * 
     * configSections中务必配置section元素
     * name：dependencyConfiguration
     * type：AdvancedDependencyContainer.Configurations.DependencyContainerAppConfiguration, AdvancedDependencyContainer
     * section节的结构为：
     * <dependencyConfiguration>
     *  <dependency>
     *    <assembly provider="程序集名称">
     *      <binds>
     *        <bind contract="接口所在命名空间.契约接口名称" realization="实现类所在命名空间.实现类名称" />
     *        ...
     *      </binds>
     *    </assembly>
     *    ...
     *  </dependency>
     * </dependencyConfiguration>
     * 
     *  ******Object Create by Shine Lee 2019-09-19******
     */

    /// <summary>
    /// 定义Section
    /// </summary>
    internal sealed class DependencyComponentAppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("dependency", IsDefaultCollection = true)]
        public AppAssemblyCollection Dependency
        {
            get
            {
                return base["dependency"].CastTo<AppAssemblyCollection>();
            }
        }
    }
    /// <summary>
    /// 定义component节点集合
    /// </summary>
    [ConfigurationCollection(typeof(AppCompositionCollection), AddItemName = "composition")]
    internal sealed class AppAssemblyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppCompositionCollection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.CastTo<AppCompositionCollection>().Provider;
        }
    }
    /// <summary>
    /// 定义component节点集合
    /// </summary>
    internal sealed class AppCompositionCollection : ConfigurationElementCollection
    {
        public AppCompositionCollection()
        {
            base.AddElementName = "component";
        }
        /// <summary>
        /// 表示程序集名称
        /// </summary>
        [ConfigurationProperty("provider", IsRequired = true)]
        public string Provider
        {
            get
            {
                return base["provider"].CastTo<string>();
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppComponentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.CastTo<AppComponentElement>();
        }
    }

    internal sealed class AppComponentElement : ConfigurationElement
    {
        [ConfigurationProperty("contract", IsRequired = true)]
        public AppComponentProperty Contract
        {
            get
            {
                return base["contract"].CastTo<AppComponentProperty>();
            }
        }
        [ConfigurationProperty("realizer", IsRequired = true)]
        public AppComponentProperty Realizer
        {
            get
            {
                return base["realizer"].CastTo<AppComponentProperty>();
            }
        }
    }
    /// <summary>
    /// 定义key,val元素
    /// </summary>
    internal sealed class AppComponentProperty : ConfigurationElement
    {
        /// <summary>
        /// 表示接口所在命名空间
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = false)]
        public string Name
        {
            get
            {
                return base["name"].CastTo<string>();
            }
        }
        /// <summary>
        /// 表示实现类所在命名空间
        /// </summary>
        [ConfigurationProperty("location", IsRequired = true, IsKey = true)]
        public string Location
        {
            get
            {
                return base["location"].CastTo<string>();
            }
        }
    }
}
