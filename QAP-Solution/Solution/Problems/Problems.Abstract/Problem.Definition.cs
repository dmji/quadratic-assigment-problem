using System;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IProblem
    {
        long calc(IPermutation src);
        ushort size();
        bool verify(IPermutation obj);
        IPermutation getRandomOne();
    }

    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public abstract partial class AProblem : IProblem
    {
        //LOGGER
        protected Func<string, bool> msg = (string s) => false;
        public void setLogger(TestSystem.ILogger log = null) { msg = log == null ? (string s) => false : log.init(); }

        //PROBLEM
        ///<summary>n</summary>
		ushort m_ProblemSize;

        public ushort size() => m_ProblemSize;

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public virtual long calc(IPermutation src) => 0;
        public virtual bool verify(IPermutation obj) => false;
        public virtual IPermutation getRandomOne() => null;

        protected AProblem(ushort size = 0) { init(size); }
        protected virtual void init(ushort size = 0) { m_ProblemSize = size; }
        
    }
}
