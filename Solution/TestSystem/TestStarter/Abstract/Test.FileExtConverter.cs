using System.IO;
using System.Collections.Generic;

namespace TestSystem
{
    public abstract partial class ATest
    {
        public static bool FileExtConverter(List<string> aPath, string oldExt, string newExt)
        {
            List<string> aRes = new List<string>();
            foreach(string resultPath in aPath)
            {
                string str = new CFile(resultPath).ReadToEnd();

                string convName = resultPath.Replace(oldExt, newExt);
                new CFile(convName).WriteTotal(str);
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
