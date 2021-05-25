using System;
using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public class CTestFullfoce
    {
        public static void StartTestFullforce(string path)
        {
            List<CTestInfo> aTest = new List<CTestInfo>();
            string dirPath = path.Substring(0, path.LastIndexOf('\\') + 1);
            {
                XmlReader xml = XmlReader.Create(path);
                xml.Read();

                List<string> aProblemFile = STestTools.GetArrtibuteDirFiles(xml, "pathProblems", path, ".dat");
                foreach(var file in aProblemFile)
                    aTest.Add(new CTestInfo(file));
            }

            CTimer timer = new CTimer();
            foreach(CTestInfo test in aTest)
            {
                IProblem QAP = new CQAPProblem(test.pathProblem);
                IAlgorithm ALG = new FullforceAlgorithm(QAP);
                timer.Reset();
                IResultAlg result = ALG.Start(null);
                test.GenerateResultFile(dirPath + "//generated_results//", QAP.Size(), result.GetResultValue(), ALG.Result.ToString());
            }
        }
    }
}
