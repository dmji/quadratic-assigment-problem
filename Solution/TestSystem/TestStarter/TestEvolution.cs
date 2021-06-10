using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public partial class CTestEvolution : ATest
    {
        public CTestEvolution(string path, int count, bool bLog) : base(path, count, bLog) {}

        protected override IOptions GetOptionsAlg(string path) => new CEvolutionAlgorithm.COptions(path);
        protected override string GetAlgName() => CEvolutionAlgorithm.Name(true);

        public override void Start()
        {
            Init();
            InitLogger();

            WriteOptionsHeader(m_tbl, m_aOptions);
            CTimer timer = new CTimer();

            m_aOptStat = new List<CTestStatistic>();
            m_aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
            m_aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
            m_aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));

            foreach(CTestInfo test in m_aTest)
            {
                timer.Reset();
                m_problem.Deserialize(test.pathProblem);
                string timeLoad = timer.Stop().ToString();

                long examVal = 0;
                bool bExam = test.Exam(ref examVal);
                m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {m_problem.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                m_tbl.AddRow();
                if(m_nCount == 1)
                    m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                else
                    m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                
                IAlgorithm ALG = new CEvolutionAlgorithm(m_problem);
                IDelayedRow row = new CDelayedRow(m_tbl, true);
                EnableLog(m_problem, ALG);
                foreach(IOptions opt in m_aOptions)
                {
                    string optName = opt.Name();
                    long timerAlg = 0, calcCount = 0, resultValue = 0, resultBest = 0;
                    for(int i = 0; i < m_nCount; i++)
                    {
                        timer.Reset();
                        ALG.Start(opt);

                        timerAlg += timer.Stop();
                        calcCount += ALG.GetCalcCount();

                        long curRes = ALG.GetResultValue();
                        resultValue += curRes;
                        if(resultBest == 0 || resultBest > curRes)
                            resultBest = curRes;

                        m_log.Msg($"On opt: {optName} problem {test.Name()} Iteration: {i}", true);
                    }
                    double avgTimerAlg = timerAlg / m_nCount;
                    double avgCalcCount = calcCount / m_nCount;
                    double avgResultValue = resultValue / m_nCount;

                    m_log.Msg($"On opt: {optName} problem {test.Name()} log:{ALG})");
                    if(bExam)
                    {
                        double err = avgResultValue - examVal;
                        double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                        long nRow = row.AddRow(errPersent, optName, avgTimerAlg.ToString(), avgCalcCount.ToString(), err.ToString(), errPersent.ToString(), avgResultValue.ToString(), m_nCount == 1 ? "" : resultBest.ToString());
                        if(m_aOptStat != null)
                        {
                            foreach(var optStat in m_aOptStat)
                                optStat.AddStat(optName, m_problem.Size(), nRow);
                        }
                    }
                    else
                        row.AddRow(-1, optName, avgTimerAlg.ToString(), avgCalcCount.ToString(), "-", "-", avgResultValue.ToString(), m_nCount == 1 ? "" : resultBest.ToString());
                }
                row.Release();
                m_tbl.AddRow();
                m_tbl.AddRow();
            }
            Close();
        }
    }
}