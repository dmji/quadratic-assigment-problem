using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CTSProblem : AProblem
    {
        public int isValid(List<ushort> obj)
        {
            List<int> aProblems = new List<int>();
            for(ushort i = 1; i < obj.Count; i++)
            {
                if(GetDist(obj[i - 1], obj[i]) <= 0)
                    aProblems.Add(i);
            }
            if(GetDist(obj[obj.Count - 1], obj[0]) <= 0)
                aProblems.Add(0);
            return aProblems.Count;
        }
        public override int isValid(IPermutation obj) => isValid(obj.ToArray());

        public override IPermutation GetRandomPermutation()
        {
            List<ushort> t = new List<ushort>();
            List<ushort> pool = new List<ushort>();
            for(ushort i = 0, n = Size(); i < n; i++)
            {
                pool.Add(i);
                t.Add(n);
            }
            bool bWrongPerm = false;
            for(ushort i = 0, n = Size(); i < n; i++)
            {
                ushort val = n;
                int nRand = 0;
                do
                {
                    nRand++;
                    if(nRand > n)
                    {
                        bWrongPerm = true;
                        break;
                    }
                   val = (ushort)new System.Random().Next(pool.Count);
                } while(pool[val] == i);
                t[i] = pool[val];
                pool.RemoveAt(val);
            }
            var perm = new CPermutation(this, t);
            if(isValid(perm) > 0 || bWrongPerm)
                Repair(perm);
            return perm;
        }

        public override bool Repair(IPermutation obj)
        {
            List<int> aProblems = new List<int>();
            for(ushort i = 1; i < obj.Size(); i++)
            {
                if(GetDist(obj[i - 1], obj[i]) <= 0)
                    aProblems.Add(i);
            }
            if(GetDist(obj[obj.Size() - 1], obj[0]) <= 0)
                aProblems.Add(0);

            bool bOK = false;
            if(aProblems.Count == 1)
            {
                int index = aProblems[0];
                for(int i = 0; i < Size(); i++)
                {
                    if(i != index)
                    {
                        if(obj.Swap(index, i) == 0)
                            return true;
                        obj.Swap(index, i);
                    }
                }
            }
            else
            {
                bOK = true;
                while(aProblems.Count > 0)
                {
                    bool bOkLocal = false;
                    int index = aProblems[0], isolve = -1;
                    for(int i = 0; i < Size(); i++)
                    {
                        if(i != index)
                        {
                            bOkLocal = true;
                            if(obj.Swap(i, index) < aProblems.Count)
                            {
                                isolve = i;
                                if(aProblems.Contains(i))
                                {
                                    aProblems.Remove(isolve);
                                    obj.Swap(i, index);
                                    break;
                                }
                            }
                            obj.Swap(i, index);
                        }
                    }
                    if(bOkLocal)
                    {
                        obj.Swap(index, isolve);
                        aProblems.RemoveAt(0);
                    }
                    else
                    {
                        bOK = false;
                        break;
                    }
                }
            }

            if(!bOK)
                obj = GetRandomPermutation();
            return true;
        }
    }
}
