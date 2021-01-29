using System;
using System.Collections.Generic;

namespace Problem
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in evalution algorithm).</summary>
    public partial class CPermutation
    {
        ///<summary>Construct permutation from exist one</summary>
        public CPermutation(CPermutation src) : this(src.m_p) {}

        ///<summary>Construct permutation from list</summary>
        public CPermutation(ICollection<ushort> src)
        {
            m_p = new ushort[src.Count];
            src.CopyTo(m_p, 0);
        }

        ///<summary>Construct permutation from array</summary>
        public CPermutation(params ushort[] src)
        {
            m_p = new ushort[src.Length];
            src.CopyTo(m_p, 0);
        }

        ///<summary>Construct random permutation, <c>count</c> is problem size</summary>
        public CPermutation(ushort count = 0)
        {
            m_p = new ushort[count];
            List<ushort> filler = new List<ushort>();
            for(int i = 0; i < count; i++)
                filler.Add(Convert.ToUInt16(i));
            for(int i = 0; i < count; i++)
            {
                int k = new Random().Next(0, filler.Count);
                m_p[i] = filler[k];
                filler.RemoveAt(k);
            }
        }

        ///<summary>Construct corrupted permutation, <c>count</c> is problem size, <c>fill</c> is int in all slots </summary>
        public CPermutation(int count, ushort filler)
        {
            m_p = new ushort[count];
            for(int i = 0; i < count; i++)
                m_p[i]=filler;
        }
    }
}
