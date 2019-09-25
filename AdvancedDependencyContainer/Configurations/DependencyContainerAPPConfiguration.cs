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
     *      <bind>
     *        <add contract="接口所在命名空间.契约接口名称" realization="实现类所在命名空间.实现类名称" />
     *        ...
     *      </bind>
     *    </assembly>
     *    ...
     *  </dependency>
     * </dependencyConfiguration>
     *  ******Object Create by Shine Lee 2019-09-19******
     */

    /// <summary>
    /// 定义Binds节Section
    /// </summary>
    internal sealed class DependencyContainerAppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("dependency", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(AssemblyCollection), AddItemName = "assembly")]
        public AssemblyCollection Dependency
        {
            get
            {
                return base["dependency"].CastTo<AssemblyCollection>();
            }
        }
    }
    /// <summary>
    /// 定义binds元素集合
    /// </summary>
    internal sealed class AssemblyCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyElementCollection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.CastTo<AssemblyElementCollection>().Provider;
        }
    }
    /// <summary>
    /// 定义assembly元素集合
    /// </summary>
    internal sealed class AssemblyElementCollection : ConfigurationElement
    {
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
        [ConfigurationProperty("bind", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(BindElementCollection), AddItemName = "add")]
        public BindElementCollection BindCollection
        {
            get
            {
                return base["bind"].CastTo<BindElementCollection>();
            }
        }
    }
    /// <summary>
    /// 定义bind节
    /// </summary>
    internal sealed class BindElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.CastTo<AssemblyElement>().Contract;
        }
    }

    /// <summary>
    /// 定义key,val元素
    /// </summary>
    internal sealed class AssemblyElement : ConfigurationElement
    {
        /// <summary>
        /// 表示接口所在命名空间
        /// </summary>
        [ConfigurationProperty("contract", IsRequired = true, IsKey = false)]
        public string Contract
        {
            get
            {
                return base["contract"].CastTo<string>();
            }
            set
            {
                base["contract"] = value;
            }
        }
        /// <summary>
        /// 表示实现类所在命名空间
        /// </summary>
        [ConfigurationProperty("realization", IsRequired = true, IsKey = true)]
        public string Realization
        {
            get
            {
                return base["realization"].CastTo<string>();
            }
            set
            {
                base["realization"] = value;
            }
        }
    }
}
