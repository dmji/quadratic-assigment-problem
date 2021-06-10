﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
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
        void Deserialize(string path);
    }
}