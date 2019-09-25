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
    [XmlRoot("dependencyContainerConfiguration")]
    internal sealed class DependencyContainerXmlConfiguration
    {
        [XmlElement("dependency")]
        public XmlAssemblyCollection Dependency { get; set; }
    }
    /// <summary>
    /// 定义assembly节
    /// </summary>
    internal sealed class XmlAssemblyCollection
    {
        [XmlElement("assembly")]
        public List<XmlBindCollection> Assemblies { get; set; }
    }
    /// <summary>
    /// 定义bind节，表示assembly节点内含属性provider，子节点bind，
    /// </summary>
    internal sealed class XmlBindCollection
    {
        [XmlAttribute("provider")]
        public string Provider { get; set; }
        [XmlElement("bind")]
        public XmlBindElement Bind { get; set; }
    }
    /// <summary>
    /// 定义add元素
    /// </summary>
    internal sealed class XmlBindElement
    {
        [XmlElement("add")]
        public List<XmlKeyValElement> Elements { get; set; }
    }
    /// <summary>
    /// add元素属性
    /// </summary>
    internal sealed class XmlKeyValElement
    {
        [XmlAttribute("contract")]
        public string Contract { get; set; }
        [XmlAttribute("realization")]
        public string Realization { get; set; }
    }
}
