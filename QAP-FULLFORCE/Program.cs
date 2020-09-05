using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QAPenviron;

namespace AlgorithmsBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QAP_FULLFORCE ss;
            Info s;
            s = new Info("..\\..\\..\\..\\QAP-CONTEST\\contest\\ex1.txt");
            ss = new QAP_FULLFORCE(s.calculate, s.problem_size);
            ss.Start();
            Console.WriteLine(ss.ToStr());
            ss.StartMT();
            Console.WriteLine(ss.ToStr());
        }
    }
}