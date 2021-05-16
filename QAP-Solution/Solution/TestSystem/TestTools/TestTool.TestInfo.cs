using System;
using System.IO;

namespace TestSystem
{
    public class CTestInfo
    {
        public struct SExam
        {
            public SExam(long val = 0, bool ok = false){ bInit = ok; value = val; }
            public bool isInit() => bInit;
            public long val() => value;
            long value;
            bool bInit;
        }

        public string pathProblem { get; }
        SExam val;

        public CTestInfo(string problem, string resultPath = "")
        {
            pathProblem = problem;
            if(resultPath == "")
                val = new SExam();
            else
            {
                StreamReader file = new StreamReader(resultPath);
                string str = file.ReadToEnd();
                file.Close();
                str.Trim(' ');
                str = str.Replace("\r\n", "\n");
                string[] strSplitN = str.Split('\n');
                string[] strSplitNSpace = strSplitN[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                val = new SExam(Convert.ToInt64(strSplitNSpace[1]), true);
            }
        }
        public bool exam(ref long obj)
        {
            obj = val.val();
            return val.isInit();
        }
        public string nameProblem() => pathProblem.Substring(pathProblem.LastIndexOf("\\") + 1, pathProblem.LastIndexOf('.') - pathProblem.LastIndexOf("\\") - 1);
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
