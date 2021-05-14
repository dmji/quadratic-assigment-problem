using System.Collections.Generic;
using Solution.Util;

namespace Solution
{
    public partial class TestSystem
    {
        public static void StartTestEvolution(string path, int reply_count = 1)
        {
            Logger log = null;
            ITabler tbl = null;
            List<TestInfo> aTest = null;
            List<IOptions> aOptions = null;
            initEvolution(path, ref aTest, ref aOptions, ref log, ref tbl);
            Timer timer = new Timer();

            if(reply_count > 1)
            {
                foreach(IOptions opt in aOptions)
                    ((EvolutionAlgorithm.Options)opt).U_SEEDi = -1;
            }

            writeHeader(tbl, aOptions);
            
            OptionsStatistic optStat = new OptionsStatistic();
            foreach(TestInfo test in aTest)
            {
                timer.Reset();
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                string timeLoad = timer.Stop().ToString();
                tbl.addCells("bold", "Name problem", test.nameProblem(), $"Size: {QAP.size()}", $"Load time: {timeLoad}", "Optimal:", test.isExamed() ? test.exam().ToString() : "UNDEFINED");
                tbl.addRow();
                if(reply_count == 1)
                    tbl.addCells("bold", "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                else
                    tbl.addCells("bold", "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                IAlgorithm ALG = new EvolutionAlgorithm(QAP);

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
                    addResult(test, opt.getName(), avgTimerAlg.ToString(), avgCalcCount, avgResultValue, reply_count == 1, optStat, resultBest.ToString(), QAP.size());
                }

                test.RelaseRow(tbl);
                tbl.addRow();
                tbl.addRow();
            }
            optStat.releaseOptStat(tbl);
            log.Close();
            tbl.Close();
        }
    }
}