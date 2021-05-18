using System;
using System.Collections.Generic;

namespace Solution
{
    public abstract partial class AAlgorithm : IAlgorithm, IDiagnostic
    {
        TestSystem.ILogger m_log;
        public void SetLogger(TestSystem.ILogger log = null) { m_log = log == null ? new TestSystem.CEmptyLogger() : log; }
        protected bool msg(string s) => m_log.Msg(s);
    }
}
