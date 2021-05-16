using System;
using System.Collections.Generic;

namespace Solution
{
    public interface IDiagnostic
    {
        void setLogger(TestSystem.ILogger log);
        long getCalcCount();
        long getResultValue();
    }

    public abstract partial class AAlgorithm : IAlgorithm, IDiagnostic
    {
        public void setLogger(TestSystem.ILogger log = null) { msg = log == null ? ((string s) => false) : log.init(); }
        public override string ToString()
        {
            string log = getName() + " algorithm.\n";
            if(m_bFinish)
            {
                log += $"Finished with {m_calculationCounter} calculations. Final cost: {m_q.calc(m_p[0]).ToString()}\n";
                foreach(IPermutation a in m_p)
                    log += a.ToString() + '\n';
            }
            else
                log = ("Not yet started!");
            return log;
        }
        public long getCalcCount() => m_calculationCounter;
        public long getResultValue() => m_p[0].cost();

        protected void diagReset()
        {
            m_bFinish = false;
            if(m_p != null)
                m_p.Clear();
            else
                m_p = new List<IPermutation>();

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
        protected void START_TIMER() { m_timer.Start(); }
        protected void STOP_TIMER() { m_timer.Stop(); }

        //func diagnostic counter
        protected long calc(IPermutation obj)
        {
            lock(m_timer)
                m_calculationCounter++;
            return obj.cost();
        }

        //LOGGER
        protected Func<string, bool> msg = (string s) => false;

        //DIAGNOSTIC TOOLS
        System.Diagnostics.Stopwatch m_timer;
        long m_calculationCounter;
    }
}
