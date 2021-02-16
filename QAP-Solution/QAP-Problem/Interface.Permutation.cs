using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in evalution algorithm).</summary>
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
}
