using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem
{ 
    public class CRegularSTR
    {
        List<string[]> aSplitStr;

        public CRegularSTR(string reg)
        {
            string[] orCondition = reg.Split(';', StringSplitOptions.RemoveEmptyEntries);
            aSplitStr = new List<string[]>();
            foreach(string str in orCondition)
                aSplitStr.Add(str.Split('*', StringSplitOptions.RemoveEmptyEntries));
        }

        bool matchAnd(string str, string[] aReg)
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

        public bool match(string str)
        {
            if(aSplitStr.Count == 0)
                return true;
            foreach(string[] reg in aSplitStr)
            {
                if(matchAnd(str, reg))
                    return true;
            }
            return false;
        }
    }
}
