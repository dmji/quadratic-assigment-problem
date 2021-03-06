﻿using System;
using System.IO;

namespace TestSystem
{
    public interface ITestInfo
    {
        bool Exam(ref long obj);
        bool Worst(ref long obj);
        string Name();
        int Size();
        string pathProblem { get; }
    }

    public class CTestInfo : ITestInfo
    {
        public struct SExam
        {
            public SExam(int size, long val = 0, bool bValInit = false, long worst = 0, bool bWorstInit = false) 
            { 
                m_size = size;

                m_bInit = bValInit; 
                m_value = val;

                m_bInitWorst = bWorstInit;
                m_valueWorst = worst;
            }

            public bool IsInit() => m_bInit;
            public long Value() => m_value;
            long m_value;
            bool m_bInit;

            public bool IsInitWorst() => m_bInitWorst;
            public long WorstValue() => m_valueWorst;
            long m_valueWorst;
            bool m_bInitWorst;

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
                str = str.Replace("\r\n", "\n").Replace('\n', ' ');
                string[] strSplitNSpace = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if(strSplitNSpace.Length > 2)
                    m_val = new SExam(Int32.Parse(strSplitNSpace[0]), Convert.ToInt64(strSplitNSpace[1]), true, Convert.ToInt64(strSplitNSpace[2]), true);
                else
                    m_val = new SExam(Int32.Parse(strSplitNSpace[0]), Convert.ToInt64(strSplitNSpace[1]), true);
            }
        }

        public int Size() => m_val.IsInit() ? m_val.m_size : -1;
        public bool Exam(ref long obj)
        {
            obj = m_val.Value();
            return m_val.IsInit();
        }
        
        public bool Worst(ref long obj)
        {
            obj = m_val.WorstValue();
            return m_val.IsInitWorst();
        }
        public string Name() => pathProblem.Substring(pathProblem.LastIndexOf("\\") + 1, pathProblem.LastIndexOf('.') - pathProblem.LastIndexOf("\\") - 1);
    }
}
