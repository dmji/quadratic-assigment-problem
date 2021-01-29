using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Problem;
using Log;
using System.IO;
using System.Xml;


namespace Algorithms
{
    public partial class TestSystem
    {
        static List<string> getArrtibuteDirFiles(XmlReader xml,string attrName, string path, string ext)
        {
            string attr = xml.GetAttribute(attrName);
            string pathDir = path + attr.Substring(0, attr.LastIndexOf('\\') + 1);
            string regStr = (attr.Substring(attr.LastIndexOf('\\') + 1) + "*." + ext).Replace("*", "\\w+");
            Regex regExp = new Regex(regStr);
            List<string> aResult = new List<string>(System.IO.Directory.GetFiles(pathDir));
            for(int i = 0; i < aResult.Count; i++)
            {
                if(!regExp.IsMatch(aResult[i].Substring(aResult[i].LastIndexOf('\\') + 1)))
                    aResult.RemoveAt(i);
            }
            return aResult;
        }

        public static void StartTestEvalution(string path)
        {
            XmlReader xml = XmlReader.Create(path);
            xml.Read();
            path = path.Substring(0, path.LastIndexOf('\\')+1);

            List<string> aOptionsFile = getArrtibuteDirFiles(xml, "pathOptions",path,"json");
            List<IOptions> aOptions = new List<IOptions>();
            foreach(string str in aOptionsFile)
                aOptions.Add(new EvalutionAlgorithm.Options(str));
            aOptionsFile.Clear();

            List<string> aProblemFile = getArrtibuteDirFiles(xml, "pathProblems", path,"dat");
            List<string> aResultFile  = getArrtibuteDirFiles(xml, "pathProblems", path,"sln");
            List<TestInfo> aTest = new List<TestInfo>();
            while(aProblemFile.Count > 0)
            {
                int index = aProblemFile[0].LastIndexOf('\\')+1;
                string name = aProblemFile[0].Substring(index, aProblemFile[0].LastIndexOf('.') - index) + ".sln";
                Regex reg = new Regex(name);
                bool bExamFound = false;
                for(int i = 0; i < aResultFile.Count; i++)
                {
                    if(aResultFile[i].Contains(name))
                    {
                        aTest.Add(new TestInfo(aProblemFile[0], aResultFile[i]));
                        aResultFile.RemoveAt(i--);
                        bExamFound = true;
                        break;
                    }
                }
                if(!bExamFound)
                    aTest.Add(new TestInfo(aProblemFile[0]));
                aProblemFile.RemoveAt(0);
            }

            string pathLog = path + xml.GetAttribute("pathLog");
            Logger log = new Logger(pathLog, $"{xml.GetAttribute("name")}_{EvalutionAlgorithm.getName(true)}");
            string pathTable = path + xml.GetAttribute("pathTable");
            Tabler tbl = new Tabler(pathTable, $"{xml.GetAttribute("name")}_{EvalutionAlgorithm.getName(true)}");
            tbl.put("Name", "OptionSet"); //TODO TABLE
            foreach(TestInfo test in aTest)
            {
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                Algorithm ALG = new EvalutionAlgorithm(QAP);

                ALG.setLogger(log);
                QAP.setLogger(log);
                foreach(IOptions opt in aOptions)
                {
                    ALG.Start(opt);
                    tbl.put(ALG.result.ToString); //TODO TABLE
                }

                log.msg(ALG.ToString());
            }
            log.Close();
        }
    }
}
