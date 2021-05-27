using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public class CTestLSA2 : ATest
    {
        string m_curOpt;
        int m_iOpt;
        public CTestLSA2(string path, int count, bool bm_log) : base(path, count, bm_log) { m_curOpt = ""; }

        protected override IOptions GetOptionsAlg(string path) => new CLocalSearchAlgorithm.Options(path);
        protected override string GetAlgName() => CLocalSearchAlgorithm.Name(true);

        void StartThread()
        {
            IOptions curOption;
            ILogger log;
            ITabler tbl;
            lock(m_curOpt)
            {
                m_curOpt = m_iOpt == 0 ? "A" : "B";

                // create logger
                string pathLog = m_path + "logs\\";
                log = new CLogger(pathLog, $"{m_xmlName}_{GetAlgName()}_{m_curOpt}");

                // create tabler
                string pathTable = m_path + "results\\";
                string pathTemplate = m_path + "template.xml";
                tbl = new CTablerExcel(pathTable, $"{m_xmlName}_{GetAlgName()}_{m_curOpt}", pathTemplate);

                curOption = m_aOptions[m_iOpt];
            }
            log.Msg($"Option {curOption.Name()} start", true);
            CTimer timer = new CTimer();
            var aOptStat = new List<CTestStatistic>();
            aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
            aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
            aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));
            if(m_nCount == 1)
                tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", "Timer, ms", "Calc count", "Error", "Error, %", "Result", "Optimal");
            foreach(CTestInfo test in m_aTest)
            {
                log.Msg($"Test {test.Name()} started", true);
                string timeLoad = timer.Stop().ToString();
                long examVal = 0;
                bool bExam = test.Exam(ref examVal);
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                if(m_nCount > 1)
                {
                    tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {QAP.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                    tbl.AddRow();
                    tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Iteration", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                }
                IDelayedRow row = new CDelayedRow(tbl);
                for(int i = 0; i < m_nCount; i++)
                {
                    timer.Reset();
                    IAlgorithm ALG = new CLocalSearchAlgorithm(QAP);
                    EnableLog(QAP, ALG);
                    QAP.SetLogger(log);
                    timer.Reset();
                    IResultAlg result = ALG.Start(curOption);

                    long timerAlg = timer.Stop();
                    long calcCount = result.GetCalcCount();
                    long curRes = result.GetResultValue();
                    long resultValue = curRes;
                    long resultBest = 0;
                    if(resultBest == 0 || resultBest > curRes)
                        resultBest = curRes;

                    log.Msg($"Problem {test.Name()}; iteration: {i} done", true);
                    log.Msg($"Problem {test.Name()}; iteration: {i}, log:{ALG})");
                    if(m_nCount == 1)
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
                if(m_nCount > 1)
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
        

        public override void Start()
        {
            Init();
            List<System.Threading.Thread> t = new List<System.Threading.Thread>();
            for(int iOpt = 0; iOpt < m_aOptions.Count; iOpt++)
            {
                lock(m_curOpt)
                    m_iOpt = iOpt;
                t.Add(new System.Threading.Thread(StartThread));
                t[t.Count - 1].Start();
                System.Threading.Thread.Sleep(100);
            }
            for(int iOpt = 0; iOpt < m_aOptions.Count; iOpt++)
                t[iOpt].Join();
        }
    }
}