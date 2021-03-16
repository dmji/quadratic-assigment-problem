using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using Solution.Util;
using System.IO;
using System.Xml;


namespace Solution
{
    public partial class TestSystem
    {
        public static void StartTestProblemPerformance(string path)
        {
            List<TestInfo> aTest = null;
            string dirPath = initFullforce(path, ref aTest);
            Timer timer = new Timer();

            foreach(TestInfo test in aTest)
            {
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                List<ushort> perm = new List<ushort>(QAP.size());
                for(ushort i = 0; i < QAP.size(); i++)
                    perm.Add(i);

                CPermutation cur = new CPermutation(QAP.calc, perm);
                for(int i = 0; i < 100; i++)
                {
                    long ct = QAP.calc(cur);
                    string tt = (ct+1).ToString();
                }
                Console.Write($"{QAP.size()} : ");
                for(int i = 0; i < 10; i++)
                {
                    timer.Reset();
                    long c = QAP.calc(cur);
                    long t = timer.StopT();
                    Console.Write($"{t} ");
                }
                Console.Write("\n");
            }
        }
    }
}
