﻿using System;
using System.IO;

namespace Solution
{
    public interface IClose
    {
        bool Close();
    }

    public interface ILogger : IClose
    {
        bool Msg(string str, bool bForceConsole = false);
    }

    public class CEmptyLogger : ILogger
    {
        public CEmptyLogger() { }
        public bool Close() => false;
        public bool Msg(string str, bool bForceConsole = false) => false;
    }

    public class CLogger : ILogger
    {
        TestSystem.CFile m_stream;

        public CLogger() {}
        public CLogger(string path, string name) 
        {
            if(path.Length > 0 && name.Length > 0)
            {
                if(m_stream != null)
                    m_stream.Close();
                string time = DateTime.Now.ToString().Replace(":", "_").Replace(" ", "_").Replace(".", "_").Replace("\\", "_").Replace("/", "_").Replace("-", "_");
                string pathLog = $"{path}{name}_{time}_log.~.txt";
                m_stream = new TestSystem.CFile(pathLog);
            }
            if(m_stream == null)
                throw new Exception("Need init w/ params");
        }

        public bool Msg(string str, bool bForceConsole=false)
        {
            m_stream.Write($"{DateTime.Now} {str}\n");
            if(bForceConsole)
                Console.WriteLine($"{DateTime.Now} {str}");
            return true;
        }
        public bool Close()
        {
            if(m_stream != null)
            {
                m_stream.Close();
                return true;
            }
            return false;
        }

        ~CLogger()
        {
            Close();
        }
    }
}
