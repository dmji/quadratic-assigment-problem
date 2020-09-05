using System;
using QAPenviron;

namespace AlgorithmsBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Info problem = new Info("..\\..\\..\\..\\QAP-CONTEST\\contest\\ex1.txt");
            Evalution prog = new Evalution(problem.calculate,problem.problem_size);
            prog.Start(20,0.1);
            Console.WriteLine(prog.ToStr());
        }
    }
}
