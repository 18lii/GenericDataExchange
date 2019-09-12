using Core.Interface;
using DatabaseFactory.Interface;

namespace Database.Interface
{
    /// <summary>
    /// 数据库操作上下文接口
    /// </summary>
    public interface IWorkContext : IBaseContext
    {
        IGenericResult Activing(IGenericEventArg<IFactoryContext> e);
    }
}
