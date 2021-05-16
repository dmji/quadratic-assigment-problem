using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public partial struct STestStarter
    {
        static void initEvolution(string path, ref List<CTestInfo> aTest, ref List<Solution.IOptions> aOptions, ref ILogger log, ref ITabler tbl)
        {
            XmlReader xml = XmlReader.Create(path);
            xml.Read();
            path = path.Substring(0, path.LastIndexOf('\\') + 1);

            List<string> aOptionsFile = STestTools.getArrtibuteDirFiles(xml, "pathOptions", path, "json");
            aOptions = new List<IOptions>();
            foreach(string str in aOptionsFile)
                aOptions.Add(new EvolutionAlgorithm.Options(str));
            aOptionsFile.Clear();

            List<string> aProblemFile = STestTools.getArrtibuteDirFiles(xml, "pathProblems", path, ".dat");
            List<string> aResultFile = STestTools.getArrtibuteDirFiles(xml, "pathProblems", path, ".bin");
            List<string> aResultFileCorrupt = new List<string>();

            List<string> aResultFileToConvert = STestTools.getArrtibuteDirFiles(xml, "pathProblems", path, ".sln");
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
                    aResultFileCorrupt.Add(aTest[aTest.Count - 1].nameProblem());
                }
                aProblemFile.RemoveAt(0);
            }

            string pathLog = path + xml.GetAttribute("pathLog");
            log = new CLogger(pathLog, $"{xml.GetAttribute("name")}_{EvolutionAlgorithm.getName(true)}");


            string pathTable = path + xml.GetAttribute("pathTable");
            string pathTemplate = path + xml.GetAttribute("pathTemplate");
            tbl = new CTablerExcel(pathTable, $"{xml.GetAttribute("name")}_{EvolutionAlgorithm.getName(true)}", pathTemplate);
        }

        public static void StartTestEvolution(string path, int reply_count = 1)
        {
            ILogger log = null;
            ITabler tbl = null;
            List<CTestInfo> aTest = null;
            List<IOptions> aOptions = null;
            initEvolution(path, ref aTest, ref aOptions, ref log, ref tbl);

            STestTools.writeOptionsHeader(tbl, aOptions);

            CTimer timer = new CTimer();

            CTestStatistic optStat = new CTestStatistic();
            foreach(CTestInfo test in aTest)
            {
                timer.Reset();
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                string timeLoad = timer.Stop().ToString();

                long examVal = 0;
                bool bExam = test.exam(ref examVal);
                tbl.addCells("bold", "Name problem", test.nameProblem(), $"Size: {QAP.size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                tbl.addRow();
                if(reply_count == 1)
                    tbl.addCells("bold", "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                else
                    tbl.addCells("bold", "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                IAlgorithm ALG = new EvolutionAlgorithm(QAP);

                IDelayedRow row = new CDelayedRow(tbl);

                //ALG.setLogger(log);
                //QAP.setLogger(log);
                foreach(IOptions opt in aOptions)
                {
                    long timerAlg = 0, calcCount = 0, resultValue = 0, resultBest = 0;
                    for(int i = 0; i < reply_count; i++)
                    {
                        timer.Reset();
                        ALG.Start(opt);

                        timerAlg += timer.Stop();
                        calcCount += ALG.getCalcCount();
                        long curRes = ALG.getResultValue();
                        resultValue += curRes;
                        if(resultBest == 0 || resultBest > curRes)
                            resultBest = curRes;
                    }
                    double avgTimerAlg = timerAlg / reply_count;
                    double avgCalcCount = calcCount / reply_count;
                    double avgResultValue = resultValue / reply_count;

                    log.msg($"On opt: {opt.getName()} problem {test.nameProblem()} log:{ALG})");
                    addResult(row, opt.getName(), avgTimerAlg.ToString(), avgCalcCount, avgResultValue, bExam ? examVal : -1, reply_count == 1, optStat, resultBest.ToString(), QAP.size());
                }
                row.Release();
                tbl.addRow();
                tbl.addRow();
            }
            optStat.releaseOptStat(tbl);
            log.Close();
            tbl.Close();
        }

        public static void addResult(IDelayedRow row, string optName, string timer, double calcs, double resultValue, long examVal, bool bSingleExec = true, CTestStatistic optStat = null, string resultBest = "", int size = 0)
        {
            if(examVal > -1)
            {
                double err = resultValue - examVal;
                double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                long nRow = row.AddRow(errPersent, optName, timer, calcs.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString(), bSingleExec ? "" : resultBest);
                optStat.addStat(optName, size, nRow, 5);
            }
            else
                row.AddRow(-1, optName, timer, calcs.ToString(), "-", "-", resultValue.ToString(), bSingleExec ? "" : resultBest);
        }
    }
}