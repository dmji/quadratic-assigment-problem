using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
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
            string regStr = attr.Substring(attr.LastIndexOf('\\') + 1);
            RegularSTR regExt = new RegularSTR(ext);
            RegularSTR reg = new RegularSTR(regStr);
            List<string> aResult = new List<string>(System.IO.Directory.GetFiles(pathDir));
            for(int i = 0; i < aResult.Count; i++)
            {
                string match = aResult[i].Substring(aResult[i].LastIndexOf('\\') + 1);
                if(!regExt.match(match) || !reg.match(match))
                    aResult.RemoveAt(i--);
            }
            return aResult;
        }

        static void writeHeader(ITabler tbl, List<IOptions> aOptions)
        {
            tbl.addCells("boldGrey", aOptions[0].getValuesNames().Replace("DEFINE_", "").Replace("_", " ").Split(';', StringSplitOptions.RemoveEmptyEntries));
            foreach(IOptions opt in aOptions)
            {
                tbl.addRow();
                tbl.addCells("greyColored", opt.getValues().Split(';', StringSplitOptions.RemoveEmptyEntries));
            }
            tbl.addRow();
            tbl.addRow();
        }

        static void initEvalution(string path, ref List<TestInfo> aTest, ref List<IOptions> aOptions, ref Logger log, ref ITabler tbl)
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
            List<string> aResultFileCorrupt = new List<string>();

            List<string> aResultFileToConvert = getArrtibuteDirFiles(xml, "pathProblems", path, ".sln");
            aResultFile.AddRange(convertResultsToBin(aResultFileToConvert, ".sln", ".bin"));

            aTest = new List<TestInfo>();
            while(aProblemFile.Count > 0)
            {
                int index = aProblemFile[0].LastIndexOf('\\') + 1;
                string name = aProblemFile[0].Substring(index, aProblemFile[0].LastIndexOf('.') - index) + ".bin";
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
                {
                    aTest.Add(new TestInfo(aProblemFile[0]));
                    aResultFileCorrupt.Add(aTest[aTest.Count-1].nameProblem());
                } 
                aProblemFile.RemoveAt(0);
            }

            string pathLog = path + xml.GetAttribute("pathLog");
            log = new Logger(pathLog, $"{xml.GetAttribute("name")}_{EvalutionAlgorithm.getName(true)}");


            string pathTable = path + xml.GetAttribute("pathTable");
            string pathTemplate = path + xml.GetAttribute("pathTemplate");
            tbl = new Tabler(pathTable, $"{xml.GetAttribute("name")}_{EvalutionAlgorithm.getName(true)}", pathTemplate);
        }

        static string initFullforce(string path, ref List<TestInfo> aTest)
        {
            XmlReader xml = XmlReader.Create(path);
            xml.Read();
            path = path.Substring(0, path.LastIndexOf('\\') + 1);

            List<string> aProblemFile = getArrtibuteDirFiles(xml, "pathProblems", path, ".dat");

            aTest = new List<TestInfo>();
            while(aProblemFile.Count > 0)
            {
                aTest.Add(new TestInfo(aProblemFile[0]));
                aProblemFile.RemoveAt(0);
            }
            return path;
        }

        static List<string> convertResultsToBin(List<string> aPath, string oldExt, string newExt)
        {
            List<string> aRes = new List<string>();
            foreach(string resultPath in aPath)
            {
                StreamReader file = new StreamReader(resultPath);
                string str = file.ReadToEnd();

                string convName = resultPath.Replace(oldExt, newExt);
                if(!System.IO.File.Exists(convName))
                    System.IO.File.Create(convName).Close();
                StreamWriter converter = new StreamWriter(convName);
                converter.Write(str);
                converter.Close();

                aRes.Add(convName);
            }
            return aRes;
        }

        static void addResult(TestInfo test, string optName, string timer, double calcs, double resultValue, bool bSingleExec=true, OptionsStatistic optStat=null, string resultBest = "", int size = 0)
        {
            if(test.isExamed())
            {
                long examVal = test.exam();
                double err = resultValue - examVal;
                double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                if(bSingleExec)
                    test.AddRow(errPersent, optName, timer, calcs.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString());
                else
                {
                    test.AddRow(errPersent, optName, timer, calcs.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString(), resultBest);
                    optStat.addStat(optName, size, errPersent);
                }
            }
            else
            {
                if(bSingleExec)
                    test.AddRow(-1, optName, timer, calcs.ToString(), "-", "-", resultValue.ToString());
                else
                    test.AddRow(-1, optName, timer, calcs.ToString(), "-", "-", resultValue.ToString(), resultBest);
            }
        }
    }
}
