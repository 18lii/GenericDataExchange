using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedDependencyContainer.Interface
{
    public interface ILogMessage
    {
        string Name { get; }
        string UserId { get; }
        Guid Id { get; }
        string OperateType { get; }
        string Message { get; }
        string LogMessage { get; }
        string Transaction { get; }
        DateTime TimeStamp { get; }
    }
}
