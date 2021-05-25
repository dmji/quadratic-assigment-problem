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

    public class CEmptyLogger : ILogger
    {
        public CEmptyLogger() { }
        public bool Close() => false;
        public bool Msg(string str, bool bForceConsole = false) => false;
    }

    public class CLogger : ILogger
    {
        System.Threading.Thread m_thread;
        string m_log="";
        StreamWriter m_stream;
        bool m_bThreadOk;

        public CLogger() {}
        public CLogger(string path, string name) 
        {
            if(path.Length > 0 && name.Length > 0)
            {
                if(m_stream != null)
                    m_stream.Close();
                string time = DateTime.Now.ToString().Replace(":", "_").Replace(" ", "_").Replace(".", "_");
                string pathLog = $"{path}{name}_{time}_log.~.txt";
                if(!System.IO.File.Exists(pathLog))
                    System.IO.File.Create(pathLog).Close();
                m_stream = new StreamWriter(pathLog);
                m_thread = new System.Threading.Thread(ThreadWorker);
                m_bThreadOk = true;
                m_thread.Start();
            }
            if(m_stream == null)
                throw new Exception("Need init w/ params");
        }

        public bool Msg(string str, bool bForceConsole=false)
        {
            lock(m_log)
            {
                m_log += $"{DateTime.Now} {str}\n";
            } 
            if(bForceConsole)
                Console.WriteLine($"{DateTime.Now} {str}");
            return true;
        }
        public bool Close()
        {
            m_bThreadOk = false;
            while(m_thread.IsAlive) System.Threading.Thread.Sleep(150);
            if(m_stream != null)
            {
                m_stream.Close();
                return true;
            }
            return false;
        }

        void ThreadWorker()
        {
            while(m_bThreadOk || m_log.Length>0)
            {
                if(m_log.Length > 0)
                {
                    lock(m_log)
                    {
                        m_stream.Write(m_log);
                        m_log = "";
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        ~CLogger()
        {
            Close();
        }
    }
}
