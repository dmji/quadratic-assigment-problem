using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using QAPenviron;

namespace QAPenviron
{
    public class Program
    {
        public static void Main(string[] args)
        {
            QAP_FULLFORCE ss;
            if(args.Length >0)
                ss= new QAP_FULLFORCE(new Info(args[0]));
            else
                ss= new QAP_FULLFORCE(new Info("..\\..\\..\\..\\QAP-CONTEST\\ex1.txt"));
            ss.Start();
            ss.ShowInConsole();
            ss.StartMT();
            ss.ShowInConsole();
        }
    }
}