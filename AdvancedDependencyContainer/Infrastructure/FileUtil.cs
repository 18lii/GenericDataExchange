using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Infrastructure
{
    internal static class FileUtil
    {
        public static void FileCreate(byte[] buffer, string path)
        {
            using (var stream = File.Create(path))
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }
        }
    }
}
