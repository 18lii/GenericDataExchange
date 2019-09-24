using AdvancedDependencyContainer.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Infrastructure
{
    /// <summary>
    /// 压缩解压类
    /// </summary>
    internal static class CompressionExtensions
    {
        /// <summary>
        /// 压缩对象为字节数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] Compression(this object obj)
        {
            byte[] buffer;
            // 压缩            
            using (var formatStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(formatStream, obj);
                var formatter = formatStream.GetBuffer();
                using (var buffStream = new MemoryStream())
                {
                    using (var deflateStream = new DeflateStream(buffStream, CompressionMode.Compress, true))
                    {
                        deflateStream.Write(formatter, 0, formatter.Length);
                    }
                    buffStream.Seek(0, SeekOrigin.Begin);
                    int length = Convert.ToInt32(buffStream.Length);
                    buffer = new byte[length];
                    buffStream.Read(buffer, 0, length);
                }
            }
            return buffer;
        }
        /// <summary>
        /// 解压缩字节数组为<see cref="T"/>类型对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static T Decompress<T>(this byte[] byteArray)
        {
            var defLen = byteArray.Length * 2;
            using (var memoryStream = new MemoryStream(byteArray))
            {
                using (var deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
                {
                    using (var uncompressedstream = new MemoryStream())
                    {
                        using (var writer = new BinaryWriter(uncompressedstream))
                        {
                            int offset = 0;
                            while (true)
                            {
                                byte[] tempbuffer = new byte[defLen];

                                int bytesread = deflateStream.Read(tempbuffer, offset, defLen);

                                writer.Write(tempbuffer, 0, bytesread);

                                if (bytesread < defLen || bytesread == 0) break;
                            }

                            uncompressedstream.Seek(0, SeekOrigin.Begin);
                            var obj = new BinaryFormatter().Deserialize(uncompressedstream);
                            return (T)obj;
                        }
                    }
                }
            }
        }
    }
}
