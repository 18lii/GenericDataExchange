using Sequencer.Events;
using System;

namespace Sequencer.Interface
{
    public interface ISequencerEntry
    {
        event Action<object> ResultCallbackEvent;
        /// <summary>
        /// 
        /// </summary>
        void AccessAsync(string Name, object item, bool sequence);
        object Access(string Name, object item, bool sequence);
    }
}
