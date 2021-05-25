using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public interface ITest
    {

    }
    public abstract class ATest : ITest
    {
        protected string m_xmlName;
        protected bool m_logEnabled;
        protected int m_nCount;
        protected string m_path;
        protected ILogger m_log;
        protected ITabler m_tbl;
        protected List<CTestInfo> m_aTest;
        protected List<IOptions> m_aOptions;
        protected List<CTestStatistic> m_aOptStat;

        protected ATest(string path = "", int count = 1, bool bLog = false)
        {
            m_path = path;
            m_nCount = count;
            m_logEnabled = bLog;
            m_log = null;
            m_tbl = null;
            m_aTest = null;
            m_aOptions = null;
            m_aOptStat = null;
            m_xmlName = "";
        }
        protected void EnableLog(AProblem QAP, IAlgorithm ALG)
        {
            if(m_logEnabled)
            {
                ALG.SetLogger(m_log);
                QAP.SetLogger(m_log);
            }
        }
        protected void Close()
        {
            foreach(var optStat in m_aOptStat)
                optStat.ReleaseOptStat(m_tbl);
            m_log.Close();
            m_tbl.Close();
            m_aTest.Clear();
            m_aOptions.Clear();
            m_aOptStat.Clear();
        }

        protected virtual string GetAlgName() => "Undefine";
        protected virtual IOptions GetOptionsAlg(string path) => null;
        protected virtual void InitLogger()
        {
            // create logger
            string pathLog = m_path + "logs";
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
                List<string> aOptionsFile = STestTools.GetArrtibuteDirFiles(xml, "options", path, "json");
                m_aOptions = new List<IOptions>();
                foreach(string str in aOptionsFile)
                    m_aOptions.Add(GetOptionsAlg(str));
                aOptionsFile.Clear();
            }

            // collect problems+exams to test
            {
                // get problems input
                List<string> aProblemFile = STestTools.GetArrtibuteDirFiles(xml, "problems", m_path, ".dat");

                // get problems exam
                List<string> aResultFile = STestTools.GetArrtibuteDirFiles(xml, "problems", m_path, ".bin");
                List<string> aResultFileCorrupt = new List<string>();

                // convert sln to bin fils
                {
                    List<string> aResultFileToConvert = STestTools.GetArrtibuteDirFiles(xml, "problems", m_path, ".sln");
                    if(STestTools.FileExtConverter(aResultFileToConvert, ".sln", ".bin"))
                        aResultFile.AddRange(aResultFileToConvert);
                }

                m_aTest = new List<CTestInfo>();
                while(aProblemFile.Count > 0)
                {
                    int index = aProblemFile[0].LastIndexOf('\\') + 1;
                    string name = aProblemFile[0].Substring(index, aProblemFile[0].LastIndexOf('.') - index) + ".bin";
                    CRegularSTR reg = new CRegularSTR(name);
                    bool bExamFound = false;
                    for(int i = 0; i < aResultFile.Count; i++)
                    {
                        if(aResultFile[i].Contains(name))
                        {
                            m_aTest.Add(new CTestInfo(aProblemFile[0], aResultFile[i]));
                            aResultFile.RemoveAt(i--);
                            bExamFound = true;
                            break;
                        }
                    }
                    if(!bExamFound)
                    {
                        m_aTest.Add(new CTestInfo(aProblemFile[0]));
                        aResultFileCorrupt.Add(m_aTest[m_aTest.Count - 1].Name());
                    }
                    aProblemFile.RemoveAt(0);
                }
            }
        }
    }
}