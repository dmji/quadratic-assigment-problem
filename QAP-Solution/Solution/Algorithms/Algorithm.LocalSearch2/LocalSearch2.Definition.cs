using System.Collections.Generic;

namespace Solution
{
    public partial class LocalSearchAlgorithm : AAlgorithm
    {
        public override string getName() => "Fullforce algorithm";

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
            
            msg($"sizeQAP={size()} CPermutation: {target.ToString()} Q={target.cost()}");
            do
            {
                pt = new CPermutation(minp);
                for(int u = 0; u < pt.size() - 1; u++)
                {
                    for(int y = u + 1; y < pt.size(); y++)
                    {
                        temp = new CPermutation(pt);
                        temp.swap(y, u);
                        if(temp.cost() < minp_cost)
                        {
                            minp = new CPermutation(temp);
                            minp_cost = minp.cost();
                        }
                    }
                }
                msg($"$Local search step{i}: CPermutation: {minp.ToString()} Q={minp.cost()}");
            } while(stepCount != ++i && pt.cost() != minp.cost());
        }

        public override IDiagnostic Start(IOptions opt)
        {
            diagReset();
            var perm = new List<ushort>();
            for(ushort i = 0; i < size(); i++)
                perm.Add(i);
            local_search(new CPermutation(m_q.calc, perm));
            return this;
        }
    }
}