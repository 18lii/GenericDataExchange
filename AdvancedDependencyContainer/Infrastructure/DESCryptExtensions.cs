using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Infrastructure
{
    /// <summary>
    /// DES加密解密类
    /// </summary>
    internal static class DESCryptExtensions
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        public static string Encryptogram(this string str, string key = "#default")
        {
            using (var desp = new DESCryptoServiceProvider())
            {
                var byteArray = Encoding.Default.GetBytes(str);
                desp.IV = Encoding.Default.GetBytes(key);
                desp.Key = Encoding.Default.GetBytes(key);
                using (var buffStream = new MemoryStream())
                {
                    var cs = new CryptoStream(buffStream, desp.CreateEncryptor(), CryptoStreamMode.Write);
                    cs.Write(byteArray, 0, byteArray.Length);
                    cs.FlushFinalBlock();
                    var retStr = new StringBuilder();
                    foreach (byte b in buffStream.ToArray())
                    {
                        retStr.AppendFormat("{0:X2}", b);
                    }
                    return retStr.ToString();
                }
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="decryptStr"></param>
        /// <returns></returns>
        public static string Decryptogram(this string decryptStr, string key = "#default")
        {
            if (decryptStr.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var desp = new DESCryptoServiceProvider())
                        {
                            byte[] byteArray = new byte[decryptStr.Length / 2];
                            for (int x = 0; x < decryptStr.Length / 2; x++)
                            {
                                int i = (Convert.ToInt32(decryptStr.Substring(x * 2, 2), 16));
                                byteArray[x] = (byte)i;
                            }
                            desp.Key = Encoding.Default.GetBytes(key);
                            desp.IV = Encoding.Default.GetBytes(key);
                            using (var cs = new CryptoStream(memoryStream, desp.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(byteArray, 0, byteArray.Length);
                                cs.FlushFinalBlock();
                                return Encoding.Default.GetString(memoryStream.ToArray());
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    return default;
                }
            }
            else
            {
                return default;
            }
        }
    }
}
