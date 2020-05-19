using System;
using System.Collections.Generic;
using QAP;

namespace QAP_LSA
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 10, g_omega = 9, g_z = 3;
            int generate = 1;
            info data = new info("ex_30 1 0.txt");
            individ p = null;
            if (generate == 0)
            {
                p = data.test_generator(size, g_omega, g_z);
                data.export_txt(p, 9, 3);
            }
            for (int tst = generate; tst < 10; tst++)
            {
                if (tst > 0)
                    p = new individ(data.problem_size);
                p = data.algorithms_local_search(p);
            }
        }
    }
}
