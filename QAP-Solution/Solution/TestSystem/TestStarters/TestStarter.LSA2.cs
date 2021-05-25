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
            log = new CLogger(pathLog, $"{xml.GetAttribute("name")}_{LocalSearchAlgorithm.Name(true)}"); 

            string pathTable = path + xml.GetAttribute("pathTable");
            string pathTemplate = path + xml.GetAttribute("pathTemplate");
            tbl = new CTablerExcel(pathTable, $"{xml.GetAttribute("name")}_{LocalSearchAlgorithm.Name(true)}", pathTemplate);
        }

        public static void StartTestLSA2(string path, int reply_count = 1, bool bLogEnable = false)
        {
            ILogger log = null;
            ITabler tbl = null;
            List<CTestInfo> aTest = null;
            InitLSA(path, ref aTest, ref log, ref tbl);

            CTimer timer = new CTimer();

            List<CTestStatistic> aOptStat = new List<CTestStatistic>();
            aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
            aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
            aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));
            if(reply_count == 1)
                tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", "Timer, ms", "Calc count", "Error", "Error, %", "Result", "Optimal");
            foreach(CTestInfo test in aTest)
            {
                string timeLoad = timer.Stop().ToString();
                long examVal = 0;
                bool bExam = test.Exam(ref examVal);
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                if(reply_count > 1)
                {
                    tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {QAP.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                    tbl.AddRow();
                    tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Iteration", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                }
                IDelayedRow row = new CDelayedRow(tbl);
                for(int i = 0; i < reply_count; i++)
                {
                    timer.Reset();
                    IAlgorithm ALG = new LocalSearchAlgorithm(QAP);
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
                    if(reply_count == 1)
                    {
                        if(bExam)
                        {
                            double err = resultValue - examVal;
                            double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                            long nRow = row.AddRow(errPersent, test.Name(), timerAlg.ToString(), calcCount.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString(), examVal.ToString());
                            if(aOptStat != null)
                            {
                                foreach(var optStat in aOptStat)
                                    optStat.AddStat(QAP.Size().ToString(), QAP.Size(), nRow);
                            }
                        }
                        else
                            row.AddRow(-1, i.ToString(), timerAlg.ToString(), calcCount.ToString(), "-", "-", resultValue.ToString(), "-");
                    }
                    else
                    {
                        if(bExam)
                        {
                            double err = resultValue - examVal;
                            double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                            long nRow = row.AddRow(errPersent, i.ToString(), timerAlg.ToString(), calcCount.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString());
                            if(aOptStat != null)
                            {
                                foreach(var optStat in aOptStat)
                                    optStat.AddStat(QAP.Size().ToString(), QAP.Size(), nRow);
                            }
                        }
                        else
                            row.AddRow(-1, i.ToString(), timerAlg.ToString(), calcCount.ToString(), "-", "-", resultValue.ToString());
                    }
                }
                row.Release();
                if(reply_count > 1)
                {
                    tbl.AddRow();
                    tbl.AddRow();
                }
            }
            foreach(var optStat in aOptStat)
                optStat.ReleaseOptStat(tbl);
            log.Close();
            tbl.Close();
        }
    }
}