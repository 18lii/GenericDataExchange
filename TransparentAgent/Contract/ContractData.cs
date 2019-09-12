﻿using System;
using System.Collections;
using System.Data;
using TransparentAgent.Interface;

namespace TransparentAgent.Contract
{
    [Serializable]
    public class ContractData : IContractData
    {
        public string UserId { get; set; }
        public string[] SqlText { get; set; }
        public Hashtable[] Param { get; set; }
        public DataSet[] DataSet { get; set; }
    }
}
