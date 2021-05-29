using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public interface ITest
    {
        void Start();
    }
    public abstract partial class ATest : ITest
    {
        public virtual void Start() { }

        protected string m_xmlName;
        protected bool m_logEnabled;
        protected int m_nCount;
        protected string m_path;
        protected ILogger m_log;
        protected ITabler m_tbl;
        protected List<ITestInfo> m_aTest;
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
        protected virtual string GetAlgName() => "Undefine";
        protected virtual ITestInfo createTestInfo(string problem, string result) => new CTestInfo(problem, result);
        protected virtual IOptions GetOptionsAlg(string path) => null;
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
    }
}