using System;
using System.IO;

namespace Log
{
    public class Logger
    {
        StreamWriter m_s;
        public Logger(string path, string name) { init(path,name); }

        public Func<string,bool> init(string path="", string sAlg = "")
        {
            if(path.Length>0 && sAlg.Length>0)
            {
                if(m_s != null)
                    m_s.Close();
                string time = DateTime.Now.ToString();
                time = time.Replace(":", "_");
                time = time.Replace(" ", "_");
                time = time.Replace(".", "_");
                m_s = new StreamWriter($"{path}{sAlg}_{time}_log.~.txt");
            }
            if(m_s == null)
                throw new Exception("Need init w/ params");
            return msg;
        }

        public bool msg(string str)
        {
            m_s.WriteLine($"{DateTime.Now} {str}");
            Console.WriteLine($"{DateTime.Now} {str}");
            return true;
        }

        public bool Close()
        {
            if(m_s != null)
            {
                m_s.Close();
                return true;
            }
            return false;
        }
    }
}
