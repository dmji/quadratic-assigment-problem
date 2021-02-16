using System;

namespace Problem
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public abstract partial class AProblem : IProblem
    {
        //LOGGER
        protected Func<string, bool> msg = (string s) => false;
        public void setLogger(Util.Logger log = null)
        {
            msg = log == null ? (string s) => false : log.init();
        }

        //PROBLEM
        ///<summary>n</summary>
		protected ushort m_ProblemSize;

        public ushort size() => m_ProblemSize;

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public virtual long calc(IPermutation src)
        {
            return 0;
        }

        public AProblem(ushort size = 0) {}
    }
}
