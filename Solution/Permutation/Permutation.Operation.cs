using System;
using System.Collections.Generic;

namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public partial class CPermutation : IPermutation
    {
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
    }
}
