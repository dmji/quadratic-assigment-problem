using System;
using System.Collections.Generic;

namespace Solution
{
    public abstract partial class AAlgorithm : ISetLogger
    {
        ILogger m_log;
        public void SetLogger(ILogger log = null) { m_log = log == null ? new CEmptyLogger() : log; }
        protected bool Msg(string s) => m_log.Msg(s, true);
    }
}
