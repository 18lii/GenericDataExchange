using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    /// <summary>
    /// 通用返回类型，本接口用于接收返回结果
    /// </summary>
    public interface IGenericResult
    {
        /// <summary>
        ///  获取或设置操作结果类型
        /// </summary>
        ResultType ResultType { get; set; }

        /// <summary>
        ///  获取或设置操作返回信息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///  获取或设置操作返回的日志消息，用于记录日志
        /// </summary>
        string LogMessage { get; set; }

        /// <summary>
        ///  获取或设置操作结果附加信息
        /// </summary>
        object AppendData { get; set; }
    }
    public interface IGenericResult<T>
    {
        /// <summary>
        ///  获取或设置操作结果类型
        /// </summary>
        ResultType ResultType { get; set; }

        /// <summary>
        ///  获取或设置操作返回信息
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///  获取或设置操作返回的日志消息，用于记录日志
        /// </summary>
        string LogMessage { get; set; }

        /// <summary>
        ///  获取或设置操作结果附加信息
        /// </summary>
        T Item { get; set; }
    }
    public interface IGenericPageData
    {
        Hashtable[] Data { get; }
        int Total { get; }
    }
}
