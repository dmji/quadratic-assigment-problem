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
        protected override void InitLogger(ITabler table)
        {
            // create logger
            string pathLog = m_path + "logs\\";
            m_log = new CLogger(pathLog, $"{m_path.GetNameExt()}_{GetAlgName()}_{m_curOpt}");

            // create tabler
            string pathTable = m_path + "results\\";
            string pathTemplate = m_path + "_template.xml";
            table = new CTablerExcel(pathTable, $"{m_path.GetNameExt()}_{GetAlgName()}_{m_curOpt}", pathTemplate);
        }

        public override void Start()
        {
            Init();
            System.Console.WriteLine("Total test " + m_aTest.Count);
            List<IPermutation> aPermToTest = new List<IPermutation>();
            for(int iOpt = 0; iOpt < m_aOptions.Count; iOpt++)
            {
                m_curOpt = iOpt == 0 ? "A" : "B";
                ITabler table = null;
                InitLogger(table);
                var curOption = m_aOptions[iOpt];

                if(m_log != null)
                    m_log.Msg($"Option {curOption.Name()} start", true);
                CTimer timer = new CTimer();
                var aOptStat = new List<CTestStatistic>();
                aOptStat.Add(new CTestStatistic("Avg Error, %", 5));
                aOptStat.Add(new CTestStatistic("Avg timer, %", 2));
                aOptStat.Add(new CTestStatistic("Avg cacl count, %", 3));
                
                foreach(ITestInfo test in m_aTest)
                {
                    if(m_log != null)
                        m_log.Msg($"Test {test.Name()} started", true);
                    string timeLoad = timer.Stop().ToString();

                    long examVal = 0;
                    bool bExam = test.Exam(ref examVal);

                    long worstVal = 0;
                    bool bWorst = test.Worst(ref worstVal);

                    if(!m_problem.Deserialize(test.pathProblem))
                        continue;
                    SetLogger(m_problem);
                    var trow= table.AddRow();
                    trow.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Name problem", test.Name(), $"Size: {m_problem.Size()}", "Optimal:", bExam ? examVal.ToString() : "", bWorst ? worstVal.ToString() : "");
                    var iHeaderRow = trow.GetIndex();

                    trow = table.AddRow();
                    trow.AddCells(CTablerExcel.Styles.eStyleSimpleBold, "Iteration", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                    
                    IDelayedRow row = new CDelayedRow(table);
                    for(int i = 0; i < m_nCount; i++)
                    {
                        timer.Reset();
                        IAlgorithm ALG = new CLocalSearchAlgorithm(m_problem);
                        SetLogger(ALG);
                        timer.Reset();

                        // use single permutation for one test in all options
                        {
                            CTestInfoLSA t = (CTestInfoLSA)test;
                            if(t.m_aPerm.Count <= i)
                                t.m_aPerm.Add(m_problem.GetRandomPermutation());
                            IPermutation p = t.m_aPerm[i];
                            ((CLocalSearchAlgorithm.Options)curOption).m_p = p.Clone();
                        }

                        // start alg with permutation from test
                        ALG.Start(curOption);

                        long timerAlg = timer.Stop();
                        long calcCount = ALG.GetCalcCount();
                        long curRes = ALG.GetResultValue();
                        long resultValue = curRes;
                        long resultBest = 0;
                        if(resultBest == 0 || resultBest > curRes)
                            resultBest = curRes;

                        if(m_log != null)
                        {
                            m_log.Msg($"Problem {test.Name()}; iteration: {i} done", true);
                            m_log.Msg($"Problem {test.Name()}; iteration: {i}, log:{ALG})");
                        }
                        string errStr = $"=RC6-R{iHeaderRow}C5";
                        string errPersentStr = $"=ЕСЛИ(И(R{iHeaderRow}C6=0;R{iHeaderRow}C5 = 0);0;100*(RC6-R{iHeaderRow}C5)/(R{iHeaderRow}C6-R{iHeaderRow}C5))";
                        long nRow = row.AddRow(resultValue,i.ToString(), timerAlg.ToString(), calcCount.ToString(), errStr, errPersentStr, resultValue.ToString());
                        if(aOptStat != null)
                        {
                            foreach(var optStat in aOptStat)
                                optStat.AddStat("-", m_problem.Size(), nRow);
                        }
                    }
                    row.Release(table);
                    if(m_nCount > 1)
                    {
                        table.AddRow();
                        table.AddRow();
                    }
                }
                foreach(var optStat in aOptStat)
                    optStat.ReleaseOptStat(table);
                m_log.Close();
                table.Close();
            }
        }
    }
}