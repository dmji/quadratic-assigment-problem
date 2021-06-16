using System.Collections.Generic;

namespace Solution
{
    public partial class CLocalSearchAlgorithm : AAlgorithm
    {
        public override string Name() => "LocalSearchBased";
        public static string Name(bool b) => "LocalSearchBased";

        public CLocalSearchAlgorithm(IProblem problem) : base(problem) { }

        public bool bBreak;

        /// <summary>get solve with local search (2-swap method)</summary>
        /// <param name="p">start permutation</param>
        /// <returns>local optimal solution</returns>
        public void local_search(IPermutation target, int stepCount = -1)
        {
            Calc(target);
            Msg($"size={Size()} CPermutation: {target.Cost()}");
            IPermutation pt = target.Clone();
            IPermutation minp = pt.Clone();
            int i = 0;
            do
            {
                bool bNestedBreak = false;
                pt = minp.Clone();
                for(int u = 0; u < pt.Size() - 1; u++)
                {
                    for(int y = u + 1; y < pt.Size(); y++)
                    {
                        IPermutation temp = pt.Clone();
                        temp.Swap(y, u);
                        if(m_problem.isValid(temp) == 0)
                        {
                            if(m_problem.PermutationComparision(temp, minp) == 1)
                            {
                                minp = temp.Clone();
                                if(bBreak)
                                {
                                    bNestedBreak = true;
                                    break;
                                }
                            }
                        }
                    }
                    if(bNestedBreak)
                        break;
                }
                Msg($"$Local search step{i}: CPermutation: {minp.Cost()}");
            } while(stepCount != ++i && Calc(pt) != Calc(minp));
            Result = minp.Clone();
        }

        public override void Start(IOptions opt)
        {
            ResetDiagnostic();
            Options t = (Options)opt;
            bBreak = t.B_FULLIFY;
            if(t.m_p != null)
                local_search(t.m_p);
            else
                local_search(m_problem.GetRandomPermutation());
        }
    }
}