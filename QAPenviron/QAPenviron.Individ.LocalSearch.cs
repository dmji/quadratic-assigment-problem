using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QAPenviron
{
    public partial class Individ
    {
        /// <summary>get solve with local search (2-swap method)</summary>
        /// <param name="p">start permutation</param>
        /// <returns>local optimal solution</returns>
        public void local_search(Info problem, int stepCount=-1, int DEBUG_CONSOLE_OUT=0)
        {
            Individ pt = new Individ(this);
            Individ temp = new Individ(pt);
            Individ minp = new Individ(pt);
            double minp_cost = 0;
            int i = 0;
            if (DEBUG_CONSOLE_OUT==1) Console.WriteLine($"sizeQAP={problem.problem_size} Individ: {this.ToStr()} Q={problem.calculate(pt)}");
            do
            {
                pt = new Individ(minp);
                for (int u = 0; u < pt.size - 1; u++)
                    for (int y = u + 1; y < pt.size; y++)
                    {
                        temp = new Individ(pt);
                        int swap = temp[y];
                        temp[y] = temp[u];
                        temp[u] = swap;
                        if (problem.calculate(temp) < minp_cost)
                        {
                            minp = new Individ(temp);
                            minp_cost = problem.calculate(minp);
                        }
                    }
                if (DEBUG_CONSOLE_OUT == 1) Console.WriteLine($"$Local search step{i}: Individ: {minp.ToStr()} Q={problem.calculate(minp)}");
            } while (stepCount != ++i && problem.calculate(pt) != problem.calculate(minp));

            p = new List<int>(minp.p);
        }
    }
}
