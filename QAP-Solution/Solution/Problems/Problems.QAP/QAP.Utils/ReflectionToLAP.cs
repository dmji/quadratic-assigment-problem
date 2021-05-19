﻿using System.Collections.Generic;

namespace Solution
{
    public partial struct QAPUtils
    {
        /// <summary>
        /// Generation linear problem for bound method (WIP)
        /// </summary>
        /// <returns>matrix of double</returns>
        public static double[,] ReflectionToLAP(CQAPProblem src)
        {
            double calcReflection(int a, int b)
            {
                List<double> min = new List<double>(src.Size() - 1), 
                             max = new List<double>(src.Size() - 1);

                for(int i = 0; i < src.Size(); i++)
                {
                    if(i != a)
                        min.Add(src.GetFlow(i, a));
                    if(i != b)
                        max.Add(src.GetDist(i, b));
                }
                min.Sort();
                max.Sort();
                max.Reverse();

                double res = 0;
                for(int i = 0; i < src.Size() - 1; i++)
                    res = res + min[i] * max[i];
                return res;
            }

            double[,] res = new double[src.Size(), src.Size()];

            for(int i = 0; i < src.Size(); i++)
            {
                for(int j = 0; j < src.Size(); j++)
                    res[i, j] = calcReflection(i, j);
            }
            return null;
        }
    }
}
