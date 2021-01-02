using System;
using QAPenviron;

namespace QAP_EVALRANDOM
{
    class Program
    {
        static void Main(string[] args)
        {
            Info problem = new Info("..\\..\\..\\..\\QAP-CONTEST\\contest\\ex1.dat");
            QAP_EVALRANDOM prog = new QAP_EVALRANDOM(problem.calculate, problem.problem_size);
            prog.Start(1000000,0.1);
            Console.WriteLine(prog.ToStr());
        }
    }
}
