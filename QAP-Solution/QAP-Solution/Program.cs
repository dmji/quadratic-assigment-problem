using System;
using Algorithms;

namespace Problem
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] path = {"C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_all.xml",    //0
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_chr.xml",      //1 bad
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_els.xml",      //2 
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_esc.xml",      //3 bad
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_had.xml",      //4 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_kra.xml",      //5 bad
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_lipa.xml",     //6 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_nug.xml",      //7 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_rou.xml",      //8 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_sko.xml",      //9 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_scr.xml",      //10 bad
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_ste.xml",      //11 bad
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_tai.xml",      //12 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_tho.xml",      //13 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_wil.xml",      //14 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_bur.xml",      //15 ok
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_ff.xml",       //16
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_eva_test_little.xml",         //17
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_eva_test_big.xml",     //18
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\badPROB.xml", // 19
                            "C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\goodPROB.xml"};         //20


            //for(int i = 0; i < 15; i++)
            TestSystem.StartTestEvalution(path[19], 20);
            TestSystem.StartTestEvalution(path[20], 20);
            //TestSystem.StartTestEvalution(path[15], 20);
            
            //fullforce for size <15
            //TestSystem.StartTestFullforce(path[16]);
            
            //performance check - "nug"
            //TestSystem.StartTestProblemPerformance(path[7]);
        }
    }
}
