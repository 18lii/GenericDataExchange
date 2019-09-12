using System.Collections;
using System.Data;

namespace TransparentAgent.Interface
{
    /// <summary>
    /// 契约数据类型接口
    /// </summary>
    public interface IContractData
    {
        string UserId { get; set; }
        string[] SqlText { get; set; }
        Hashtable[] Param { get; set; }
        DataSet[] DataSet { get; set; }
    }
}
