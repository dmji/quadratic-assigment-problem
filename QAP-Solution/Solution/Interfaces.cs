using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
    public interface IName
    {
        string getName();
    }

    public interface IOptions : IName
    {
        void serielize(string path);
        int getSeed();
        string getValues();
        string getValuesNames();
    }
}
