using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sequencer.Interface
{
    public interface IQueueModel
    {
        string Name { get; set; }
        object Item { get; set; }
    }
}
