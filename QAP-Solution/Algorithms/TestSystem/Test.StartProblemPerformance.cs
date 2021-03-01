using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Problem;
using Util;
using System.IO;
using System.Xml;


namespace Algorithms
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
                timer.Reset();
                long cST = QAP.calc(new CPermutation(QAP.calc, perm));
                long timeST = timer.StopT();
                timer.Reset();
                long cMT = QAP.calc(new CPermutation(QAP.calc, perm));
                long timeMT = timer.StopT();
                string check = cST == cMT ? "OK" : "NK";
                Console.WriteLine($"{QAP.size()} {check} : {timeST} vs {timeMT} = {timeST-timeMT}"); 
            }
        }
    }
}
