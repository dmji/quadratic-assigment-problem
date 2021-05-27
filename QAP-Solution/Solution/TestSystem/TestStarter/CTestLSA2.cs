using System.Collections.Generic;
using System.Xml;
using Solution;

namespace TestSystem
{
    public class CTestLSA2 : ATest
    {
        string m_curOpt;
        public CTestLSA2(string path, int count, bool bm_log) : base(path, count, bm_log) { }

        protected override IOptions GetOptionsAlg(string path) => new CLocalSearchAlgorithm.Options(path);
        protected override string GetAlgName() => CLocalSearchAlgorithm.Name(true);
        protected override void InitLogger()
        {
            // create logger
            string pathLog = m_path + "logs\\";
            m_log = new CLogger(pathLog, $"{m_xmlName}_{GetAlgName()}");

            // create tabler
            string pathTable = m_path + "results\\";
            string pathTemplate = m_path + "template.xml";
            m_tbl = new CTablerExcel(pathTable, $"{m_xmlName}_{GetAlgName()}_{m_curOpt}", pathTemplate);
        }

        //bool Proc()
        //{
        //    for(int iOpt = 0; iOpt < m_aOptions.Count; iOpt++)
        //    {
        //        m_curOpt = iOpt == 0 ? "A" : "B";
        //        InitLogger();
        //        m_log.Msg($"Option {m_aOptions[iOpt].Name()} start", true);

        //        m_aOptStat = new List<CTestStatistic>();
        //        m_aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
        //        m_aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
        //        m_aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));
        //        if(m_nCount == 1)
        //            m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", "Timer, ms", "Calc count", "Error", "Error, %", "Result", "Optimal");
        //        foreach(CTestInfo test in m_aTest)
        //        {
        //            m_log.Msg($"Test {test.Name()} started", true);
        //            string timeLoad = timer.Stop().ToString();
        //            long examVal = 0;
        //            bool bExam = test.Exam(ref examVal);
        //            CQAPProblem QAP = new CQAPProblem(test.pathProblem);
        //            if(m_nCount > 1)
        //            {
        //                m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {QAP.Size()}", $"Load time: {timeLoad}", "Optimal:", bExam ? examVal.ToString() : "");
        //                m_tbl.AddRow();
        //                m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Iteration", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
        //            }
        //            IDelayedRow row = new CDelayedRow(m_tbl);
        //            for(int i = 0; i < m_nCount; i++)
        //            {
        //                timer.Reset();
        //                IAlgorithm ALG = new CLocalSearchAlgorithm(QAP);
        //                EnableLog(QAP, ALG);

        //                timer.Reset();
        //                IResultAlg result = ALG.Start(m_aOptions[iOpt]);

        //                long timerAlg = timer.Stop();
        //                long calcCount = result.GetCalcCount();
        //                long curRes = result.GetResultValue();
        //                long resultValue = curRes;
        //                long resultBest = 0;
        //                if(resultBest == 0 || resultBest > curRes)
        //                    resultBest = curRes;

        //                m_log.Msg($"Problem {test.Name()}; iteration: {i} done", true);
        //                m_log.Msg($"Problem {test.Name()}; iteration: {i}, m_log:{ALG})");
        //                if(m_nCount == 1)
        //                {
        //                    if(bExam)
        //                    {
        //                        double err = resultValue - examVal;
        //                        double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
        //                        long nRow = row.AddRow(errPersent, test.Name(), timerAlg.ToString(), calcCount.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString(), examVal.ToString());
        //                        if(m_aOptStat != null)
        //                        {
        //                            foreach(var optStat in m_aOptStat)
        //                                optStat.AddStat(QAP.Size().ToString(), QAP.Size(), nRow);
        //                        }
        //                    }
        //                    else
        //                        row.AddRow(-1, i.ToString(), timerAlg.ToString(), calcCount.ToString(), "-", "-", resultValue.ToString(), "-");
        //                }
        //                else
        //                {
        //                    if(bExam)
        //                    {
        //                        double err = resultValue - examVal;
        //                        double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
        //                        long nRow = row.AddRow(errPersent, i.ToString(), timerAlg.ToString(), calcCount.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString());
        //                        if(m_aOptStat != null)
        //                        {
        //                            foreach(var optStat in m_aOptStat)
        //                                optStat.AddStat(QAP.Size().ToString(), QAP.Size(), nRow);
        //                        }
        //                    }
        //                    else
        //                        row.AddRow(-1, i.ToString(), timerAlg.ToString(), calcCount.ToString(), "-", "-", resultValue.ToString());
        //                }
        //            }
        //            row.Release();
        //            if(m_nCount > 1)
        //            {
        //                m_tbl.AddRow();
        //                m_tbl.AddRow();
        //            }
        //        }
        //        foreach(var optStat in m_aOptStat)
        //            optStat.ReleaseOptStat(m_tbl);
        //        m_log.Close();
        //        m_tbl.Close();
        //        return true;
        //    }


        public override void Start()
        {
            Init();
            CTimer timer = new CTimer();
            CLogger log = (CLogger)m_log;

            //if(bMultithreading)
            //{
            //    System.Threading.Tasks.Parallel.For(0, m_aOptions.Count,);
            //}
            //else
            //{
                for(int iOpt = 0; iOpt < m_aOptions.Count; iOpt++)
                {
                    m_curOpt = iOpt == 0 ? "A" : "B";
                    InitLogger();
                    m_log.Msg($"Option {m_aOptions[iOpt].Name()} start", true);

                    m_aOptStat = new List<CTestStatistic>();
                    m_aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
                    m_aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
                    m_aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));
                    if(m_nCount == 1)
                        m_tbl.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", "Timer, ms", "Calc count", "Error", "Error, %", "Result", "Optimal");
                    foreach(CTestInfo test in m_aTest)
                    {
                        m_log.Msg($"Test {test.Name()} started", true);
                        string timeLoad = timer.Stop().ToString();
                        long examVal = 0;
                        bool bExam = test.Exam(ref examVal);
                        CQAPProblem QAP = new CQAPProblem(test.pathProblem);
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
                            IResultAlg result = ALG.Start(m_aOptions[iOpt]);

                            long timerAlg = timer.Stop();
                            long calcCount = result.GetCalcCount();
                            long curRes = result.GetResultValue();
                            long resultValue = curRes;
                            long resultBest = 0;
                            if(resultBest == 0 || resultBest > curRes)
                                resultBest = curRes;

                            m_log.Msg($"Problem {test.Name()}; iteration: {i} done", true);
                            m_log.Msg($"Problem {test.Name()}; iteration: {i}, m_log:{ALG})");
                            if(m_nCount == 1)
                            {
                                if(bExam)
                                {
                                    double err = resultValue - examVal;
                                    double errPersent = examVal != 0 ? (err / ((double)examVal) * 100) : 1000;
                                    long nRow = row.AddRow(errPersent, test.Name(), timerAlg.ToString(), calcCount.ToString(), err.ToString(), errPersent.ToString(), resultValue.ToString(), examVal.ToString());
                                    if(m_aOptStat != null)
                                    {
                                        foreach(var optStat in m_aOptStat)
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
                                    if(m_aOptStat != null)
                                    {
                                        foreach(var optStat in m_aOptStat)
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
                            m_tbl.AddRow();
                            m_tbl.AddRow();
                        }
                    }
                    foreach(var optStat in m_aOptStat)
                        optStat.ReleaseOptStat(m_tbl);
                    m_log.Close();
                    m_tbl.Close();
                //}
            }
        }
    }
}