using System;
using System.Collections.Generic;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IPermutation : IEquatable<object>, IToString
    {
        /// <summary> Get permutation as ushort array </summary>
        List<ushort> ToArray();
        /// <summary>index operator</summary>
        ushort this[int i] { get; set; }
        /// <summary>return current permutation size</summary>
        int Size();
        /// <summary>Get. One-line permutation w/ spaces </summary>
        IPermutation Clone();
        long Cost();
        void Swap(int i1, int i2);
        void Repair();
    }

    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public partial class CPermutation : IPermutation
    {
        protected IProblem m_problem = null;
        ///<summary>permutation</summary>
        ushort[] m_p;
        long m_c;
        bool m_bCalced = false;

        protected void OnEdit() { m_bCalced = false; }
        public long Cost()
        {
            if(m_bCalced == true && m_c < 0)
                m_bCalced = false;
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
        public ushort this[int i]
        {
            get => m_p[i];
            set { OnEdit(); m_p[i] = value; }
        }
        /// <summary> Get permutation as ushort array </summary>
        public List<ushort> ToArray() => new List<ushort>(m_p); // ??
        /// <summary>Get. One-line permutation w/ spaces </summary>
        public override string ToString()
        {
            string result = "";
            int i = 0;
            for(; i < m_p.Length - 1; i++)
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

        public void Repair()
        {
            m_problem.Repair(this);
            OnEdit();
        }
    }
}
