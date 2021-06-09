﻿using System;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IPermutation : IEquatable<object>, IToString
    {
        /// <summary> Get permutation as ushort array </summary>
        ushort[] ToArray();
        /// <summary>index operator</summary>
        ushort this[int i] { get; set; }
        /// <summary>return current permutation size</summary>
        int Size();
        /// <summary>Get. One-line permutation w/ spaces </summary>
        IPermutation Clone();
        bool Verify();
        long Cost();
        void Swap(int i1, int i2);
    }

    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public partial class CPermutation : IPermutation
    {
        IProblem m_problem = null;
        ///<summary>permutation</summary>
        ushort[] m_p;
        long m_c;
        bool m_bCalced = false;

        protected void OnEdit() { m_bCalced = false; }
        public long Cost()
        {
            if(m_bCalced == true && m_c < 0)
                throw new Exception("WTF");
            if(!m_bCalced)
            {
                m_c = m_problem.Calc(this);
                m_bCalced = true;
            }
            return m_c;
        }
        /// <summary>return current permutation size</summary>
        public int Size() => m_p.Length;
        /// <summary>index operator</summary>
        public ushort this[int i] { get => m_p[i]; 
                                    set { OnEdit(); m_p[i] = value; } }
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
            return result + (m_c >= 0 ? (" : " + m_c.ToString()) : "");
        }
        public IPermutation Clone() => new CPermutation(this);
        public void Swap(int i1, int i2)
        {
            long val = long.MinValue;
            if(m_bCalced)
                val = m_problem.CalcedSwap(this, i1, i2);
            ushort tmp = m_p[i1];
            m_p[i1] = m_p[i2];
            m_p[i2] = tmp;
            if(val == long.MinValue)
                OnEdit();
            else
                m_c = val;
        }
        public static bool operator ==(CPermutation a, CPermutation b)
        {
            if((object)a == null && (object)b == null)
                return true;
            else if(((object)a == null && (object)b != null) || ((object)a != null && (object)b == null))
                return false;
            else if(a.Size() == b.Size())
            {
                for(int i = 0, n = a.Size(); i < n; i++)
                {
                    if(a[i] != b[i])
                        return false;
                }
                return true;
            }
            else
                return false;
        }
        public static bool operator !=(CPermutation a, CPermutation b) => !(a == b);
        public override bool Equals(object a)
        {
            if(a is CPermutation)
                return this == (CPermutation)a;
            else
                return false;
        }

        public bool Verify() => m_problem.Verify(this);
    }
}
