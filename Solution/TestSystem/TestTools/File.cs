using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TestSystem
{
    public class CFile
    {
        List<string> m_delayStr;
        string m_path;

        public CFile(string path)
        {
            m_delayStr = new List<string>();
            m_path = path;
            string pathDir = m_path.Substring(0, m_path.LastIndexOf('\\'));
            if(!System.IO.File.Exists(pathDir))
                System.IO.Directory.CreateDirectory(pathDir);
            if(!System.IO.File.Exists(m_path))
                System.IO.File.Create(m_path).Close();
        }
        public void WriteTotal(string s)
        {
            StreamWriter wr = new StreamWriter(m_path);
            wr.Write(s);
            wr.Close();
        }
        public string ReadToEnd()
        {
            StreamReader rd = new StreamReader(m_path);
            string res = rd.ReadToEnd();
            rd.Close();
            return res;
        }
        public string GetPath() => m_path;
        public string GetNameExt() => m_path.Substring(m_path.LastIndexOf('\\'));
        public void Close(bool bClose = true)
        {
            StreamWriter wr = new StreamWriter(m_path, true);
            foreach(string s in m_delayStr)
                wr.Write(s);
            wr.Close();
        }
        public void Write(string s) 
        {
            m_delayStr.Add(s);
            if(m_delayStr.Count > 400)
            {
                Close(false);
            }
        }
    }
}
