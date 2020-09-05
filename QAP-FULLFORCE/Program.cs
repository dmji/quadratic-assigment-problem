using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using QAPenviron;

namespace AlgorithmsBase
{
    public class Program
    {
        public static void Main()
        {
            QAP_FULLFORCE ss = new QAP_FULLFORCE(new Info("..\\..\\..\\..\\QAP-CONTEST\\ex1.txt"));
            ss.Start();
            ss.ShowInConsole();
        }
    }
}