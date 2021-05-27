using System;
using System.Collections.Generic;


namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public partial class CPermutation
    {
        ///<summary>Construct permutation from exist one</summary>
        public CPermutation(IPermutation src) : this((CPermutation)src) {}
        public CPermutation(CPermutation src) : this(src.m_problem, src.m_p) { 
            m_bCalced = src.m_bCalced; m_c = src.m_c; 
        }

        ///<summary>Construct permutation from list</summary>
        public CPermutation(IProblem problem, ICollection<ushort> src)
        {
            OnEdit();
            m_p = new ushort[src.Count];
            src.CopyTo(m_p, 0);
            m_problem = problem;
        }

        ///<summary>Construct corrupted permutation, <c>count</c> is problem size, <c>fill</c> is int in all slots </summary>
        public CPermutation(IProblem problem, ushort count, ushort filler)
        {
            OnEdit();
            m_p = new ushort[count];
            for(int i = 0; i < count; i++)
                m_p[i]=filler;
            m_problem = problem;
        }
    }
}
