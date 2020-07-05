using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Individ
    {
        /// <summary>
        /// premutation console one-line out
        /// </summary>
        public void console_print(int sign = -1)
        {
            for (int i = 0; i < p.Count; i++)
                Console.Write(p[i] + " ");
            if(sign==-1) Console.Write('\n');
        }
    }
}
