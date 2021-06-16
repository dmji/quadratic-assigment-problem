using System;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IProblem : ISetLogger, ISerialize
    {
        long Calc(IPermutation src);
        long CalcedSwap(IPermutation src, int ix, int iy);
        ushort Size();

        bool isValid(IPermutation obj);
        bool Repair(IPermutation obj);

        IPermutation GetRandomPermutation();
        int PermutationComparision(IPermutation x, IPermutation y);
    }

    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public abstract partial class AProblem : IProblem
    {
        ///<summary>n</summary>
		ushort m_problemSize;
        public ushort Size() => m_problemSize;
        
        public virtual void Serialize(string src) { }
        public virtual bool Deserialize(string src) => false;
        
        /// <summary>calculate criterion</summary>
        /// <param name="CPermutationSrc">premutation to calculate</param>
        /// <returns>double value</returns>
        
        public virtual long Calc(IPermutation src) => 0;
        public virtual long CalcedSwap(IPermutation src, int ix, int iy) => long.MinValue;

        public virtual bool isValid(IPermutation obj) => true;
        public virtual bool Repair(IPermutation obj) => false;
        public virtual IPermutation GetRandomPermutation() => null;

        protected AProblem(ushort size = 0) { m_log = new CEmptyLogger(); Init(size); }
        protected virtual void Init(ushort size = 0) { m_problemSize = size; }
    }
}
