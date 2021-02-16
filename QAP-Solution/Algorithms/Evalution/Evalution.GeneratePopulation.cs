using System;
using System.Collections.Generic;
using System.Text;
using Problem;

namespace Algorithms
{
    public partial class EvalutionAlgorithm
    {
        bool Hemming(List<Individ> aPerm, Individ t)
        {
            int min_distance = t.size();
            foreach(Individ a in aPerm)
            {
                int distance = 0;
                for(int i = 0; i < a.size(); i++)
                {
                    if(a[i] != t[i])
                        distance++;
                }
                if(distance < min_distance)
                    min_distance = distance;
            }
            return min_distance > DEFINE_START_POPULATION_MIN_HDIST;
        }

        /// <summary>Генерация популции со случайным заполнением</summary>
        /// <param name="count">размер популяции</param>
        /// <returns>популяция</returns>
        protected List<Individ> GEENERETE_POPULATION(int count)
        {
            List<Individ> res = new List<Individ>();
            while(res.Count < count)
            {
                Individ temp = new Individ(m_q.calc, size());
                if(Hemming(res, temp))
                {
                    msg($"GEENERETE_POPULATION Step {res.Count+1}: created {temp};");
                    res.Add(temp);
                }
            }
            return res;
        }
    }
}
