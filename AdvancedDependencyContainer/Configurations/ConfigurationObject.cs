using AdvancedDependencyContainer.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace AdvancedDependencyContainer.Configurations
{
    /*
     * 自定义app.config中section节所使用的实体类
     * 用于依赖注入绑定
     *  
     *  ******Object Create by Shine Lee 2019-09-19******
     */

    /// <summary>
    /// 定义Add元素
    /// </summary>
    public class AssemblyElement : ConfigurationElement
    {
        /// <summary>
        /// 表示接口所在命名空间
        /// </summary>
        [ConfigurationProperty("keyLocation", IsRequired = true, IsKey = true)]
        public string KeyLocation
        {
            get
            {
                return base["keyLocation"].CastTo<string>();
            }
            set
            {
                base["keyLocation"] = value;
            }
        }
        /// <summary>
        /// 表示实现类所在命名空间
        /// </summary>
        [ConfigurationProperty("valueLocation", IsRequired = true, IsKey = true)]
        public string ValueLocation
        {
            get
            {
                return base["valueLocation"].CastTo<string>();
            }
            set
            {
                base["valueLocation"] = value;
            }
        }
        /// <summary>
        /// 表示接口名称
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return base["key"].CastTo<string>();
            }
            set
            {
                base["key"] = value;
            }
        }
        /// <summary>
        /// 表示实现类名称
        /// </summary>
        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return base["value"].CastTo<string>();
            }
            set
            {
                base["value"] = value;
            }
        }
    }
    /// <summary>
    /// 定义assembly元素集合
    /// </summary>
    [ConfigurationCollection(typeof(AssemblyElement), AddItemName = "add")]
    public class AssemblyElementCollection : ConfigurationElementCollection
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
        protected override ConfigurationElement CreateNewElement()
        {
            return new AssemblyElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element.CastTo<AssemblyElement>().Key;
        }
    }
    /// <summary>
    /// 定义binds元素集合
    /// </summary>
    [ConfigurationCollection(typeof(AssemblyElementCollection), AddItemName = "assembly")]
    public class AssemblyCollection : ConfigurationElementCollection
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
    /// 定义Binds节Section
    /// </summary>
    public class DependencyConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("binds", IsDefaultCollection = false)]
        public AssemblyCollection Binds
        {
            get
            {
                return base["binds"].CastTo<AssemblyCollection>();
            }
        }
    }
    
    
    
}
