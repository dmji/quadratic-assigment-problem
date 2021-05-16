using System;
using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public partial struct STestStarter
    {
        public static void StartTestFullforce(string path)
        {
            List<CTestInfo> aTest = new List<CTestInfo>();
            string dirPath = path.Substring(0, path.LastIndexOf('\\') + 1);
            {
                XmlReader xml = XmlReader.Create(path);
                xml.Read();

                List<string> aProblemFile = STestTools.getArrtibuteDirFiles(xml, "pathProblems", path, ".dat");
                foreach(var file in aProblemFile)
                    aTest.Add(new CTestInfo(file));
            }

            CTimer timer = new CTimer();
            foreach(CTestInfo test in aTest)
            {
                IProblem QAP = new CQAPProblem(test.pathProblem);
                IAlgorithm ALG = new FullforceAlgorithm(QAP);
                timer.Reset();
                IDiagnostic result = ALG.Start(null);
                test.generateResultFile(dirPath + "//generated_results//", QAP.size(), result.getResultValue(), ALG.result.ToString());
            }
        }
    }
}
