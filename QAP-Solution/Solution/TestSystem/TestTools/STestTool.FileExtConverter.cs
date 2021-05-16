using System.IO;
using System.Collections.Generic;

namespace TestSystem
{
    public partial struct STestTools
    {
        public static bool FileExtConverter(List<string> aPath, string oldExt, string newExt)
        {
            List<string> aRes = new List<string>();
            foreach(string resultPath in aPath)
            {
                StreamReader file = new StreamReader(resultPath);
                string str = file.ReadToEnd();

                string convName = resultPath.Replace(oldExt, newExt);
                if(!File.Exists(convName))
                    File.Create(convName).Close();
                
                StreamWriter converter = new StreamWriter(convName);
                converter.Write(str);
                converter.Close();

                aRes.Add(convName);
                File.Delete(resultPath);
            }

            if(aRes.Count == 0)
                return false;
            
            aPath.Clear();
            aPath.InsertRange(0, aRes);
            return true;
        }
    }
}
