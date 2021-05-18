using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public partial struct STestStarter
    {
        static void InitEvolution(string path, ref List<CTestInfo> aTest, ref List<Solution.IOptions> aOptions, ref ILogger log, ref ITabler tbl)
        {
            XmlReader xml = XmlReader.Create(path);
            xml.Read();
            path = path.Substring(0, path.LastIndexOf('\\') + 1);

            List<string> aOptionsFile = STestTools.GetArrtibuteDirFiles(xml, "pathOptions", path, "json");
            aOptions = new List<IOptions>();
            foreach(string str in aOptionsFile)
                aOptions.Add(new EvolutionAlgorithm.Options(str));
            aOptionsFile.Clear();

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
            log = new CLogger(pathLog, $"{xml.GetAttribute("name")}_{EvolutionAlgorithm.getName(true)}");


            string pathTable = path + xml.GetAttribute("pathTable");
            string pathTemplate = path + xml.GetAttribute("pathTemplate");
            tbl = new CTablerExcel(pathTable, $"{xml.GetAttribute("name")}_{EvolutionAlgorithm.getName(true)}", pathTemplate);
        }

        public static void StartTestEvolution(string path, int reply_count = 1, bool bLogEnabled = false)
        {
            ILogger log = null;
            ITabler tbl = null;
            List<CTestInfo> aTest = null;
            List<IOptions> aOptions = null;
            InitEvolution(path, ref aTest, ref aOptions, ref log, ref tbl);

            STestTools.WriteOptionsHeader(tbl, aOptions);

            CTimer timer = new CTimer();

            CTestStatistic optStat = new CTestStatistic(5);
            foreach(CTestInfo test in aTest)
            {
                timer.Reset();
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                string timeLoad = timer.Stop().ToString();

                long examVal = 0;
                bool bExam = test.Exam(ref examVal);
                tbl.AddCells("bold", "Name problem", test.Name(), $"Size: {QAP.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                tbl.AddRow();
                if(reply_count == 1)
                    tbl.AddCells("bold", "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                else
                    tbl.AddCells("bold", "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                IAlgorithm ALG = new EvolutionAlgorithm(QAP);

                IDelayedRow row = new CDelayedRow(tbl, true);

                if(bLogEnabled)
                {
                    ((IDiagnostic)ALG).SetLogger(log);
                    QAP.setLogger(log);
                }
                foreach(IOptions opt in aOptions)
                {
                    long timerAlg = 0, calcCount = 0, resultValue = 0, resultBest = 0;
                    for(int i = 0; i < reply_count; i++)
                    {
                        timer.Reset();
                        IDiagnostic result = ALG.Start(opt);

                        timerAlg += timer.Stop();
                        calcCount += result.GetCalcCount();
                        long curRes = result.GetResultValue();
                        resultValue += curRes;
                        if(resultBest == 0 || resultBest > curRes)
                            resultBest = curRes;
                    }
                    double avgTimerAlg = timerAlg / reply_count;
                    double avgCalcCount = calcCount / reply_count;
                    double avgResultValue = resultValue / reply_count;

                    log.Msg($"On opt: {opt.Name()} problem {test.Name()} log:{ALG})");
                    AddResult(row, opt.Name(), avgTimerAlg.ToString(), avgCalcCount, avgResultValue, bExam ? examVal : -1, reply_count == 1, optStat, resultBest.ToString(), QAP.Size());
                }
                row.Release();
                tbl.AddRow();
                tbl.AddRow();
            }
            optStat.ReleaseOptStat(tbl);
            log.Close();
            tbl.Close();
        }

        public static void AddResult(IDelayedRow row, string optName, string timer, double calcs, double resultValue, long examVal, bool bSingleExec = true, CTestStatistic optStat = null, string resultBest = "", int size = 0)
        {
            if(examVal > -1)
            {
                double err = resultValue - examVal;
                double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                long nRow = row.AddRow(errPersent, optName, timer, calcs.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString(), bSingleExec ? "" : resultBest);
                if(optStat != null)
                    optStat.AddStat(optName, size, nRow);
            }
            else
                row.AddRow(-1, optName, timer, calcs.ToString(), "-", "-", resultValue.ToString(), bSingleExec ? "" : resultBest);
        }
    }
}