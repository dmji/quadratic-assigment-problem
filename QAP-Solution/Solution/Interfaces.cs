using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
    public interface IName
    {
        string Name();
    }
    public interface IToString
    {
        string ToString();
    }
    public interface ISetLogger
    {
        void SetLogger(TestSystem.ILogger log = null);
    }
}
