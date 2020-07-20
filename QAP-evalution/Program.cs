using System;

namespace QAPenviron
{
    class Program
    {
        static void Main(string[] args)
        {
            Info problem = new Info("..\\..\\..\\..\\QAP-CONTEST\\ex1.txt");
            Evalution prog = new Evalution(problem);
            prog.Start(100,0.1);
        }
    }
}
