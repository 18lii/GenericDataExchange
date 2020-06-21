

namespace TransparentAgent.Interface
{
    /// <summary>
    /// 通用返回类型，本接口用于接收返回结果
    /// </summary>
    public interface IServiceResult
    {
        bool ResultType { get; set; }
        string Message { get; set; }
        string LogMessage { get; set; }
        object AppendData { get; set; }
    }
    public interface IServiceResult<T> { }
}
