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
     * type：AdvancedDependencyContainer.Configurations.DependencyComponentAppConfiguration, AdvancedDependencyContainer
     * section节的结构为：
     * <dependencyConfiguration>
     *  <dependency>
     *    <composition provider="程序集名称">
     *       <component>
     *         <contract name="接口名称" location="接口所在命名空间" />
     *         <realizer name="实现名称" location="实现所在命名空间" />
     *       </component>
     *       ...
     *     </composition>
     *     ...
     *   </dependency>
     *  </dependency>
     * </dependencyConfiguration>
     * 
     *  ******Object Create by Shine Lee 2019-09-19******
     */

    /// <summary>
    /// 定义dependency节点
    /// </summary>
    internal sealed class DependencyComponentAppConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("dependency", IsDefaultCollection = true)]
        public AppCompositionCollection Dependency
        {
            get
            {
                return base["dependency"].CastTo<AppCompositionCollection>();
            }
        }
    }

    /// <summary>
    /// 定义composition节点集合
    /// </summary>
    //[ConfigurationCollection(typeof(AppComponentCollection), AddItemName = "composition")]
    internal sealed class AppCompositionCollection : ConfigurationElementCollection
    {
        public AppCompositionCollection()
        {
            base.AddElementName = "composition";
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new AppComponentCollection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.CastTo<AppComponentCollection>().Provider;
        }
    }
    /// <summary>
    /// 定义component节点集合
    /// </summary>
    internal sealed class AppComponentCollection : ConfigurationElementCollection
    {
        public AppComponentCollection()
        {
            base.AddElementName = "component";
        }
        /// <summary>
        /// 表示程序集名称
        /// </summary>
        [ConfigurationProperty("provider", IsRequired = false)]
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
    /// <summary>
    /// 定义component节点元素
    /// </summary>
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
    /// 定义component节点元素属性
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
