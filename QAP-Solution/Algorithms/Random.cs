using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class rand
    {
        static Random m_rnd; 
        static long m_c;
        static int m_seed;
        private rand() {}

        public static int next(int max)
        {
            if(m_c >= 100000000)
                init(m_seed);
            m_c++;
            return m_rnd.Next(max);
        }
        public static void init(int seed)
        {
            if(seed > 0)
                m_rnd = new Random(seed);
            else
                m_rnd = new Random();
            m_seed = seed;
            m_c = 0;
        }

    }
}
