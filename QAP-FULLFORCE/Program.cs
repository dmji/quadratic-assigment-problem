using System;
using QAPenviron;
using System.Collections.Generic;

namespace QAP_FULLFORCE
{
    class Program
    {
        static void Main(string[] args)
        {
            Info ff = new Info("ex1.txt");
            ff.DEBUG_CONSOLE_OUT = 0;
            List<Individ> ss = ff.FullForce();
            ss[0].console_print();
            Console.WriteLine("Q=" + ff.cost(ss[0]));
        }
    }
}
