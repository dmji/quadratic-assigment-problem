using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public partial struct STestStarter
    {
        static void InitLSA(string path, ref List<CTestInfo> aTest, ref ILogger log, ref ITabler tbl)
        {
            XmlReader xml = XmlReader.Create(path);
            xml.Read();
            path = path.Substring(0, path.LastIndexOf('\\') + 1);

            List<string> aProblemFile = STestTools.GetArrtibuteDirFiles(xml, "pathProblems", path, ".dat");
            List<string> aResultFile = STestTools.GetArrtibuteDirFiles(xml, "pathProblems", path, ".bin");
            List<string> aResultFileCorrupt = new List<string>();

            List<string> aResultFileToConvert = STestTools.GetArrtibuteDirFiles(xml, "pathProblems", path, ".sln");
            if(STestTools.FileExtConverter(aResultFileToConvert, ".sln", ".bin"))
                aResultFile.AddRange(aResultFileToConvert);

            aTest = new List<CTestInfo>();
            while(aProblemFile.Count > 0)
            {
                int index = aProblemFile[0].LastIndexOf('\\') + 1;
                string name = aProblemFile[0].Substring(index, aProblemFile[0].LastIndexOf('.') - index) + ".bin";
                CRegularSTR reg = new CRegularSTR(name);
                bool bExamFound = false;
                for(int i = 0; i < aResultFile.Count; i++)
                {
                    if(aResultFile[i].Contains(name))
                    {
                        aTest.Add(new CTestInfo(aProblemFile[0], aResultFile[i]));
                        aResultFile.RemoveAt(i--);
                        bExamFound = true;
                        break;
                    }
                }
                if(!bExamFound)
                {
                    aTest.Add(new CTestInfo(aProblemFile[0]));
                    aResultFileCorrupt.Add(aTest[aTest.Count - 1].Name());
                }
                aProblemFile.RemoveAt(0);
            }

            string pathLog = path + xml.GetAttribute("pathLog");
            log = new CLogger(pathLog, $"{xml.GetAttribute("name")}_{EvolutionAlgorithm.Name(true)}"); 

            string pathTable = path + xml.GetAttribute("pathTable");
            string pathTemplate = path + xml.GetAttribute("pathTemplate");
            tbl = new CTablerExcel(pathTable, $"{xml.GetAttribute("name")}_{EvolutionAlgorithm.Name(true)}", pathTemplate);
        }

        public static void StartTestLSA2(string path, int reply_count = 1, bool bLogEnable = false)
        {
            ILogger log = null;
            ITabler tbl = null;
            List<CTestInfo> aTest = null;
            InitLSA(path, ref aTest, ref log, ref tbl);

            CTimer timer = new CTimer();

            CTestStatistic optStat = new CTestStatistic(1);
            foreach(CTestInfo test in aTest)
            {
                for(int i = 0; i < reply_count; i++)
                {
                    timer.Reset();
                    CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                    string timeLoad = timer.Stop().ToString();

                    long examVal = 0;
                    bool bExam = test.Exam(ref examVal);
                    if(reply_count == 1)
                    {
                        tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {QAP.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                        tbl.AddRow();
                        tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                    }
                    else
                        tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                    IAlgorithm ALG = new LocalSearchAlgorithm(QAP);
                    IDelayedRow row = new CDelayedRow(tbl, false);

                    if(bLogEnable)
                    {
                        ALG.SetLogger(log);
                        QAP.SetLogger(log);
                    }

                    timer.Reset();
                    IResultAlg result = ALG.Start(null);

                    long timerAlg = timer.Stop();
                    long calcCount = result.GetCalcCount();
                    long curRes = result.GetResultValue();
                    long resultValue = curRes;
                    long resultBest = 0;
                    if(resultBest == 0 || resultBest > curRes)
                        resultBest = curRes;

                    log.Msg($"Problem {test.Name()}; iteration: {i} done", true);
                    log.Msg($"Problem {test.Name()}; iteration: {i}, log:{ALG})");
                    AddResult(row, test.Name(), timerAlg.ToString(), calcCount, resultValue, bExam ? examVal : -1, reply_count == 1, null, resultBest.ToString(), QAP.Size());

                    row.Release();
                    tbl.AddRow();
                    tbl.AddRow();
                }
                if(reply_count > 1)
                {
                    //tbl.addCells();
                    // todo statistic 
                }
            }
            optStat.ReleaseOptStat(tbl);
            log.Close();
            tbl.Close();
        }
    }
}