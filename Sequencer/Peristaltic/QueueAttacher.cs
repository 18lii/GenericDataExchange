using Sequencer.Entities;
using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;

namespace Sequencer.Peristaltic
{
    public struct vector
    {
        public double x, y, z;
        public vector(vector vhs)
        {
            x = vhs.x;
            y = vhs.y;
            z = vhs.z;
        }
        public vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public static vector operator +(vector n1, vector n2)
        {
            vector result = new vector(n1);
            result.x += n2.x;
            return result;
        }
        public static implicit operator float(vector v)
        {
            return 0f;
        }
    }
    internal class QueueAttacher
    {
        private readonly ConcurrentQueue<QueueModel> _troops;
        private readonly WaitHandle[] _signal;
        public QueueAttacher(ConcurrentQueue<QueueModel> queue, WaitHandle[] signals)
        {
            _troops = queue;
            _signal = signals;
        }
        /// <summary>
        /// 消息入列方法
        /// </summary>
        /// <param name="t"></param>
        unsafe internal void Add(string n, Guid i, object c, bool s)
        {
            _troops.Enqueue(new QueueModel(n, i, c, s));
            //调度信号
            ((ManualResetEvent)_signal[1]).Set();
            var num = stackalloc int[] { 1, 2 };
            
            
            //int* p = ;
        }
        
    }
}
