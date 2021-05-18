using System;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IProblem
    {
        long Calc(IPermutation src);
        ushort Size();
        bool Verify(IPermutation obj);
        IPermutation GetRandomPermutation();
    }

    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public abstract partial class AProblem : IProblem
    {
        //LOGGER
        TestSystem.ILogger m_log;
        public void setLogger(TestSystem.ILogger log = null) { m_log = log == null ? new TestSystem.CEmptyLogger() : log; }
        protected bool msg(string s) => m_log.Msg(s);

        //PROBLEM
        ///<summary>n</summary>
		ushort m_ProblemSize;
        public ushort Size() => m_ProblemSize;

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public virtual long Calc(IPermutation src) => 0;
        public virtual bool Verify(IPermutation obj) => false;
        public virtual IPermutation GetRandomPermutation() => null;

        protected AProblem(ushort size = 0) { init(size); }
        protected virtual void init(ushort size = 0) { m_ProblemSize = size; }
        
    }
}
