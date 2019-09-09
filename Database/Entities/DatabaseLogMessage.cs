using Core.Interface;
using Database.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class DatabaseLogMessage : ILogMessage
    {
        public string Name { get; }
        public string UserId { get; }
        public Guid Id { get; }
        public string OperateType { get; }
        public string Message { get; }
        public string LogMessage { get; }
        public int Count { get; }
        public string Transaction { get; }
        public DateTime TimeStamp { get; } = DateTime.Now;
        public DatabaseLogMessage
            (string name, string userId, Guid id, string operateType, string message, string logMessage, int count, string transaction)
        {
            Name = name;
            UserId = userId;
            Id = id;
            OperateType = operateType;
            Message = message;
            LogMessage = logMessage;
            Transaction = transaction;
            Count = count;
        }
    }
}
