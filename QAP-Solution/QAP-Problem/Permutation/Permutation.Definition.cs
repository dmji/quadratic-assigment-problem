using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in evalution algorithm).</summary>
    public partial class CPermutation : IPermutation
    {
        public static Func<CPermutation, ulong> calc;
        ///<summary>permutation</summary>
        ushort[] m_p;
        ulong m_c;
        bool m_bUncalc = true;


        public ulong cost()
        {
            if(m_bUncalc)
                m_c = calc(this);
            m_bUncalc = false;
            return m_c;
        }

        /// <summary>return current permutation size</summary>
        public int size() => m_p.Length;

        /// <summary>index operator</summary>
        public ushort this[int i] { get => m_p[i]; set { m_bUncalc = true; m_p[i] = value; } }

		public bool Equals(IPermutation other)
        {
            if(this.GetType() != other.GetType() || other == null)
                return false;
            for(int i = 0; i < m_p.Length; i++)
            {
                if(m_p[i] != other[i])
                    return false;
            }
            return true;
        }

        /// <summary> Get permutation as ushort array </summary>
        public ushort[] ToArray() => m_p/*.Clone()*/; // ??

        /// <summary>Get. One-line permutation w/ spaces </summary>
        public override string ToString()
        {
            string result = "";
            int i = 0;
            for(; i < m_p.Length-1; i++)
                result = result + m_p[i] + " ";
            result = result + m_p[i];
            return result + " : " + cost();
        }

        public IPermutation Clone() => new CPermutation(this);

        public void swap(int i1, int i2)
        {
            ushort tmp = m_p[i1];
            m_p[i1] = m_p[i2];
            m_p[i2] = tmp;
        }

    }
}
