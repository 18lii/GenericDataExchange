using System.Collections.Generic;
using System.Xml.Serialization;

namespace AdvancedDependencyContainer.Configurations
{
    /*
     * 本文件用于定义以XML方式配置依赖组件绑定，
     * 配置文件应按此结构进行组织
     *xxxx.cfg文件需配置以下项
     * 
     * <dependencyContainerConfiguration>
     *   <dependency>
     *     <assembly provider="程序集名称">
     *       <bind>
     *         <add contract="接口所在命名空间.接口名称" realization="实现类所在命名空间.类名称" />
     *         ...
     *       <bind>
     *     </assembly>
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
        public XmlAssemblyCollection Dependency { get; set; }
    }
    /// <summary>
    /// 定义assembly节点
    /// </summary>
    public sealed class XmlAssemblyCollection
    {
        /// <summary>
        /// assembly节点集合
        /// </summary>
        [XmlElement("assembly")]
        public List<XmlAssemblyElement> Assemblies { get; set; }
    }
    /// <summary>
    /// 定义binds节点
    /// </summary>
    public sealed class XmlAssemblyElement
    {
        /// <summary>
        /// assembly节点属性provider
        /// </summary>
        [XmlAttribute("provider")]
        public string Provider { get; set; }
        /// <summary>
        /// binds节点
        /// </summary>
        [XmlElement("binds")]
        public XmlBindCollection Binds { get; set; }
    }
    /// <summary>
    /// 定义bind节点
    /// </summary>
    public sealed class XmlBindCollection
    {
        /// <summary>
        /// bind元素集合
        /// </summary>
        [XmlElement("bind")]
        public List<XmlBindElement> BindElements { get; set; }
    }
    /// <summary>
    /// bind元素
    /// </summary>
    public sealed class XmlBindElement
    {
        /// <summary>
        /// bind节点属性contract
        /// </summary>
        [XmlAttribute("contract")]
        public string Contract { get; set; }
        /// <summary>
        /// bind节点属性realization
        /// </summary>
        [XmlAttribute("realization")]
        public string Realization { get; set; }
    }
}
