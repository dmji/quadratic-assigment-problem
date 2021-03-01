using System;
using Algorithms;

namespace Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] path = {"C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_all.xml",    //0
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_chr.xml",      //1
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_els.xml",      //2
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_esc.xml",      //3
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_had.xml",      //4
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_kra.xml",      //5
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_lipa.xml",     //6
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_nug.xml",      //7
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_rou.xml",      //8
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_sko.xml",      //9
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_src.xml",      //10
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_ste.xml",      //11
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_tai.xml",      //12
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_tho.xml",      //13
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_wil.xml",      //14
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_bur.xml",    //15
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_ff.xml" };    //16


            //for(int i = 0; i < 15; i++)
                TestSystem.StartTestEvalution(path[0],20);
            
            //fullforce for size <15
            //TestSystem.StartTestFullforce(path[16]);
            
            //performance check - "nug"
            //TestSystem.StartTestProblemPerformance(path[7]);
        }
    }
}
