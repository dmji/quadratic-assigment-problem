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
        public static void StartTestEvalutionSingle(string path)
        {
            Logger log = null;
            ITabler tbl = null;
            List<TestInfo> aTest = null;
            List<IOptions> aOptions = null;
            initEvalution(path, ref aTest, ref aOptions, ref log, ref tbl);
            Timer timer = new Timer();

            writeHeader(tbl, aOptions);
            foreach(TestInfo test in aTest)
            {
                timer.Reset();
                CQAPProblem QAP = new CQAPProblem(test.pathProblem);
                string timeLoad = timer.Stop().ToString();
                long examVal = test.exam();
                tbl.addCells("bold", "Name problem", test.nameProblem(), $"Size: {QAP.size()}",$"Load time: {timeLoad}","Optimal:", test.isExamed() ? examVal.ToString() : "UNDEFINED");
                tbl.addRow();
                tbl.addCells("bold", "Option set", "Timer, ms", "Calc count", "Error", "Error, %", "Result");
                Algorithm ALG = new EvalutionAlgorithm(QAP);

                //ALG.setLogger(log);
                //QAP.setLogger(log);
                foreach(IOptions opt in aOptions)
                {
                    timer.Reset();
                    ALG.Start(opt);
                    string timerAlg = timer.Stop().ToString();
                    log.msg($"On opt: {opt.getName()} problem {test.nameProblem()} log:{ALG})");
                    if(test.isExamed())
                    {
                        double err = ALG.getResultValue() - examVal;
                        double errPersent = (err / ((double)examVal) * 100);
                        test.AddRow(errPersent, opt.getName(), timerAlg, ALG.getCalcCount().ToString(), err.ToString(), errPersent.ToString(), ALG.getResultValue().ToString());
                    }
                    else
                    {
                        tbl.addRow();
                        tbl.addCells("simple", opt.getName(), timerAlg, ALG.getCalcCount().ToString(), "-", "-", ALG.getResultValue().ToString());
                    }
                }
                test.RelaseRow(tbl);
                tbl.addRow();
                tbl.addRow();
            }
            log.Close();
            tbl.Close();
        }
    }
}
