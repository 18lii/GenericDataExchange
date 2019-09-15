using System;
using System.Collections;
using System.Data;
using TransparentAgent;
using TransparentAgent.Interface;

namespace WCFService.Entity
{
    [Serializable]
    public class ReceiveData : IContractData
    {
        public string[] SqlText { get; set; }
        public Hashtable[] Param { get; set; }
        public DataSet[] DataSet { get; set; }
        public bool sequence { get; set; }
    }
}
