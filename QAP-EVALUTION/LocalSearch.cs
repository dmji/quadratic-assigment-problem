using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution : Algorithms
    {
        public List<int> local_search(List<int> src,int stepCount = -1, int DEBUG_CONSOLE_OUT = 0)
        {
            List<int> pt = new List<int>(src);
            List<int> temp = new List<int>(pt);
            List<int> minp = new List<int>(pt);
            double minp_cost = calculate(src);
            int i = 0;
            if (DEBUG_CONSOLE_OUT == 1) Console.WriteLine($"\nsizeQAP={problem_size} Individ: {this.ToStr()} Q={calculate(pt)}");
            do
            {
                pt = new List<int>(minp);
                for (int u = 0; u < pt.Count - 1; u++)
                    for (int y = u + 1; y < pt.Count; y++)
                    {
                        temp = new List<int>(pt);
                        int swap = temp[y];
                        temp[y] = temp[u];
                        temp[u] = swap;
                        if (calculate(temp) < minp_cost)
                        {
                            minp = new List<int>(temp);
                            minp_cost = calculate(minp);
                        }
                    }
                if (DEBUG_CONSOLE_OUT == 1) Console.WriteLine($"$Local search step{i}: Individ: {getPermutation(minp)} Q={calculate(minp)}");
            } while (stepCount != ++i && calculate(pt) != calculate(minp));
            return minp;
        }
    }
}
