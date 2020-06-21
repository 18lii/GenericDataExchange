using Sequencer.Events;
using Sequencer.Interface;
using System;

namespace Sequencer
{
    /// <summary>
    /// 定序器入口类
    /// </summary>
    internal class SequencerEntry : ISequencerEntry, IDisposable
    {
        private Guid Id { get; }
        public event Action<object> ResultCallbackEvent
        {
            add
            {
                SequenceEventProvider.OnAppendEvent(Id, value);
            }
            remove { }
        }
        public SequencerEntry()
        {
            Id = Guid.NewGuid();
        }
        public void AccessAsync(string Name, object item, bool sequence)
        {
            SequenceEventProvider.OnAccessEvent(Name, Id, item, sequence);
        }

        public object Access(string Name, object item, bool sequence)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            SequenceEventProvider.OnRemoveEvent(Id);
        }
    }
}
