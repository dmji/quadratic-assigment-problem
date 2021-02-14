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
                  
                    if(resultPath.Contains(".sln"))
                    {
                        string convName = resultPath.Replace(".sln", ".bin");
                        if(!System.IO.File.Exists(convName))
                            System.IO.File.Create(convName).Close();
                        StreamWriter converter = new StreamWriter(convName);
                        converter.Write(str);
                        converter.Close();
                    }

                    file.Close();
                    str.Trim(' ');
                    str = str.Replace("\r\n", "\n");
                    string[] strSplitN = str.Split('\n');
                    string[] strSplitNSpace = strSplitN[0].Split(' ',StringSplitOptions.RemoveEmptyEntries);
                    resultExam = Convert.ToUInt64(strSplitNSpace[1]);
                    bExamExist = true;
                }
            }
            public string exam() => resultExam.ToString();
            public string nameProblem() => pathProblem.Substring(pathProblem.LastIndexOf("\\"), pathProblem.LastIndexOf('.')- pathProblem.LastIndexOf("\\")+1);
        }
    }
}
