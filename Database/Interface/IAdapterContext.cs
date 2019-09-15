using Database.Helper;
using System;
using System.Data;

namespace Database.Interface
{
    public interface IAdapterContext : IBaseContext
    {
        Tuple<bool, object> Activing(Tuple<AptOperate, string[], DataSet[]> context);
    }
}
