using System;
using System.Collections.Generic;

namespace Solution
{
    public interface IResultAlg
    {
        long GetCalcCount();
        long GetResultValue();
    }

    public abstract partial class AAlgorithm : IResultAlg
    {
        public long GetResultValue() => m_results[0].Cost();

        //func diagnostic counter
        protected long Calc(IPermutation obj)
        {
            m_calculationCounter++;
            return obj.Cost();
        }

        //DIAGNOSTIC TOOLS
        long m_calculationCounter;

        protected void ResetDiagnostic()
        {
            m_bFinish = false;
            if(m_results != null)
                m_results.Clear();
            else
                m_results = new List<IPermutation>();

            m_calculationCounter = 0;
            if(m_results == null)
                m_results = new List<IPermutation>();
            else
                m_results.Clear();
        }
        public long GetCalcCount() => m_calculationCounter;
        
    }
}
