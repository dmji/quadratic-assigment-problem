using System.Collections.Generic;

namespace Solution
{
    public partial class EvolutionAlgorithm
    {
        bool Hemming(List<Individ> aPerm, Individ t, int H_MINi)
        {
            int min_distance = t.Size();
            foreach(Individ a in aPerm)
            {
                int distance = 0;
                for(int i = 0; i < a.Size(); i++)
                {
                    if(a[i] != t[i])
                        distance++;
                }
                if(distance < min_distance)
                    min_distance = distance;
            }
            return min_distance > H_MINi;
        }

        /// <summary>Генерация популции со случайным заполнением</summary>
        /// <param name="count">размер популяции</param>
        /// <returns>популяция</returns>
        protected List<Individ> GEENERETE_POPULATION(int count, int H_MINi)
        {
            List<Individ> res = new List<Individ>();
            while(res.Count < count)
            {
                Individ temp = new Individ(m_problem, Size());
                if(Hemming(res, temp, H_MINi))
                {
                    Msg($"GEENERETE_POPULATION Step {res.Count+1}: created {temp};");
                    res.Add(temp);
                }
            }
            return res;
        }
    }
}
