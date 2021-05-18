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
            IPermutation pt = new CPermutation(target);
            IPermutation temp = new CPermutation(pt);
            IPermutation minp = new CPermutation(pt);
            double minp_cost = 0;
            int i = 0;
            
            msg($"sizeQAP={size()} CPermutation: {target.ToString()} Q={target.Cost()}");
            do
            {
                pt = new CPermutation(minp);
                for(int u = 0; u < pt.Size() - 1; u++)
                {
                    for(int y = u + 1; y < pt.Size(); y++)
                    {
                        temp = new CPermutation(pt);
                        temp.Swap(y, u);
                        if(temp.Cost() < minp_cost)
                        {
                            minp = new CPermutation(temp);
                            minp_cost = minp.Cost();
                        }
                    }
                }
                msg($"$Local search step{i}: CPermutation: {minp.ToString()} Q={minp.Cost()}");
            } while(stepCount != ++i && pt.Cost() != minp.Cost());
        }

        public override IDiagnostic Start(IOptions opt)
        {
            ResetDiagnostic();
            var perm = new List<ushort>();
            for(ushort i = 0; i < size(); i++)
                perm.Add(i);
            local_search(new CPermutation(m_q.Calc, perm));
            return this;
        }
    }
}