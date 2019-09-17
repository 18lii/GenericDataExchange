using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFService.Service
{
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void Reuslt(byte[] value);
    }
}
