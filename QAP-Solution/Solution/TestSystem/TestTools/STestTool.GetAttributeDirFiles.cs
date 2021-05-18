using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TestSystem
{
    public partial struct STestTools
    {
        public static List<string> GetArrtibuteDirFiles(XmlReader xml,string attrName, string path, string ext)
        {
            string attr = xml.GetAttribute(attrName);
            string pathDir = path + attr.Substring(0, attr.LastIndexOf('\\') + 1);
            string regStr = attr.Substring(attr.LastIndexOf('\\') + 1);
            CRegularSTR regExt = new CRegularSTR(ext);
            CRegularSTR reg = new CRegularSTR(regStr);

            List<string> aResult = new List<string>(System.IO.Directory.GetFiles(pathDir));
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
