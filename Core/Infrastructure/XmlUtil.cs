using Core.Entities;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Core.Infrastructure
{
    internal static class XmlUtil
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IList<Bony> Deserialize(this BonyRoot value, string path)
        {
            var type = typeof(BonyRoot);
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path);
                var outer = xmlDoc.OuterXml;
                using (var sr = new StringReader(outer))
                {
                    var xmldes = new XmlSerializer(type);
                    var des = xmldes.Deserialize(sr);
                    return (des as BonyRoot).Records;
                }
            }
            catch (Exception e)
            {
                return new List<Bony>();
            }
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Serializer(this BonyRoot value)
        {
            var type = typeof(BonyRoot);
            var stream = new MemoryStream();
            var xml = new XmlSerializer(type);
            var setting = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    ",
                NewLineChars = "\r\n",
                Encoding = Encoding.UTF8
            };
            using (var xw = XmlWriter.Create(stream, setting))
            {
                //序列化对象
                try
                {
                    xml.Serialize(xw, value);
                    stream.Position = 0;
                    using(var sr = new StreamReader(stream))
                    {
                        var str = sr.ReadToEnd();
                        return str;
                    }
                }
                catch (Exception e)
                {
                    return new ExceptionMessage(e).ExMessage;
                }
            }
        }
    }
}
