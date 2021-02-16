using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Problem;
using Util;
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
            string regStr = attr.Substring(attr.LastIndexOf('\\') + 1) + ext;
            RegularSTR reg = new RegularSTR(regStr);
            List<string> aResult = new List<string>(System.IO.Directory.GetFiles(pathDir));
            for(int i = 0; i < aResult.Count; i++)
            {
                string match = aResult[i].Substring(aResult[i].LastIndexOf('\\') + 1);
                if(!reg.match(match))
                    aResult.RemoveAt(i--);
            }
            return aResult;
        }

        private static void init(string path, ref List<TestInfo> aTest, ref List<IOptions> aOptions, ref Logger log, ref ITabler tbl)
        {
            XmlReader xml = XmlReader.Create(path);
            xml.Read();
            path = path.Substring(0, path.LastIndexOf('\\') + 1);

            List<string> aOptionsFile = getArrtibuteDirFiles(xml, "pathOptions", path, "json");
            aOptions = new List<IOptions>();
            foreach(string str in aOptionsFile)
                aOptions.Add(new EvalutionAlgorithm.Options(str));
            aOptionsFile.Clear();

            List<string> aProblemFile = getArrtibuteDirFiles(xml, "pathProblems", path, ".dat");
            List<string> aResultFile = getArrtibuteDirFiles(xml, "pathProblems", path, ".bin");
            aResultFile.AddRange(getArrtibuteDirFiles(xml, "pathProblems", path, ".sln"));

            aTest = new List<TestInfo>();
            while(aProblemFile.Count > 0)
            {
                int index = aProblemFile[0].LastIndexOf('\\') + 1;
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
            log = new Logger(pathLog, $"{xml.GetAttribute("name")}_{EvalutionAlgorithm.getName(true)}");


            string pathTable = path + xml.GetAttribute("pathTable");
            string pathTemplate = path + xml.GetAttribute("pathTemplate");
            tbl = new Tabler(pathTable, $"{xml.GetAttribute("name")}_{EvalutionAlgorithm.getName(true)}", pathTemplate);
        }

        public static void StartTestEvalution(string path)
        {
            Logger log = null;
            ITabler tbl = null;
            List<TestInfo> aTest = null;
            List<IOptions> aOptions = null;
            init(path, ref aTest, ref aOptions, ref log, ref tbl);
            Timer timer = new Timer();

            tbl.addCells("boldGrey",aOptions[0].getValuesNames().Replace("DEFINE_", "").Replace("_"," ").Split(';',StringSplitOptions.RemoveEmptyEntries));
            foreach(IOptions opt in aOptions)
            {
                tbl.addRow();
                tbl.addCells("greyColored", opt.getValues().Split(';', StringSplitOptions.RemoveEmptyEntries));
            }
            tbl.addRow();
            tbl.addRow();
            foreach(TestInfo test in aTest)
            {
                timer.Reset();
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                string timeLoad = timer.Stop();
                long examVal = test.exam();
                tbl.addCells("bold", "Name problem", test.nameProblem(), $"Size: {QAP.size()}",$"Load time: {timeLoad}","Optimal:", test.isExamed() ? examVal.ToString() : "UNDEFINED");
                tbl.addRow();
                tbl.addCells("bold", "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                Algorithm ALG = new EvalutionAlgorithm(QAP);

                //ALG.setLogger(log);
                //QAP.setLogger(log);
                foreach(IOptions opt in aOptions)
                {
                    timer.Reset();
                    ALG.Start(opt);
                    string timerAlg = timer.Stop();
                    log.msg($"On opt: {opt.getName()} problem {test.nameProblem()} log:{ALG})");
                    if(test.isExamed())
                    {
                        double err = ALG.ResultValue() - examVal;
                        double errPersent = (err / ((double)examVal) * 100);
                        test.AddRow(errPersent, opt.getName(), timerAlg, ALG.strCalcCount(), err.ToString(), errPersent.ToString(), ALG.ResultValue().ToString());
                    }
                    else
                    {
                        tbl.addRow();
                        tbl.addCells("simple", opt.getName(), timerAlg, ALG.strCalcCount(), "-", "-", ALG.ResultValue().ToString());
                    }
                }
                test.RelaseRow(tbl);
                tbl.addRow();
                tbl.addRow();
            }
            log.Close();
            tbl.Close();
        }
    }
}
