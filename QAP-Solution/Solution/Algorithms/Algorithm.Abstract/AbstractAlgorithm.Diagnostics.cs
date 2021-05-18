using System;
using System.Collections.Generic;

namespace Solution
{
    public interface IDiagnostic
    {
        void SetLogger(TestSystem.ILogger log);
        long GetCalcCount();
        long GetResultValue();
    }

    public abstract partial class AAlgorithm : IAlgorithm, IDiagnostic
    {
        public override string ToString()
        {
            string log = Name() + " algorithm.\n";
            if(m_bFinish)
            {
                log += $"Finished with {m_calculationCounter} calculations. Final cost: {m_q.Calc(m_p[0]).ToString()}\n";
                foreach(IPermutation a in m_p)
                    log += a.ToString() + '\n';
            }
            else
                log = ("Not yet started!");
            return log;
        }
        public long GetResultValue() => m_p[0].Cost();

        //func diagnostic counter
        protected long calc(IPermutation obj)
        {
            lock(m_timer)
                m_calculationCounter++;
            return obj.Cost();
        }

        //DIAGNOSTIC TOOLS
        System.Diagnostics.Stopwatch m_timer;
        long m_calculationCounter;

        protected void ResetDiagnostic()
        {
            m_bFinish = false;
            if(m_p != null)
                m_p.Clear();
            else
                m_p = new List<IPermutation>();

            m_calculationCounter = 0;
            if(m_p == null)
                m_p = new List<IPermutation>();
            else
                m_p.Clear();
            if(m_timer == null)
                m_timer = new System.Diagnostics.Stopwatch();
            else
                m_timer.Reset();
        }
        public long GetCalcCount() => m_calculationCounter;
        protected void START_TIMER() { m_timer.Start(); }
        protected void STOP_TIMER() { m_timer.Stop(); }
        
    }
}
