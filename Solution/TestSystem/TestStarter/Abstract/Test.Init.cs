using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public abstract partial class ATest : ITest
    {
        protected virtual void InitProblem(string s)
        {
            if(s.Contains("\\Library\\qap\\"))
                m_problem = new CQAProblem();
            else if(s.Contains("\\Library\\tsp\\"))
                m_problem = new CTSProblem();
            else
                throw new System.Exception("Problem undefine");
        }

        protected virtual void InitLogger(ITabler table)
        {
            // create logger
            string pathLog = m_path + "logs\\";
            m_log = new CLogger(pathLog, $"{m_path.GetNameExt()}_{GetAlgName()}");

            // create tabler
            string pathTable = m_path + "results\\";
            string pathTemplate = m_path + "_template.xml";
            table = new CTablerExcel(pathTable, $"{m_path.GetNameExt()}_{GetAlgName()}", pathTemplate);
        }

        protected void Init()
        {
            // read xml starter
            XmlReader xml = null;
            {
                string path = m_path.GetPath();
                while(path.Contains('\"'))
                {
                    var i = path.IndexOf('\"') + 1;
                    path = path.Substring(i, path.LastIndexOf('\"') - i);
                }
                xml = XmlReader.Create(path);
                xml.Read();
            }

            // get options files
            {
                string path = m_path.GetDir() + "Options\\" + GetAlgName() + '\\';
                List<string> aOptionsFile = GetArrtibuteDirFiles(xml, "options", path);
                m_aOptions = new List<IOptions>();
                foreach(string str in aOptionsFile)
                    m_aOptions.Add(GetOptionsAlg(str));
                aOptionsFile.Clear();
            }

            // collect problems+exams to test
            {
                // get problems input
                List<string> aProblemFile = GetArrtibuteDirFiles(xml, "problems", m_path.GetDir());

                InitProblem(aProblemFile[0]);

                // get problems exam
                List<string> aResultFile = GetArrtibuteDirFiles(xml, "problems", m_path.GetDir(), "exam");
                List<string> aResultFileCorrupt = new List<string>();

                m_aTest = new List<ITestInfo>();
                while(aProblemFile.Count > 0)
                {
                    int index = aProblemFile[0].LastIndexOf('\\') + 1;
                    string name = aProblemFile[0].Substring(index) + ".exam";
                    bool bExamFound = false;

                    // try to find exam for problem
                    for(int i = 0; i < aResultFile.Count; i++)
                    {
                        if(aResultFile[i].Contains(name))
                        {
                            m_aTest.Add(CreateTestInfo(aProblemFile[0], aResultFile[i]));
                            aResultFile.RemoveAt(i--);
                            bExamFound = true;
                            break;
                        }
                    }

                    // exam not fount
                    if(!bExamFound)
                    {
                        m_aTest.Add(CreateTestInfo(aProblemFile[0], null));
                        aResultFileCorrupt.Add(m_aTest[m_aTest.Count - 1].Name());
                    }
                    aProblemFile.RemoveAt(0);
                }

                for(int i=0;i<m_aTest.Count;i++)
                {
                    if(m_aTest[i].Size() > 300)
                    {
                        m_aTest.RemoveAt(i);
                        i--;
                    }
                }    
            }
        }
    }
}