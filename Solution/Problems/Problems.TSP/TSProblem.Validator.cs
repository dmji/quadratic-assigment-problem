using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public partial class CTSProblem : AProblem
    {
        public bool isValid(List<ushort> obj)
        {
            return true;
            if(Size() != obj.Count)
                return false;
            for(ushort i = 0; i < obj.Count/* - 1*/; i++)
            {
                if(obj[i] == i || GetDist(i, obj[i]) <=0)
                    return false;
            }
            return true;
        }
        public override bool isValid(IPermutation obj) => isValid(obj.ToArray());

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
            if(!isValid(perm) || bWrongPerm)
                Repair(perm);
            return perm;
        }

        public override bool Repair(IPermutation obj)
        {
            List<int> aProblems = new List<int>();
            for(int i = 0; i < obj.Size(); i++)
            {
                if(obj[i] == i || GetDist(i, obj[i]) <= 0)
                    aProblems.Add(i);
            }

            bool bOK = false;
            if(aProblems.Count == 1)
            {
                int index = aProblems[0], isolve = -1;
                for(int i = 0; i < Size(); i++)
                {
                    if(i != index)
                    {
                        if(GetDist(i, obj[index]) > 0 && GetDist(index, obj[i]) > 0 && obj[i] != index)
                        {
                            bOK = true;
                            if(isolve == -1 || CalcedSwap(obj, index, i) < CalcedSwap(obj, index, isolve))
                                isolve = i;
                        }
                    }
                }
                if(bOK)
                {
                    obj.Swap(index, isolve);
                    return true;
                }
            }
            else
            {
                bOK = true;
                while(aProblems.Count > 0)
                {
                    bool bOkLocal = false;
                    bool bProblemIndex = false;
                    int index = aProblems[0], isolve = -1;
                    for(int i = 0; i < Size(); i++)
                    {
                        if(i != index)
                        {
                            if(GetDist(i, obj[index]) > 0 && GetDist(index, obj[i]) > 0 && obj[i] != index)
                            {
                                bOkLocal = true;
                                if(isolve == -1 || CalcedSwap(obj, index, i) < CalcedSwap(obj, index, isolve))
                                    isolve = i;
                                if(aProblems.Contains(i))
                                {
                                    isolve = i;
                                    bProblemIndex = true;
                                    break;
                                }
                            }
                        }
                    }
                    if(bOkLocal)
                    {
                        obj.Swap(index, isolve);
                        aProblems.RemoveAt(0);
                        if(bProblemIndex)
                            aProblems.Remove(isolve);
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
