using System;

namespace Queue.Interface
{
    /// <summary>
    /// 线程配置接口，用于配置线程的对象类型及处理方法，在<see cref="IPeristalticConfiguration.Configuration"/>方法中配置以下项：
    /// 用于绑定带有异步回调函数，有返回值的处理方法，返回值在异步回调中处理；</para>
    /// <para><see cref="void"/><see cref="IBindContext.BindDatabaseAsync(string, AsyncCallback, int)"/>用于绑定数据库服务线程</para>
    /// </summary>
    public interface IPeristalticConfiguration
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// 线程配置上下文
        /// </summary>
        IBindContext Context { get; set; }
        /// <summary>
        /// 线程配置方法
        /// 使用属性<see cref="IBindContext.Bind{T}()"/>方法或<see cref="IBindContext.BindAsync{T}()"/>进行线程绑定,
        /// 所有经绑定的线程均为驻留线程
        /// </summary>
        /// <returns></returns>
        IBindContext Configuration();
    }
    /// <summary>
    /// 消息队列配置接口
    /// </summary>
    public interface IBindContext
    {
        void BindDatabase(int d = 1);
        /// <summary>
        /// 绑定数据库线程，使用此绑定后，数据库管理异步模块可用,此线程采用队列处理，可等待，线程驻留。
        /// <para>委托类型：<see cref="Func{T, TResult}"/> T为<see cref="IPeristalticEventArg{}"/>, TResult为<see cref="IGenericResult"/></para>
        /// <para><see cref="string"/> connectionString表示数据库连接字符串</para>
        /// <para><see cref="int"/> def表示此线程启动数量，大于1时处理模式为并行；<see cref="IBindContext.Automatic"/>为<see cref="true"/>时参数无效</para>
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="def"></param>
        void BindDatabaseAsync(AsyncCallback a, int d = 1);
    }
}
