using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public class CTestInfoLSA : CTestInfo
    {
        public List<IPermutation> m_aPerm;
        public CTestInfoLSA(string problem, string resultPath = "") : base(problem, resultPath) { m_aPerm = new List<IPermutation>(); }
    }

    public class CTestLSA2 : ATest
    {
        string m_curOpt;
        public CTestLSA2(string path, int count, bool bm_log) : base(path, count, bm_log) { m_curOpt = ""; }

        protected override IOptions GetOptionsAlg(string path) => new CLocalSearchAlgorithm.Options(path);
        protected override string GetAlgName() => CLocalSearchAlgorithm.Name(true);
        protected override ITestInfo CreateTestInfo(string problem, string result) => new CTestInfoLSA(problem, result);
        protected override void InitLogger()
        {
            // create logger
            string pathLog = m_path + "logs\\";
            m_log = new CLogger(pathLog, $"{m_xmlName}_{GetAlgName()}_{m_curOpt}");

            // create tabler
            string pathTable = m_path + "results\\";
            string pathTemplate = m_path + "template.xml";
            m_tbl = new CTablerExcel(pathTable, $"{m_xmlName}_{GetAlgName()}_{m_curOpt}", pathTemplate);
        }

        public override void Start()
        {
            Init();
            List<IPermutation> aPermToTest = new List<IPermutation>();
            for(int iOpt = 0; iOpt < m_aOptions.Count; iOpt++)
            {
                m_curOpt = iOpt == 0 ? "A" : "B";
                InitLogger();
                var curOption = m_aOptions[iOpt];

                m_log.Msg($"Option {curOption.Name()} start", true);
                CTimer timer = new CTimer();
                var aOptStat = new List<CTestStatistic>();
                aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
                aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
                aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));
                if(m_nCount == 1)
                    m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", "Timer, ms", "Calc count", "Error", "Error, %", "Result", "Optimal");
                foreach(ITestInfo test in m_aTest)
                {
                    m_log.Msg($"Test {test.Name()} started", true);
                    string timeLoad = timer.Stop().ToString();
                    long examVal = 0;
                    bool bExam = test.Exam(ref examVal);
                    CQAProblem QAP = new CQAProblem(test.pathProblem);
                    if(m_nCount > 1)
                    {
                        m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {QAP.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
                        m_tbl.AddRow();
                        m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Iteration", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                    }
                    IDelayedRow row = new CDelayedRow(m_tbl);
                    for(int i = 0; i < m_nCount; i++)
                    {
                        timer.Reset();
                        IAlgorithm ALG = new CLocalSearchAlgorithm(QAP);
                        EnableLog(QAP, ALG);
                        timer.Reset();
                        QAP.Size();

                        // use single permutation for one test in all options
                        {
                            CTestInfoLSA t = (CTestInfoLSA)test;
                            if(t.m_aPerm.Count <= i)
                                t.m_aPerm.Add(QAP.GetRandomPermutation());
                            IPermutation p = t.m_aPerm[i];
                            ((CLocalSearchAlgorithm.Options)curOption).m_p = p.Clone();
                        }

                        // start alg with permutation from test
                        IResultAlg result = ALG.Start(curOption);

                        long timerAlg = timer.Stop();
                        long calcCount = result.GetCalcCount();
                        long curRes = result.GetResultValue();
                        long resultValue = curRes;
                        long resultBest = 0;
                        if(resultBest == 0 || resultBest > curRes)
                            resultBest = curRes;

                        m_log.Msg($"Problem {test.Name()}; iteration: {i} done", true);
                        m_log.Msg($"Problem {test.Name()}; iteration: {i}, log:{ALG})");
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
                                        optStat.AddStat("-", QAP.Size(), nRow);
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
                                        optStat.AddStat("-", QAP.Size(), nRow);
                                }
                            }
                            else
                                row.AddRow(-1, i.ToString(), timerAlg.ToString(), calcCount.ToString(), "-", "-", resultValue.ToString());
                        }
                    }
                    row.Release();
                    if(m_nCount > 1)
                    {
                        m_tbl.AddRow();
                        m_tbl.AddRow();
                    }
                }
                foreach(var optStat in aOptStat)
                    optStat.ReleaseOptStat(m_tbl);
                m_log.Close();
                m_tbl.Close();
            }
        }
    }
}