using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
    public static class Consts
    {
        public static bool bDebug = false;
    }

    public interface IName
    {
        string Name();
    }
    public interface IToString
    {
        string ToString();
    }
    public interface ISetLogger
    {
        void SetLogger(ILogger log = null);
    }
    public interface ISerialize
    {
        void Serialize(string path);
        bool Deserialize(string path);
    }
}
