using System.Collections.Generic;

namespace Solution
{
    public partial class CEvolutionAlgorithm
    {
        bool Hemming(List<CIndivid> aPerm, CIndivid t, int H_MINi)
        {
            int min_distance = t.Size();
            foreach(CIndivid a in aPerm)
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
        protected List<CIndivid> GEENERETE_POPULATION(int count, int H_MINi)
        {
            List<CIndivid> res = new List<CIndivid>();
            while(res.Count < count)
            {
                CIndivid temp = new CIndivid(m_problem, Size());
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
