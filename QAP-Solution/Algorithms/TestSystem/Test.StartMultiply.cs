using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Problem;
using Util;
using System.IO;
using System.Xml;


namespace Algorithms
{
    public partial class TestSystem
    {
        public static void StartTestEvalutionMultiply(int count, string path)
        {
            Logger log = null;
            ITabler tbl = null;
            List<TestInfo> aTest = null;
            List<IOptions> aOptions = null;
            initEvalution(path, ref aTest, ref aOptions, ref log, ref tbl);
            Timer timer = new Timer();

            writeHeader(tbl, aOptions);
            foreach(IOptions opt in aOptions)
                ((EvalutionAlgorithm.Options)opt).U_SEEDi = -1;
            OptionsStatistic optStat = new OptionsStatistic();
            foreach(TestInfo test in aTest)
            {
                timer.Reset();
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                string timeLoad = timer.Stop().ToString();
                long examVal = test.exam();
                tbl.addCells("bold", "Name problem", test.nameProblem(), $"Size: {QAP.size()}", $"Load time: {timeLoad}", "Optimal:", test.isExamed() ? examVal.ToString() : "UNDEFINED");
                tbl.addRow();
                tbl.addCells("bold", "Option set", "Avg Timer, ms", "Avg Calc count", "Avg Error", "Avg Error, %", "Avg Result", "Best Result");
                Algorithm ALG = new EvalutionAlgorithm(QAP);

                //ALG.setLogger(log);
                //QAP.setLogger(log);
                foreach(IOptions opt in aOptions)
                {
                    long timerAlg = 0;
                    long calcCount = 0;
                    long resultValue = 0;
                    long resultBest = 0;
                    for(int i = 0; i < count; i++)
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
                    double avgTimerAlg = timerAlg / count;
                    double avgCalcCount = calcCount / count;
                    double avgResultValue = resultValue / count;

                    log.msg($"On opt: {opt.getName()} problem {test.nameProblem()} log:{ALG})");
                    if(test.isExamed())
                    {
                        double err = avgResultValue - examVal;
                        double errPersent = (err / ((double)examVal) * 100);
                        test.AddRow(errPersent, opt.getName(), avgTimerAlg.ToString(), avgCalcCount.ToString(), err.ToString(), errPersent.ToString(), avgResultValue.ToString(), resultBest.ToString());
                        optStat.addStat(opt.getName(), QAP.size(), errPersent);
                    }
                    else
                    {
                        tbl.addRow();
                        tbl.addCells("simple", opt.getName(), avgTimerAlg.ToString(), avgCalcCount.ToString(), "-", "-", avgResultValue.ToString(), resultBest.ToString());
                    }
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