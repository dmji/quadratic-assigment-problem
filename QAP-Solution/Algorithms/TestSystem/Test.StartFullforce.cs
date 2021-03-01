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
        public static void StartTestFullforce(string path)
        {
            List<TestInfo> aTest = null;
            string dirPath = initFullforce(path, ref aTest);
            Timer timer = new Timer();

            foreach(TestInfo test in aTest)
            {
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                Algorithm ALG = new FullforceAlgorithm(QAP);
                timer.Reset();
                ALG.Start(null);
                Console.WriteLine(timer.Stop());
                test.generateResultFile(dirPath + "//generated_results//", QAP.size(), ALG.getResultValue(), ALG.result.ToString());
            }
        }
    }
}
