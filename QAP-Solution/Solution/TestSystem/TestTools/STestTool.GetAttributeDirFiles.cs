using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TestSystem
{
    public partial struct STestTools
    {
        public static List<string> GetArrtibuteDirFiles(XmlReader xml, string attrName, string path, string ext)
        {
            string regStr = xml.GetAttribute(attrName);
            if(regStr.Contains('\\'))
            {
                path = path + regStr.Substring(0, regStr.LastIndexOf('\\') + 1);
                regStr = regStr.Substring(regStr.LastIndexOf('\\') + 1);
            }
            CRegularSTR regExt = new CRegularSTR(ext);
            CRegularSTR reg = new CRegularSTR(regStr);

            List<string> aResult = new List<string>(System.IO.Directory.GetFiles(path));
            for(int i = 0; i < aResult.Count; i++)
            {
                string match = aResult[i].Substring(aResult[i].LastIndexOf('\\') + 1);
                if(!regExt.Match(match) || !reg.Match(match))
                    aResult.RemoveAt(i--);
            }
            return aResult;
        }
    }
}
