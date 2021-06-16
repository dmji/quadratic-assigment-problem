using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CQAProblem : AProblem
    {
        public override int isValid(IPermutation obj)
        {
            List<ushort> aProblem = new List<ushort>();
            List<ushort> t = obj.ToArray();
            t.Sort();
            if(t[0] != 0 || t[t.Count - 1] != t.Count - 1)
                return obj.Size();
            for(int i=0;i<t.Count-1;i++)
            {
                if(t[i] == t[i + 1])
                    aProblem.Add((ushort)i);
            }
            return aProblem.Count;
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
