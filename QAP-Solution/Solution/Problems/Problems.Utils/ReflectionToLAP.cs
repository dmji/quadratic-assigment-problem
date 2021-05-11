using System.Collections.Generic;

namespace Solution
{
    public partial class Utils
    {
        /// <summary>
        /// Generation linear problem for bound method (WIP)
        /// </summary>
        /// <returns>matrix of double</returns>
        public double[,] ReflectionToLAP(CQAPProblem src)
        {
            double calcReflection(int a, int b)
            {
                List<double> min = new List<double>(src.size() - 1), 
                             max = new List<double>(src.size() - 1);

                for(int i = 0; i < src.size(); i++)
                {
                    if(i != a)
                        min.Add(src.getFlow(i, a));
                    if(i != b)
                        max.Add(src.getDist(i, b));
                }
                min.Sort();
                max.Sort();
                max.Reverse();

                double res = 0;
                for(int i = 0; i < src.size() - 1; i++)
                    res = res + min[i] * max[i];
                return res;
            }

            double[,] res = new double[src.size(), src.size()];

            for(int i = 0; i < src.size(); i++)
            {
                for(int j = 0; j < src.size(); j++)
                    res[i, j] = calcReflection(i, j);
            }
            return null;
        }
    }
}
