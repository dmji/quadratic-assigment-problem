using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CQAPProblem : AProblem
    {
        public override bool verify(IPermutation obj)
        {
            List<ushort> t = new List<ushort>(obj.ToArray());
            t.Sort();
            for(int i=0;i<t.Count-1;i++)
            {
                if(t[i] == t[i + 1])
                    return false;
            }
            return true;
        }

        public override IPermutation getRandomOne()
        {
            List<ushort> t = new List<ushort>();
            for(ushort i = 0; i < size(); i++)
                t.Insert(new System.Random().Next(t.Count), i);
            return new CPermutation(this.calc, t);
        }
    }
}
