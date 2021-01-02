using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        protected int Hemming(List<int> a, List<int> b)
        {
            int result = 0;
            for (int i = 0; i < a.Count; i++)
                if (a[i] != b[i])
                    result++;
            return result;
        }

        /// <summary>Генерация популции со случайным заполнением</summary>
        /// <param name="count">размер популяции</param>
        /// <returns>популяция</returns>
        protected List<Individ> _generate_population(int count)
        {
            List<Individ> res = new List<Individ>();
            for (int i = 0; i < count; i++)
            {
                res.Add(new Individ(randomPermutation(problem_size)));
                int save = res.Count;
                for (int j=0; j < res.Count - 1; j++)
                    if (Hemming(res[res.Count - 1].info, res[j].info) == 0)
                    {
                        res.RemoveAt(res.Count - 1);
                        i--;
                        break;
                    }
                if (save == res.Count)
                    res[res.Count - 1].cost = calculate(res[res.Count - 1].info);
            }
            return res;
        }
    }
}
