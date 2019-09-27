using AdvancedDependencyContainer.Helper;
using AdvancedDependencyContainer.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedDependencyContainer.Configurations
{
    /*
     * 本文件用于定义以XML方式配置依赖组件绑定，
     * 配置文件应按此结构进行组织
     *xxxx.cfg文件需配置以下项
     * 
     * <dependencyComponentConfiguration>
     *   <dependency>
     *     <composition provider="程序集名称">
     *       <component>
     *         <contract name="接口名称" location="接口所在命名空间" />
     *         <realizer name="实现名称" location="实现所在命名空间" />
     *       </component>
     *       ...
     *     </composition>
     *     ...
     *   </dependency>
     * </dependencyContainerConfiguration>
     * 
     * 
     * ******Object Creat By Shine Lee 2019-09-19******
     */

    /// <summary>
    /// 定义ROOT节
    /// </summary>
    [XmlRoot("dependencyComponentConfiguration")]
    public sealed class DependencyComponentXmlConfiguration
    {
        /// <summary>
        /// dependency节点
        /// </summary>
        [XmlElement("dependency")]
        public XmlCompositionCollection Dependency { get; set; }
    }
    /// <summary>
    /// 定义composition节点集合
    /// </summary>
    public sealed class XmlCompositionCollection
    {
        /// <summary>
        /// component节点集合
        /// </summary>
        [XmlElement("composition")]
        public List<XmlComponentCollection> Compositions { get; set; }
    }
    public sealed class XmlComponentCollection
    {
        /// <summary>
        /// assembly节点属性provider
        /// </summary>
        [XmlAttribute("provider")]
        public string Provider { get; set; }
        [XmlElement("component")]
        public List<XmlComponentElement> Components { get; set; }
    }
    
    /// <summary>
    /// 定义component节点元素
    /// </summary>
    public sealed class XmlComponentElement
    {
        /// <summary>
        /// contract节点
        /// </summary>
        [XmlElement("contract", IsNullable = false)]
        public XmlComponentProperty Contract { get; set; }
        /// <summary>
        /// realizer节点
        /// </summary>
        [XmlElement("realizer", IsNullable = false)]
        public XmlComponentProperty Realizer { get; set; }
    }
    /// <summary>
    /// 定义component节点元素属性
    /// </summary>
    public sealed class XmlComponentProperty
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("location")]
        public string Location { get; set; }
    }
}
