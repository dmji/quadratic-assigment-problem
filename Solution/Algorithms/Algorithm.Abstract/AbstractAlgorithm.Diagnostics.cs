using System;
using System.Collections.Generic;

namespace Solution
{
    public abstract partial class AAlgorithm
    {
        public long GetResultValue() => m_result.Cost();

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
            if(m_result != null)
                m_result = null;
            m_calculationCounter = 0;
        }
        public long GetCalcCount() => m_calculationCounter;
        
    }
}
