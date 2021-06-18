using System;
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

    public abstract class ALoggerContainer
    {
        protected ILogger m_log;
        public void SetLogger(ILogger log = null) { m_log = log; }
        protected bool Msg(string s) => m_log.Msg(s, true);
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
                string pathLog = $"{path}{name}_{TestSystem.CTimer.DataTime()}_log.~.txt";
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
