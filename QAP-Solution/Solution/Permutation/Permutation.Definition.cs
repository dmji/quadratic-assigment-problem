using System;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IPermutation : IEquatable<object>
    {
        /// <summary> Get permutation as ushort array </summary>
        ushort[] ToArray();
        /// <summary>index operator</summary>
        ushort this[int i] { get; set; }
        /// <summary>return current permutation size</summary>
        int size();
        /// <summary>Get. One-line permutation w/ spaces </summary>
        string ToString();
        IPermutation Clone();
        long cost();
        void swap(int i1, int i2);
    }

    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public partial class CPermutation : IPermutation
    {
        Func<IPermutation, long> m_calc = null;
        ///<summary>permutation</summary>
        ushort[] m_p;
        long m_c;
        bool m_bCalced = false;

        protected void OnEdit() { m_bCalced = false; }
        public long cost()
        {
            if(!m_bCalced)
            {
                m_c = m_calc(this);
                m_bCalced = true;
            }
            return m_c;
        }
        /// <summary>return current permutation size</summary>
        public int size() => m_p.Length;
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
        public void swap(int i1, int i2)
        {
            ushort tmp = m_p[i1];
            m_p[i1] = m_p[i2];
            m_p[i2] = tmp;
            //TODO: add recalc after swap
            OnEdit();
        }
        public override int GetHashCode() => m_p.GetHashCode();
        public static bool operator ==(CPermutation a, CPermutation b)
        {
            if((object)a == null && (object)b == null)
                return true;
            else if(((object)a == null && (object)b != null) || ((object)a != null && (object)b == null))
                return false;
            else if(a.size() == b.size())
            {
                for(int i = 0, n = a.size(); i < n; i++)
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
    }
}
