using System;
using System.IO;

namespace TestSystem
{
    public interface ITestInfo
    {
        bool Exam(ref long obj);
        string Name();
        int Size();
        string pathProblem { get; }
    }

    public class CTestInfo : ITestInfo
    {
        public struct SExam
        {
            public SExam(int size, long val = 0, bool ok = false){ m_size = size; m_bInit = ok; m_value = val; }
            public bool IsInit() => m_bInit;
            public long Value() => m_value;
            long m_value;
            bool m_bInit;
            public int m_size;
        }

        public string pathProblem { get; }
        SExam m_val;

        public CTestInfo(string problem, string resultPath = "")
        {
            pathProblem = problem;
            if(resultPath == "" || resultPath == null)
                m_val = new SExam();
            else
            {
                string str = new CFile(resultPath).ReadToEnd();
                str.Trim(' ');
                str = str.Replace("\r\n", "\n");
                string[] strSplitN = str.Split('\n');
                string[] strSplitNSpace = strSplitN[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                m_val = new SExam(Int32.Parse(strSplitNSpace[0]), Convert.ToInt64(strSplitNSpace[1]), true);
            }
        }
        public int Size() => m_val.IsInit() ? m_val.m_size : -1;
        public bool Exam(ref long obj)
        {
            obj = m_val.Value();
            return m_val.IsInit();
        }
        public string Name() => pathProblem.Substring(pathProblem.LastIndexOf("\\") + 1, pathProblem.LastIndexOf('.') - pathProblem.LastIndexOf("\\") - 1);
    }
}
