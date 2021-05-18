﻿namespace TestSystem
{
    public class CTimer
    {
        System.Diagnostics.Stopwatch m_time;

        public CTimer()
        {
            m_time = new System.Diagnostics.Stopwatch();
            m_time.Start();
        }

        public string Reset()
        {
            string s = m_time.ElapsedMilliseconds.ToString();
            m_time.Restart();
            return s;
        }

        public long Stop()
        {
            m_time.Stop();
            return m_time.ElapsedMilliseconds;
        }

        public long StopT()
        {
            m_time.Stop();
            return m_time.ElapsedTicks;
        }

        public override string ToString() => m_time.ToString();
    }
}
