using System;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public abstract partial class AProblem : ISetLogger
    {
        TestSystem.ILogger m_log;

        public void SetLogger(TestSystem.ILogger log = null) { m_log = log == null ? new TestSystem.CEmptyLogger() : log; }
        protected bool Msg(string s) => m_log.Msg(s);
    }
}
