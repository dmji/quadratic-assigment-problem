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
            Msg($"sizeQAP={Size()} CPermutation: {target.ToString()}");
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
                        if(Calc(temp) < Calc(minp))
                        {
                            minp = temp.Clone();
                            if(bBreak)
                            {
                                bNestedBreak = true;
                                break;
                            }
                        }
                    }
                    if(bNestedBreak)
                        break;
                }
                Msg($"$Local search step{i}: CPermutation: {minp.ToString()}");
            } while(stepCount != ++i && Calc(pt) != Calc(minp));
            Result = minp.Clone();
        }

        public override IResultAlg Start(IOptions opt)
        {
            ResetDiagnostic();
            Options t = (Options)opt;
            bBreak = t.B_FULLIFY;
            local_search(m_problem.GetRandomPermutation());
            return this;
        }
    }
}