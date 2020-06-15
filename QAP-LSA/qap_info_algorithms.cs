using System;
using System.Collections.Generic;
using System.Text;

namespace QAP
{
    public partial class info
    {
        public individ algorithms_local_search(individ p)
        {
           Individ minp = new individ(p);
           Individ temp = new individ(p);
            Console.WriteLine("sizeQAP=" + problem_size.ToString() + " Q=" + cost(p));
            int i = 0;
            do
            {
                p = new individ(minp);
                for (int u = 0; u < p.size - 1; u++)
                {
                    for (int y = u + 1; y < p.size; y++)
                    {

                        temp = new individ(p);
                        int swap = temp[y];
                        temp[y] = temp[u];
                        temp[u] = swap;
                        if (cost(temp) < cost(minp))
                            minp = new individ(temp);
                    }
                }
                i++;
                Console.WriteLine("$ " + cost(minp));
            } while (cost(p) != cost(minp));
            return minp;
        }
    }
}