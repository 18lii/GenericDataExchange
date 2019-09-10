using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Entity
{
    [Serializable]
    public class WCFData
    {
        public string SqlText { get; set; }
        public Hashtable Param { get; set; }
    }
}
