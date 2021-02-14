using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class rand
    {
        static Random m_rnd; 
        private rand() {}

        public static int next(int max) => m_rnd.Next(max);

        public static void init(int seed)
        {
            m_rnd = new Random(seed);
        }

    }
}
