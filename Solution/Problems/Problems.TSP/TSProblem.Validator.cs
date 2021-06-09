using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CTSProblem : AProblem
    {
        public override bool Verify(IPermutation obj)
        {
            List<ushort> t = new List<ushort>(obj.ToArray());
            t.Sort();
            if(t[0] != 0 || t[t.Count - 1] != t.Count - 1)
                return false;
            for(int i=0;i<t.Count-1;i++)
            {
                if(t[i] == t[i + 1])
                    return false;
            }
            return true;
        }

        public override IPermutation GetRandomPermutation()
        {
            List<ushort> t = new List<ushort>();
            for(ushort i = 0; i < Size(); i++)
                t.Insert(new System.Random().Next(t.Count), i);
            return new CPermutation(this, t);
        }
    }
}
