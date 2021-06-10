using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TestSystem
{
    public abstract partial class ATest
    {
        protected static List<string> GetArrtibuteDirFiles(XmlReader xml, string attrName, string path, bool bGetExams = false)
        {
            string filtr = xml.GetAttribute(attrName);
            string filtrExt = xml.GetAttribute(attrName+"Ext");
            if(filtr.Contains('\\'))
            {
                path = path + filtr.Substring(0, filtr.LastIndexOf('\\') + 1);
                filtr = filtr.Substring(filtr.LastIndexOf('\\') + 1);
            }

            List<string> aResult = new List<string>();
            var aFilter = filtr.Split(';');
            var aFilterExt = filtrExt.Split(';');
            if(bGetExams)
            {
                for(int i = 0; i < aFilterExt.Length; i++)
                    aFilterExt[i] += ".exam";
            }
            var opt = new EnumerationOptions();
            opt.RecurseSubdirectories = true;
            foreach(var s in aFilter)
            {
                foreach(var ext in aFilterExt)
                    aResult.InsertRange(0, System.IO.Directory.GetFiles(path, $"{s}*.{ext}", opt));
            }
            return aResult;
        }
    }
}
