using System;
using System.Collections.Generic;


namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public partial class CPermutation
    {
        ///<summary>Construct permutation from exist one</summary>
        public CPermutation(IPermutation src) : this((CPermutation)src) {}
        public CPermutation(CPermutation src) : this(src.m_calc, src.m_p) {}

        ///<summary>Construct permutation from list</summary>
        public CPermutation(Func<IPermutation, long> calc, ICollection<ushort> src)
        {
            OnEdit();
            m_p = new ushort[src.Count];
            src.CopyTo(m_p, 0);
            m_calc = calc;
        }

        ///<summary>Construct corrupted permutation, <c>count</c> is problem size, <c>fill</c> is int in all slots </summary>
        public CPermutation(Func<IPermutation, long> calc, ushort count, ushort filler)
        {
            OnEdit();
            m_p = new ushort[count];
            for(int i = 0; i < count; i++)
                m_p[i]=filler;
            m_calc = calc;
        }
    }
}
