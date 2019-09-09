using Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    /// <summary>
    /// 数据库操作工厂接口
    /// </summary>
    public interface IContext : IBaseContext
    {
        IGenericResult Activing(IGenericEventArg<IFactoryContext> e);
    }
}
