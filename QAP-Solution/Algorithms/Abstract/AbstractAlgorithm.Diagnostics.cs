using System;
using System.Collections.Generic;
using Problem;

namespace Algorithms
{
    public abstract partial class Algorithm
    {
        //LOGGER
        protected Func<string, bool> msg = (string s) => false;
        public void setLogger(Util.Logger log = null)
        {
            msg = log == null ? (string s) => false : log.init();
        }

        //DIAGNOSTIC TOOLS
        System.Diagnostics.Stopwatch m_timer;
        ulong m_calculationCounter;
        ulong m_stepPredict;

        protected void START_TIMER() { m_timer.Start(); }
        protected void STOP_TIMER() { m_timer.Stop(); }

        //func diagnostic counter
        protected ulong calc(IPermutation obj)
        {
            lock(m_timer)
                m_calculationCounter++;
            return m_q.calc(obj);
        }

        public override string ToString()
        {
            string log = getName() + " algorithm.\n";
            if(m_bFinish)
            {
                log += $"Finished with {m_calculationCounter} calculations. Final cost: {m_q.calc(m_p[0]).ToString()}";
                foreach(List<int> a in m_p)
                    log += a.ToString() + '\n';
            }
            else if(m_p.Count != 0)
            {
                if(m_stepPredict != 0)
                    log = log + "Step to do: " + m_stepPredict.ToString();
                log += $"\nIn-work time: {m_timer.ElapsedMilliseconds.ToString()}\n";
                log += $"\nCalculated: {m_calculationCounter.ToString()} Current cost: {m_q.calc(m_p[0]).ToString()}\n";
            }
            else
                log = ("Not yet started!");
            return log;
        }

        public string strCalcCount() => m_calculationCounter.ToString();
        public string strResultValue() => m_q.calc(m_p[0]).ToString();

        protected void diagReset()
        {
            m_calculationCounter = 0;
            if(m_p==null)
                m_p = new List<IPermutation>();
            else
                m_p.Clear();
            if(m_timer == null)
                m_timer = new System.Diagnostics.Stopwatch();
            else
                m_timer.Reset();
        }
    }
}
