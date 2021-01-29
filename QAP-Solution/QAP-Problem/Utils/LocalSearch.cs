using System;

namespace Problem
{
    public partial class Utils
    {
        /// <summary>get solve with local search (2-swap method)</summary>
        /// <param name="p">start permutation</param>
        /// <returns>local optimal solution</returns>
        public static CPermutation local_search(Func<string, bool> msg, IProblem problem, IPermutation target, int stepCount = -1)
        {
            CPermutation pt = new CPermutation((CPermutation)target);
            CPermutation temp = new CPermutation(pt);
            CPermutation minp = new CPermutation(pt);
            double minp_cost = 0;
            int i = 0;
            msg($"sizeQAP={problem.size()} CPermutation: {target.ToString()} Q={problem.calc(pt)}");
            do {
                pt = new CPermutation(minp);
                for(int u = 0; u < pt.size() - 1; u++)
                    for(int y = u + 1; y < pt.size(); y++)
                    {
                        temp = new CPermutation(pt);
                        temp.swap(y, u);
                        if(problem.calc(temp) < minp_cost)
                        {
                            minp = new CPermutation(temp);
                            minp_cost = problem.calc(minp);
                        }
                    }
               msg($"$Local search step{i}: CPermutation: {minp.ToString()} Q={problem.calc(minp)}");
            } while(stepCount != ++i && problem.calc(pt) != problem.calc(minp));

            return minp;
        }
    }
}
