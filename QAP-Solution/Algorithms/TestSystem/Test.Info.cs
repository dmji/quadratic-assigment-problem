using System;
using System.Collections.Generic;
using Problem;
using System.IO;
using System.Xml;


namespace Algorithms
{
    public partial class TestSystem
    {
        class TestInfo
        {
            public string pathProblem { get; }
            ulong resultExam;
            bool bExamExist;

            public TestInfo(string problem, string resultPath = "")
            {
                pathProblem = problem;
                if(resultPath=="")
                {
                    resultExam = 0;
                    bExamExist = false;
                }
                else
                {
                    StreamReader file = new StreamReader(resultPath);
                    string str = file.ReadToEnd();
                    while(str.Contains("  "))
                        str = str.Replace("  ", " ");
                    resultExam = Convert.ToUInt64(str.Split('\n')[0].Split(' ')[1]);
                    bExamExist = true;
                }
            }
        }
    }
}
