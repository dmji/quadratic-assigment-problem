using System;
using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public class CTestFullfoce : ATest
    {
        public CTestFullfoce(string path, bool bLog) : base(path, 1, bLog)
        {
            m_path = path;
        }

        public override void Start()
        {
            List<CTestInfo> aTest = new List<CTestInfo>();
            string dirPath = m_path.Substring(0, m_path.LastIndexOf('\\') + 1);
            {
                // read xml starter
                while(m_path.Contains('\"'))
                {
                    var i = m_path.IndexOf('\"') + 1;
                    m_path = m_path.Substring(i, m_path.LastIndexOf('\"') - i);
                }
                XmlReader xml = XmlReader.Create(m_path);
                xml.Read();

                // get xml name
                m_xmlName = m_path.Substring(m_path.LastIndexOf('\\') + 1, m_path.LastIndexOf('.') - (m_path.LastIndexOf('\\') + 1));

                // seporate absolute path
                m_path = m_path.Substring(0, m_path.LastIndexOf('\\') + 1);


                List<string> aProblemFile = GetArrtibuteDirFiles(xml, "problems", m_path, ".dat");
                foreach(var file in aProblemFile)
                    aTest.Add(new CTestInfo(file));
            }
            InitLogger();
            
            CTimer timer = new CTimer();
            foreach(CTestInfo test in aTest)
            {
                AProblem QAP = new CQAPProblem(test.pathProblem);
                IAlgorithm ALG = new CFullforceAlgorithm(QAP);
                EnableLog(QAP, ALG);
                timer.Reset();
                IResultAlg result = ALG.Start(null);
                test.GenerateResultFile(dirPath + "//generated_results//", QAP.Size(), result.GetResultValue(), ALG.Result.ToString());

            }
        }
    }
}
