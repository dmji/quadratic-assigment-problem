using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class RegularSTR
    {
        string[] splitStr;

        public RegularSTR(string reg)
        {
            splitStr = reg.Split('*', StringSplitOptions.RemoveEmptyEntries);
        }

        public bool match(string str)
        {
            int prewIndex = -1;
            if(splitStr.Length == 0)
                return true;
            foreach(string reg in splitStr)
            {
                int curIndex = str.IndexOf(reg);
                if(str.Contains(reg) /*|| (curIndex <= prewIndex)*/)
                    return true;
                //else
                //    prewIndex = curIndex;
            }
            return false;
        }
    }
}
