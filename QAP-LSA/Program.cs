using System;
using System.Collections.Generic;
using QAPenviron;

namespace QAP_LSA
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 10, g_omega = 9, g_z = 3;
            int generate = 1;
            Info data = new Info("ex_30 1 0.txt");
            Individ p = null
            if (generate == 0)
            {
                p = data.PalubetskisTestGeneration(size, g_omega, g_z);
                data.export_txt(p, 9, 3);
            }
            for (int tst = generate; tst < 10; tst++)
            {
                if (tst > 0)
                    p = new Individ(data.problem_size);
                p = data.solve_local_search(p);
            }
        }
    }
}
