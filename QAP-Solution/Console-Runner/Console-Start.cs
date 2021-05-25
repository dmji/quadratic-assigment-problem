using System;
using TestSystem;

namespace Console_Runner
{
    class Program
    {
        static void Start(int indexAlg, string sPath, int nReply, bool bLogEnable)
        {
            Console.WriteLine($"== Test system start");
            switch(indexAlg)
            {
                case 1:
                    STestStarter.StartTestEvolution(sPath, nReply, bLogEnable);
                    break;
                case 2:
                    STestStarter.StartTestLSA2(sPath, nReply, bLogEnable);
                    break;
                case 3:
                    STestStarter.StartTestFullforce(sPath);
                    break;
                default:
                    Console.WriteLine("Error algorithm type");
                    break;
            }
            Console.WriteLine($"== Test system end");
        }

        static void Main(string[] args)
        {
            int nPaths = 1;
            string sPath;
            if(args.Length > 0)
            {
                sPath = args[0];
                if(sPath.Length == 16)
                {
                    nPaths = 16;
                }
            }
            else
            {
                Console.WriteLine("Enter test-option path:");
                sPath = Console.ReadLine();
            }

            Console.WriteLine($"== Test system load with option: {sPath}");
            int indexAlg = -1;
            string[] alg = { "Undefine", "Evalution", "Local Search", "Full force" };
            if(args.Length > 1)
            {
                if(args[1].StartsWith("Eva") || args[1].StartsWith("EVA"))
                    indexAlg = 1;
                else if(args[1].StartsWith("LSA"))
                    indexAlg = 2;
                else if(args[1].StartsWith("F"))
                    indexAlg = 3;
            }
            else
            {
                Console.WriteLine($"Select algorithm:\n1. {alg[1]}\n2. {alg[2]}\n3.{alg[3]}\nEnter index");
                indexAlg = System.Int16.Parse(Console.ReadLine());
                if(indexAlg < 1 || indexAlg > 3)
                {
                    Console.WriteLine($"== Test system load faild. Algorithm {alg[0]}");
                    return;
                }
            }
            Console.WriteLine($"== Test system load with algorithm {alg[indexAlg]}");

            int nReply = 0;
            if(args.Length > 2)
            {
                nReply = System.Int16.Parse(args[2]);
            }
            else
            {
                Console.WriteLine("Enter execution count of test:");
                var str = Console.ReadLine();
                if(str.Length > 0)
                    nReply = System.Int16.Parse(str);
                if(nReply < 1)
                {
                    Console.WriteLine($"== Test system load faild. Execution count={nReply}");
                    return;
                }
            }
            Console.WriteLine($"== Test system load with algorithm reply={nReply}");

            bool bLogEnable = false;
            if(args.Length > 3)
                bLogEnable = args[3].StartsWith("log");
            else
            {
                Console.WriteLine("Decide is log enable (skip to false):");
                string s = Console.ReadLine();
                if(s.StartsWith("Yes") || s.StartsWith("yes") || s.StartsWith("tru") || s.StartsWith("Tru") || s.StartsWith("Ok") || s.StartsWith("ok") || s.StartsWith("OK"))
                    bLogEnable = true;
            }
            string[] log = { "without", "with" };
            Console.WriteLine($"== Test system load {log[bLogEnable ? 1:0]} logging");

            string[] aPath = {
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_all.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_had.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_kra.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_lipa.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_nug.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_rou.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_scr.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_sko.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_ste.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_tai.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_tho.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_wil.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_bur.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_chr.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_els.xml",
                        "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_esc.xml"};

            if(nPaths == 1)
            {
                Start(indexAlg, sPath, nReply, bLogEnable);
            }
            for(int i = 0; i < nPaths; i++)
            {
                if(sPath[i] == '1')
                    Start(indexAlg, aPath[i], nReply, bLogEnable);
            }
        }
    }
}
