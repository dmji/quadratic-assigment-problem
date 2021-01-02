using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Info
    {
        /// <summary>
        /// Generation linear problem for bound method (WIP)
        /// </summary>
        /// <returns>matrix of double</returns>
        public double[,] ReflectionToLAP()
        {
            double calcReflection(int a, int b)
			{
                List<double> min = new List<double>(problem_size - 1), max = new List<double>(problem_size - 1);

				for (int i = 0; i < problem_size; i++)
				{
					if (i != a)
						min.Add(distance[i,a]);
					if (i != b)
						max.Add(flow[i,b]);
				}
                min.Sort();
                max.Sort();
                max.Reverse();

				double res = 0;
				for (int i = 0; i < problem_size - 1; i++)
					res = res + min[i] * max[i];
				return res;
			}

			double[,] res = new double[problem_size, problem_size];

            for (int i = 0; i < problem_size; i++)
                for (int j = 0; j < problem_size; j++)
                    res[i,j] = calcReflection(i, j);

            return null;
        }
    }
}