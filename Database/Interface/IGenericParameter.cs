using Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interface
{
    /// <summary>
    /// 通用参数
    /// </summary>
    public interface IGenericParameter
    {
        string SqlText { get; set; }
        Hashtable Params { get; set; }
        ParamType ParamType { get; set; }
    }
}
