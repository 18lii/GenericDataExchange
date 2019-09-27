using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdvancedDependencyContainer.Entities
{
    /*
     * 此文件中的类为配置化数据库使用的序列化与反序列化类，暂不使用，仅做研究对象
     * 
     * ******Object Create By Shine Lee 2019-08-24******
     */

    /// <summary>
    /// 配置化数据库XML文档根节点
    /// </summary>
    [XmlRoot("GenericXmlDocument")]
    public sealed class BonyRoot
    {
        /// <summary>
        /// class节点集合
        /// </summary>
        [XmlElement("class")]
        public List<Bony> Records { get; set; }
    }
    /// <summary>
    /// class节点
    /// </summary>
    public sealed class Bony
    {
        /// <summary>
        /// xml中用作配置项数目, 反向使用时作为数据库表字段总数
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// xml属性，反向使用时模拟类名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        /// <summary>
        /// xml节点， 序列化时作为节点使用，反向使用时模拟类属性
        /// </summary>
        [XmlElement("dbName")]
        public string DbName { get; set; }
        /// <summary>
        /// xml节点，序列化时作为节点使用，反向使用时模拟类属性
        /// </summary>
        [XmlElement("property")]
        public List<BonyMarrow> Fields { get; set; }
    }
    /// <summary>
    /// 通用字段模型，用于模拟属性
    /// </summary>
    public sealed class BonyMarrow
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        [XmlAttribute("id")]
        public int Id { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        [XmlElement("field")]
        public string Name { get; set; }
        /// <summary>
        /// Value字段数据类型
        /// </summary>
        [XmlElement("dataType")]
        public DataType DataType { get; set; }
        /// <summary>
        /// 标题（可选）
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [XmlIgnore]
        public object Value { get; set; }
    }
    public class BonyObject
    {
        public BonyObject() { }
        public BonyObject(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public int Number { get; set; }
        public string DbName { get; set; }
        public ConcurrentBag<MarrowObject> Fields { get; set; }
    }
    public class MarrowObject
    {
        public MarrowObject() { }
        public MarrowObject(int id)
        {
            Id = id;
        }
        public int Id { get; }
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
