using Core.Interface;
using Database.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Entity
{
    /// <summary>
    /// 数据库操作工厂实现类
    /// </summary>
    public class DbFactoryImpl : DatabaseFactory
    {
        public DbFactoryImpl(IGenericEventHandle eventHandle) : base(eventHandle) { }
    }
}
