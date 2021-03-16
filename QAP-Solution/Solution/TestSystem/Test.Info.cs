using System;
using System.IO;

namespace Solution
{
    public partial class TestSystem
    {
        partial class TestInfo
        {
            public string pathProblem { get; }
            long resultExam;
            bool bExamExist;

            public TestInfo(string problem, string resultPath = "")
            {
                pathProblem = problem;
                resetStats();
                if(resultPath=="")
                {
                    resultExam = 0;
                    bExamExist = false;
                }
                else
                {
                    StreamReader file = new StreamReader(resultPath);
                    string str = file.ReadToEnd();
                    file.Close();
                    str.Trim(' ');
                    str = str.Replace("\r\n", "\n");
                    string[] strSplitN = str.Split('\n');
                    string[] strSplitNSpace = strSplitN[0].Split(' ',StringSplitOptions.RemoveEmptyEntries);
                    resultExam = Convert.ToInt64(strSplitNSpace[1]);
                    bExamExist = true;
                }
            }
            public long exam() => resultExam;
            public bool isExamed() => bExamExist;

            public string nameProblem() => pathProblem.Substring(pathProblem.LastIndexOf("\\")+1, pathProblem.LastIndexOf('.')- pathProblem.LastIndexOf("\\")-1);

            public void generateResultFile(string path, int size, long result, string perm)
            {
                if(!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                string resFilePath = path + nameProblem() + ".bin";
                if(!System.IO.File.Exists(resFilePath))
                    System.IO.File.Create(resFilePath).Close();
                StreamWriter wr = new StreamWriter(resFilePath);
                wr.Write($"{size} {result}\n{perm.Substring(0, perm.IndexOf(':'))}");
                wr.Close();
            }
        }
    }
}
