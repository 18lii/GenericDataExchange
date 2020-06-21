using Database.Helper;
using System;
using System.Collections;
using System.Collections.Concurrent;
using TransparentAgent.Interface;
using WCFService.Infrastructure;

namespace WCFService.Helper
{
    public static class Extensions
    {
        /// <summary>
        /// 将 <see cref="string"/>[] <see cref="Hashtable"/>[] 类型参数转换为内部标准参数
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Tuple<int, ConcurrentDictionary<string, Hashtable>> ToContextParam(this byte[] value, int operateCode, out bool sequence)
        {
            var data = value.Decompress<IContractData>();
            var dic = new ConcurrentDictionary<string, Hashtable>();
            for (var i = 0; i < data.SqlText.Length; i++)
            {
                dic.TryAdd(data.SqlText[i], data.Param[i]);
            }
            sequence = data.Sequence;
            return new Tuple<int, ConcurrentDictionary<string, Hashtable>>(operateCode, dic);
        }
    }
}
