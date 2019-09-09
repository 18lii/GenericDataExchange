using Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public static class DataCompression
    {
        public static byte[] CompressionDataSet(this string gr)
        {
            var content = Encoding.Default.GetBytes(gr);
            // 压缩            
            var memoryStream = new MemoryStream();
            DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress);
            deflateStream.Write(content, 0, content.Length);
            deflateStream.Close();
            return memoryStream.ToArray();
        }
    }
}
