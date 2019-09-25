using AdvancedDependencyContainer.Helper;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AdvancedDependencyContainer.Infrastructure
{
    internal static class XmlUtil
    {


        /// <summary>
        /// 反序列化xml文档为T类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string path)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path);
                var outer = xmlDoc.OuterXml;
                using (var sr = new StringReader(outer))
                {
                    var xmldes = new XmlSerializer(typeof(T));
                    var des = xmldes.Deserialize(sr);
                    return des.CastTo<T>();
                }
            }
            catch (Exception)
            {
                return default;
            }
        }
        /// <summary>
        /// 序列化对象为xml
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static string Serializer<T>(T obj)
        {
            try
            {
                var xmlText = new StringBuilder();
                using (var stream = new MemoryStream())
                {
                    var xml = new XmlSerializer(typeof(T));
                    var xmlns = new XmlSerializerNamespaces();
                    var setting = new XmlWriterSettings
                    {
                        Indent = true,
                        IndentChars = "    ",
                        NewLineChars = "\r\n",
                        Encoding = Encoding.UTF8
                    };
                    using (var writer = XmlWriter.Create(stream, setting))
                    {
                        xmlns.Add("", "");
                        xml.Serialize(writer, obj, xmlns);
                    }
                    using (var reader = new StreamReader(stream))
                    {
                        stream.Position = 0;
                        xmlText.Append(reader.ReadToEnd());
                    }
                }
                return xmlText.ToString();
            }
            catch(Exception e)
            {
                return e.Message;
            }
            
        }
    }
}
