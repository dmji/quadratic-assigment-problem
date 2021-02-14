using System;
using System.IO;

namespace Util
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
                string pathLog = $"{path}{sAlg}_{time}_log.~.txt";
                if(!System.IO.File.Exists(pathLog))
                    System.IO.File.Create(pathLog).Close();
                m_s = new StreamWriter(pathLog);
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
