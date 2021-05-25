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
        ///<summary>n</summary>
		ushort m_problemSize;
        public ushort Size() => m_problemSize;

        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        public virtual long Calc(IPermutation src) => 0;
        public virtual bool Verify(IPermutation obj) => false;
        public virtual IPermutation GetRandomPermutation() => null;

        protected AProblem(ushort size = 0) { m_log = new CEmptyLogger(); Init(size); }
        protected virtual void Init(ushort size = 0) { m_problemSize = size; }
        
    }
}
