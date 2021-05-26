using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem
{
    public struct SFile
    {
        public static void CheckDir(string dir)
        {
            string pathDir = dir.Substring(0, dir.LastIndexOf('\\'));
            if(!System.IO.File.Exists(pathDir))
                System.IO.Directory.CreateDirectory(pathDir);
        }
    }
}
