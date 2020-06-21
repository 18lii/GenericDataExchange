using System;

namespace Sequencer.Events
{
    internal delegate void AccessEventHandle(string n, Guid i, object o, bool s);
    internal delegate bool ResultEventHandle(Guid id, Action<object> method);
    internal delegate void RemoveEventHandle(Guid id);
    /// <summary>
    /// 全局通用队列入列事件管理类
    /// </summary>
    internal static class SequenceEventProvider
    {
        private static AccessEventHandle _accessing;
        internal static event AccessEventHandle AccessEvent
        {
            add
            {
                if(_accessing == null)
                {
                    _accessing += value;
                }
            }
            remove { }
        }
        public static bool OnAccessEvent(string n, Guid i, object o, bool s)
        {
            try
            {
                _accessing.Invoke(n, i, o, s);
                return true;
            }
            catch
            {
                return false;
            }

        }
        private static ResultEventHandle _append;
        public static event ResultEventHandle AppendEvent
        {
            add
            {
                if(_append == null)
                {
                    _append += value;
                }
            }
            remove { }
        }
        
        public static void OnAppendEvent(Guid id, Action<object> method)
        {
            _append.Invoke(id, method);
        }

        private static RemoveEventHandle _remove;
        public static event RemoveEventHandle RemoveEvent
        {
            add
            {
                if(_remove == null)
                {
                    _remove += value;
                }
            }
            remove { }
        }
        public static void OnRemoveEvent(Guid id)
        {
            _remove.Invoke(id);
        }
    }
}
