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
                    new CTestEvolution(sPath, nReply, bLogEnable).Start();
                    break;
                case 2:
                    new CTestLSA2(sPath, nReply, bLogEnable).Start();
                    break;
                case 3:
                    new CTestFullfoce(sPath, bLogEnable).Start();
                    break;
                case 4:
                    new CTestGRASP(sPath, nReply, bLogEnable).Start();
                    break;
                default:
                    Console.WriteLine("Error algorithm type");
                    break;
            }
            Console.WriteLine($"== Test system end");
        }

        static void Main(string[] args)
        {
            bool bPaths = false;
            string sPath;
            if(args.Length > 0)
            {
                sPath = args[0];
                if(sPath.Length == 1)
                    bPaths = true;
            }
            else
            {
                Console.WriteLine("Enter test-option path:");
                sPath = Console.ReadLine();
            }

            Console.WriteLine($"== Test system load with option: {sPath}");
            int indexAlg = -1;
            string[] alg = { "Undefine", "Evolution", "Local Search", "Full force", "GRASP" };
            if(args.Length > 1)
            {
                if(args[1].StartsWith("E") || args[1].StartsWith("E"))
                    indexAlg = 1;
                else if(args[1].StartsWith("L"))
                    indexAlg = 2;
                else if(args[1].StartsWith("F"))
                    indexAlg = 3;
                else if(args[1].StartsWith("G"))
                    indexAlg = 4;
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
                nReply = System.Int16.Parse(args[2]);
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
                bLogEnable = args[3].StartsWith("Y");
            else
            {
                Console.WriteLine("Decide is log enable (skip to false):");
                string s = Console.ReadLine();
                if(s.StartsWith("Y") || s.StartsWith("y")
                    || s.StartsWith("T") || s.StartsWith("t") 
                    || s.StartsWith("O") || s.StartsWith("o"))
                    bLogEnable = true;
            }
            string[] log = { "without", "with" };
            Console.WriteLine($"== Test system load {log[bLogEnable ? 1:0]} logging");

            if(!bPaths)
            {
                Start(indexAlg, sPath, nReply, bLogEnable);
            }
            else
            {
                if(!System.IO.File.Exists("_paths.txt"))
                {
                    Console.WriteLine("Paths.txt undefine");
                    Console.WriteLine(System.IO.Path.GetFullPath("paths.txt"));
                }
                else
                {
                    string[] aPath = System.IO.File.ReadAllText("_paths.txt").Split('\n');
                    foreach(var s in aPath)
                        Start(indexAlg, s, nReply, bLogEnable);
                }
            }
        }
    }
}
