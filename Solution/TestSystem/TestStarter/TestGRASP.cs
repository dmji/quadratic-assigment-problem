using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public partial class CTestGRASP : ATest
    {
        public CTestGRASP(string path, int count, bool bLog) : base(path, count, bLog) {}

        protected override IOptions GetOptionsAlg(string path) => new CEvolutionAlgorithm.COptions(path);
        protected override string GetAlgName() => CEvolutionAlgorithm.Name(true);

        public override void Start()
        {
            Init();
            ITabler table = null;
            InitLogger(table);
            WriteOptionsHeader(table, m_aOptions);
            CTimer timer = new CTimer();

            m_aOptStat = new List<CTestStatistic>();
            m_aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
            m_aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
            m_aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));

            SetLogger(m_problem);

            foreach(CTestInfo test in m_aTest)
            {
                timer.Reset();
                if(!m_problem.Deserialize(test.pathProblem))
                    continue;
                string timeLoad = timer.Stop().ToString();

                long examVal = 0;
                bool bExam = test.Exam(ref examVal);

                var hrow = table.AddRow();
                hrow.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {m_problem.Size()}", $"Load time: {timeLoad}", "Optimal + Worst:", bExam ? examVal.ToString() : "");
                var trow = table.AddRow();
                if(m_nCount == 1)
                    trow.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                else
                    trow.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                
                IAlgorithm ALG = new CEvolutionAlgorithm(m_problem);
                IAlgorithm ALG_LSA = new CLocalSearchAlgorithm(m_problem);
                IDelayedRow row = new CDelayedRow(table, true);
                SetLogger(ALG_LSA);
                SetLogger(ALG);
                long resultBestOverall = 0;
                foreach(IOptions opt in m_aOptions)
                {
                    string optName = opt.Name();
                    long timerAlg = 0, calcCount = 0, resultValue = 0, resultBest = 0;
                    for(int i = 0; i < m_nCount; i++)
                    {
                        timer.Reset();

                        // start evolution
                        ALG.Start(opt);

                        // start lsa
                        CLocalSearchAlgorithm.Options optLSA = new CLocalSearchAlgorithm.Options();
                        optLSA.m_p = ALG.Result;
                        optLSA.B_FULLIFY = true;
                        ALG_LSA.Start(optLSA);

                        // statistic
                        timerAlg += timer.Stop();
                        calcCount += ALG.GetCalcCount();

                        long curRes = ALG.GetResultValue();
                        resultValue += curRes;
                        if(resultBest < curRes)
                            resultBest = curRes;
                        if(m_log != null)
                            m_log.Msg($"On opt: {optName} problem {test.Name()} Iteration: {i}", true);
                    }
                    double avgTimerAlg = timerAlg / m_nCount;
                    double avgCalcCount = calcCount / m_nCount;
                    double avgResultValue = resultValue / m_nCount;
                    //if(m_log != null)
                    //  m_log.Msg($"On opt: {optName} problem {test.Name()} log:{ALG})");
                    //if(bExam)
                    //{
                    //    double err = avgResultValue - examVal;
                    //    double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : avgResultValue == 0 ? 0 : 1000;
                    //    long nRow = row.AddRow(errPersent, optName, avgTimerAlg.ToString(), avgCalcCount.ToString(), err.ToString(), errPersent.ToString(), avgResultValue.ToString(), m_nCount == 1 ? "" : resultBest.ToString());
                    //    if(m_aOptStat != null)
                    //    {
                    //        foreach(var optStat in m_aOptStat)
                    //            optStat.AddStat(optName, m_problem.Size(), nRow);
                    //    }
                    //}
                    //else
                    //    row.AddRow(-1, optName, avgTimerAlg.ToString(), avgCalcCount.ToString(), "-", "-", avgResultValue.ToString(), m_nCount == 1 ? "" : resultBest.ToString());

                    if(resultBestOverall == 0 || resultBestOverall < resultBest)
                        resultBestOverall = resultBest;
                }

                var file = new CFile(test.pathProblem + ".exam");
                var buf = file.ReadToEnd();
                buf = buf.Split('\n', System.StringSplitOptions.RemoveEmptyEntries)[0];
                buf= buf.Replace('\n', ' ').Trim();
                buf = buf + " " + resultBestOverall;
                file.WriteTotal(buf);
                row.Release(table);
                table.AddRow();
                table.AddRow();
            }
            Close(table);
        }
    }
}