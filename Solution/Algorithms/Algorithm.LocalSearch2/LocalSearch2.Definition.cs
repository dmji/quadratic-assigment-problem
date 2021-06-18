using System.Collections.Generic;

namespace Solution
{
    public partial class CLocalSearchAlgorithm : AAlgorithm
    {
        public override string Name() => "LocalSearchBased";
        public static string Name(bool b) => "LocalSearchBased";

        public CLocalSearchAlgorithm(IProblem problem) : base(problem) { }

        /// <summary>get solve with local search (2-swap method)</summary>
        /// <param name="p">start permutation</param>
        /// <returns>local optimal solution</returns>
        public void Alg(IPermutation target, bool bBreak, int stepCount = -1)
        {
            Calc(target);
            if(m_log != null)
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
                        if(temp.Swap(y, u) == 0)
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
                if(m_log != null)
                    Msg($"$Local search step{i}: CPermutation: {minp.Cost()}");
            } while(stepCount != ++i && Calc(pt) != Calc(minp));
            Result = minp.Clone();
        }

        public override void Start(IOptions opt)
        {
            ResetDiagnostic();
            Options t = opt is Options ? (Options)opt : null;

            if(t != null)
            {
                if(t.m_p == null)
                    t.m_p = m_problem.GetRandomPermutation();
                Alg(t.m_p, t.B_FULLIFY);
            }
            else
                Alg(m_problem.GetRandomPermutation(), t.B_FULLIFY);
        }
    }
}