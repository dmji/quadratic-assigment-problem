using System;
using System.IO;

namespace TestSystem
{
    public interface IClose
    {
        bool Close();
    }

    public interface ILogger : IClose
    {
        Func<string, bool> init(string path = "", string sAlg = "");
        bool msg(string str);
    }

    public class CLogger : ILogger
    {
        System.Threading.Thread m_thread;
        string m_log="";
        StreamWriter m_stream;
        bool m_bThreadOk;
        public CLogger() {}
        public CLogger(string path, string name) { init(path,name); }

        public Func<string,bool> init(string path="", string sAlg = "")
        {
            if(path.Length>0 && sAlg.Length>0)
            {
                if(m_stream != null)
                    m_stream.Close();
                string time = DateTime.Now.ToString();
                time = time.Replace(":", "_");
                time = time.Replace(" ", "_");
                time = time.Replace(".", "_");
                string pathLog = $"{path}{sAlg}_{time}_log.~.txt";
                if(!System.IO.File.Exists(pathLog))
                    System.IO.File.Create(pathLog).Close();
                m_stream = new StreamWriter(pathLog);
                m_thread = new System.Threading.Thread(threadWorker);
                m_bThreadOk = true;
                m_thread.Start();
            }
            if(m_stream == null)
                throw new Exception("Need init w/ params");
            return msg;
        }

        public bool msg(string str)
        {
            lock(m_log)
            {
                m_log += $"{DateTime.Now} {str}\n";
            } 
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

        void threadWorker()
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
