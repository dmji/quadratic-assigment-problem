using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace QAPenviron
{
    public partial class Info
    {
        /// <summary>
        /// get solve with local search (2-swap method)
        /// </summary>
        /// <param name="p">start permutation</param>
        /// <returns>local optimal solution</returns>
        public Individ solve_local_search(Individ p)
        {
            Individ minp = new Individ(p);
            Individ temp = new Individ(p);
            if(DEBUG_CONSOLE_OUT==1) Console.WriteLine("sizeQAP=" + problem_size.ToString() + " Q=" + cost(p));
            int i = 0;
            do
            {
                p = new Individ(minp);
                for (int u = 0; u < p.size - 1; u++)
                {
                    for (int y = u + 1; y < p.size; y++)
                    {

                        temp = new Individ(p);
                        int swap = temp[y];
                        temp[y] = temp[u];
                        temp[u] = swap;
                        if (cost(temp) < cost(minp))
                            minp = new Individ(temp);
                    }
                }
                i++;
                if (DEBUG_CONSOLE_OUT == 1) Console.WriteLine("$ " + cost(minp));
            } while (cost(p) != cost(minp));
            return minp;
        }
    }
}
