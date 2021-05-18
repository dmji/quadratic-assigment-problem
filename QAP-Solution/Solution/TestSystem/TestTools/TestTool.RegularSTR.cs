using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem
{ 
    public class CRegularSTR
    {
        List<string[]> m_aSplitStr;

        public CRegularSTR(string reg)
        {
            string[] orCondition = reg.Split(';', StringSplitOptions.RemoveEmptyEntries);
            m_aSplitStr = new List<string[]>();
            foreach(string str in orCondition)
                m_aSplitStr.Add(str.Split('*', StringSplitOptions.RemoveEmptyEntries));
        }

        bool MatchAnd(string str, string[] aReg)
        {
            if(aReg.Length != 0)
            {
                int prewIndex = -1;
                foreach(string reg in aReg)
                {
                    int curIndex = str.IndexOf(reg);
                    if(!str.Contains(reg) || (curIndex <= prewIndex))
                        return false;
                    else
                        prewIndex = curIndex;
                }
            }
            return true;
        }

        public bool Match(string str)
        {
            if(m_aSplitStr.Count == 0)
                return true;
            foreach(string[] reg in m_aSplitStr)
            {
                if(MatchAnd(str, reg))
                    return true;
            }
            return false;
        }
    }
}
