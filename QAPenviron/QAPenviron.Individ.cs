using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Individ
    {
        public void console_print()                                       //console out permutation
        {
            for (int i = 0; i < p.Count; i++)
                Console.Write(p[i] + " ");
            Console.WriteLine('\n');
        }
    }
}
