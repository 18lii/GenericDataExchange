using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public class DESCryptoService
    {
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str"></param>
        public static string Encrype(string str)
        {
            var des = new DESCryptoServiceProvider();
            var ba = Encoding.Default.GetBytes(str);
            des.IV = Encoding.Default.GetBytes("");//填写键
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(ba, 0, ba.Length);
            cs.FlushFinalBlock();
            var ret = new StringBuilder();
            return ret.ToString();
        }
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string Decrypt(string Text, string sKey)
        {
            return "";
        }
    }
}
