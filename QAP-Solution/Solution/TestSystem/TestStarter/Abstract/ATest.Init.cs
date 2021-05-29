using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public abstract partial class ATest : ITest
    {
        protected virtual void InitLogger()
        {
            // create logger
            string pathLog = m_path + "logs\\";
            m_log = new CLogger(pathLog, $"{m_xmlName}_{GetAlgName()}");

            // create tabler
            string pathTable = m_path + "results\\";
            string pathTemplate = m_path + "template.xml";
            m_tbl = new CTablerExcel(pathTable, $"{m_xmlName}_{GetAlgName()}", pathTemplate);
        }
        protected void Init()
        {
            // read xml starter
            while(m_path.Contains('\"'))
            {
                var i = m_path.IndexOf('\"')+1;
                m_path = m_path.Substring(i, m_path.LastIndexOf('\"') - i);
            }
            XmlReader xml = XmlReader.Create(m_path);
            xml.Read();

            // get xml name
            m_xmlName = m_path.Substring(m_path.LastIndexOf('\\') + 1, m_path.LastIndexOf('.') - (m_path.LastIndexOf('\\') + 1));

            // seporate absolute path
            m_path = m_path.Substring(0, m_path.LastIndexOf('\\') + 1);

            // get options files
            {
                string path = m_path + "Options" + GetAlgName() + '\\';
                List<string> aOptionsFile = GetArrtibuteDirFiles(xml, "options", path, "json");
                m_aOptions = new List<IOptions>();
                foreach(string str in aOptionsFile)
                    m_aOptions.Add(GetOptionsAlg(str));
                aOptionsFile.Clear();
            }

            // collect problems+exams to test
            {
                // get problems input
                List<string> aProblemFile = GetArrtibuteDirFiles(xml, "problems", m_path, ".dat");

                // get problems exam
                List<string> aResultFile = GetArrtibuteDirFiles(xml, "problems", m_path, ".bin");
                List<string> aResultFileCorrupt = new List<string>();

                // convert sln to bin fils
                {
                    List<string> aResultFileToConvert = GetArrtibuteDirFiles(xml, "problems", m_path, ".sln");
                    if(FileExtConverter(aResultFileToConvert, ".sln", ".bin"))
                        aResultFile.AddRange(aResultFileToConvert);
                }

                m_aTest = new List<ITestInfo>();
                while(aProblemFile.Count > 0)
                {
                    int index = aProblemFile[0].LastIndexOf('\\') + 1;
                    string name = aProblemFile[0].Substring(index, aProblemFile[0].LastIndexOf('.') - index) + ".bin";
                    CRegularSTR reg = new CRegularSTR(name);
                    bool bExamFound = false;

                    // try to find exam for problem
                    for(int i = 0; i < aResultFile.Count; i++)
                    {
                        if(aResultFile[i].Contains(name))
                        {
                            m_aTest.Add(createTestInfo(aProblemFile[0], aResultFile[i]));
                            aResultFile.RemoveAt(i--);
                            bExamFound = true;
                            break;
                        }
                    }

                    // exam not fount
                    if(!bExamFound)
                    {
                        m_aTest.Add(new CTestInfo(aProblemFile[0]));
                        aResultFileCorrupt.Add(m_aTest[m_aTest.Count - 1].Name());
                    }
                    aProblemFile.RemoveAt(0);
                }

                // workflow info for exams
                if(aResultFileCorrupt.Count > 0)
                    m_log.Msg($"Exam not fount for {aResultFileCorrupt.Count} problem.");
            }
        }
    }
}