using System;
using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public class CTestFullfoce : ATest
    {
        public CTestFullfoce(string path)
        {
            m_path = path;
        }

        public override void Start()
        {
            List<CTestInfo> aTest = new List<CTestInfo>();
            string dirPath = m_path.Substring(0, m_path.LastIndexOf('\\') + 1);
            {
                XmlReader xml = XmlReader.Create(m_path);
                xml.Read();

                List<string> aProblemFile = GetArrtibuteDirFiles(xml, "pathProblems", m_path, ".dat");
                foreach(var file in aProblemFile)
                    aTest.Add(new CTestInfo(file));
            }

            CTimer timer = new CTimer();
            foreach(CTestInfo test in aTest)
            {
                IProblem QAP = new CQAPProblem(test.pathProblem);
                IAlgorithm ALG = new CFullforceAlgorithm(QAP);
                timer.Reset();
                IResultAlg result = ALG.Start(null);
                test.GenerateResultFile(dirPath + "//generated_results//", QAP.Size(), result.GetResultValue(), ALG.Result.ToString());
            }
        }
    }
}
