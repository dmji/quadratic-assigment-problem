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

                List<string> aProblemFile = GetArrtibuteDirFiles(xml, "problems", m_path);
                InitProblem(aProblemFile[0]);
                foreach(var file in aProblemFile)
                    aTest.Add(new CTestInfo(file));
            }
            InitLogger();
            
            CTimer timer = new CTimer();
            foreach(CTestInfo test in aTest)
            {
                m_problem.Deserialize(test.pathProblem);
                IAlgorithm ALG = new CFullforceAlgorithm(m_problem);
                EnableLog(m_problem, ALG);
                timer.Reset();
                ALG.Start(null);
                m_log.Msg($"{m_problem.Size()} {ALG.GetResultValue()}\n{ALG.Result.ToString()}");
            }
        }
    }
}
