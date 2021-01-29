using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in evalution algorithm).</summary>
    public interface IProblem
    {
        ulong calc(IPermutation src);
        ushort size();
    }
}
