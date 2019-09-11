using Core.Entities;
using System;
using System.Collections;
using System.Data;
using System.Runtime.Serialization;

namespace WCFService.Entity
{
    [Serializable]
    [DataContract(Namespace ="http://1.1.1.1:8733/DataExchangeService")]
    public class ReceiveData
    {
        [DataMember(Order = 0)]
        public string UserId { get; set; }
        [DataMember(Order = 1)]
        public string SqlText { get; set; }
        [DataMember(Order = 2)]
        public Hashtable[] Param { get; set; }
        [DataMember(Order = 3)]
        public DataSet DataSet { get; set; }
        [DataMember(Order = 4)]
        public PolicyType PolicyType { get; set; }
    }
}
