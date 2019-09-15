using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Interface
{
    public interface IResultEventArgs
    {
        Guid Id { get; set; }
        object Item { get; set; }
    }
}
