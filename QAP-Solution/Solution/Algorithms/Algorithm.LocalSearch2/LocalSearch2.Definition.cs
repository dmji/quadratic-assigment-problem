using System.Collections.Generic;

namespace Solution
{
    public partial class LocalSearchAlgorithm : AAlgorithm
    {
        public override string Name() => "Fullforce algorithm";

        public LocalSearchAlgorithm(IProblem problem) : base(problem) { }

        /// <summary>get solve with local search (2-swap method)</summary>
        /// <param name="p">start permutation</param>
        /// <returns>local optimal solution</returns>
        public void local_search(IPermutation target, int stepCount = -1)
        {
            IPermutation pt = target.Clone();
            IPermutation temp = pt.Clone();
            IPermutation minp = pt.Clone();
            double minp_cost = 0;
            int i = 0;
            
            Msg($"sizeQAP={Size()} CPermutation: {target.ToString()} Q={target.Cost()}");
            do
            {
                pt = minp.Clone();
                for(int u = 0; u < pt.Size() - 1; u++)
                {
                    for(int y = u + 1; y < pt.Size(); y++)
                    {
                        temp = pt.Clone();
                        temp.Swap(y, u);
                        if(temp.Cost() < minp_cost)
                        {
                            minp = temp.Clone();
                            minp_cost = minp.Cost();
                        }
                    }
                }
                Msg($"$Local search step{i}: CPermutation: {minp.ToString()} Q={minp.Cost()}");
            } while(stepCount != ++i && pt.Cost() != minp.Cost());
        }

        public override IResultAlg Start(IOptions opt)
        {
            ResetDiagnostic();
            local_search(m_problem.GetRandomPermutation());
            return this;
        }
    }
}