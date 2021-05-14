using System;
using System.Collections.Generic;
using Solution.Util;


namespace Solution
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
                IAlgorithm ALG = new FullforceAlgorithm(QAP);
                timer.Reset();
                ALG.Start(null);
                Console.WriteLine(timer.Stop());
                test.generateResultFile(dirPath + "//generated_results//", QAP.size(), ALG.getResultValue(), ALG.result.ToString());
            }
        }
    }
}
