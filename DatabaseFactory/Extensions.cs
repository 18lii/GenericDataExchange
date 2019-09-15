using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseUtil
{
    public static class Extensions
    {
        /// <summary>
        /// 将 <see cref="string"/>[] <see cref="Hashtable"/>[] 类型参数转换为内部标准参数
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static ConcurrentDictionary<string, Hashtable> ToContextParam(this string[] keys, Hashtable[] values)
        {
            var dic = new ConcurrentDictionary<string, Hashtable>();
            for (var i = 0; i < keys.Length; i++)
            {
                dic.TryAdd(keys[i], values[i]);
            }
            return dic;
        }
        public static ConcurrentDictionary<string, Hashtable> ToContextParam(this string key, Hashtable value)
        {
            var dic = new ConcurrentDictionary<string, Hashtable>();
            dic.TryAdd(key, value);
            return dic;
        }
    }
}
