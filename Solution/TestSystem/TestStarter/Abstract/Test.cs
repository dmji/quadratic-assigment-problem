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

        protected AProblem m_problem;
        protected bool m_logEnabled;
        protected int m_nCount;
        protected CFile m_path;
        protected ILogger m_log;
        protected List<ITestInfo> m_aTest;
        protected List<IOptions> m_aOptions;
        protected List<CTestStatistic> m_aOptStat;

        protected ATest(string path = "", int count = 1, bool bLog = false)
        {
            m_problem = null;
            m_path = new CFile(path);
            m_nCount = count;
            m_logEnabled = bLog;
            m_log = null;
            m_aTest = null;
            m_aOptions = null;
            m_aOptStat = null;
        }
        protected virtual string GetAlgName() => "Undefine";
        protected virtual ITestInfo CreateTestInfo(string problem, string result) => new CTestInfo(problem, result);
        protected virtual IOptions GetOptionsAlg(string path) => null;
        protected void SetLogger(ISetLogger obj)
        {
            if(m_logEnabled)
            {
                obj.SetLogger(m_log);
            }
        }

        protected void Close(ITabler table)
        {
            foreach(var optStat in m_aOptStat)
                optStat.ReleaseOptStat(table);
            m_log.Close();
            table.Close();
            m_aTest.Clear();
            m_aOptions.Clear();
            m_aOptStat.Clear();
        }
    }
}