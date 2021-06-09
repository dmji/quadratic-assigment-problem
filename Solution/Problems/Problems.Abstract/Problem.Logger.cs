using System;

namespace Solution
{
    /// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
    public abstract partial class AProblem : ISetLogger
    {
        ILogger m_log;

        public void SetLogger(ILogger log = null) { m_log = log == null ? new CEmptyLogger() : log; }
        protected bool Msg(string s) => m_log.Msg(s, true);
    }
}
