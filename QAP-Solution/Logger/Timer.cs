using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class Timer
    {
        System.Diagnostics.Stopwatch time;

        public Timer()
        {
            time = new System.Diagnostics.Stopwatch();
            time.Start();
        }

        public string Reset()
        {
            string result = time.ElapsedMilliseconds.ToString();
            time.Restart();
            return result;
        }

        public long Stop()
        {
            time.Stop();
            return time.ElapsedMilliseconds;
        }

        public long StopT()
        {
            time.Stop();
            return time.ElapsedTicks;
        }

        public override string ToString() => time.ToString();
    }
}
