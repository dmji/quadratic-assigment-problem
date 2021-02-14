using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class RegularSTR
    {
        string regStr;
        string[] splitStr;
        bool bSplitted;

        public RegularSTR(string reg)
        {
            if(reg.Contains('*'))
            {
                splitStr = reg.Split('*', StringSplitOptions.RemoveEmptyEntries);
                bSplitted = true;
            }
            else
            { 
                regStr = reg;
                bSplitted = false;
            } 
        }

        public bool match(string str)
        {
            if(bSplitted)
            {
                int prewIndex = -1;
                foreach(string reg in splitStr)
                {
                    int curIndex = str.IndexOf(reg);
                    if(!str.Contains(reg) || (curIndex <= prewIndex))
                        return false;
                    else
                        prewIndex = curIndex;
                }
                return true;
            }
            else
            {
                return str.Contains(regStr);
            }
        }
    }
}
