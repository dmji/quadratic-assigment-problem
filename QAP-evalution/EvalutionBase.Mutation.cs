using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        /// <summary>Макромутация: Сальтация </summary>
        /// <param name="src"></param>
        protected void _mutation_saltation(ref List<int> src, int distance = 4)
        {
            _mutationCounter++;
            List<int> pool = new List<int>(src);
            int firstDot = new Random().Next(problem_size),
                secondDot,
                save;
            pool.Remove(firstDot);
            for (int i = 0; i < distance; i++)
            {
                save = src[firstDot];
                secondDot = pool[new Random().Next(pool.Count)];
                pool.Remove(secondDot);
                src[firstDot] = src[secondDot];
                src[secondDot] = save; 
            }
        }

        /// <summary>Точечная мутация</summary>
        /// <param name="src"></param>
        protected void _mutation_dot(ref List<int> src)
        {
            _mutationCounter++;
            int randInd = new Random().Next(problem_size-1),
                save = src[randInd];
            src[randInd] = src[randInd + 1];
            src[randInd+1] = save;
        }

        protected void _mutation(List<Individ> src, int count = 0, int type = 0)
        {
            int mutationCounter = 0;
            List<int> ed = new List<int>();
            while (mutationCounter < count)
            {
                int rind = new Random().Next(src.Count);
                if (ed.Contains(rind) == false)
                {
                    switch (type)
                    {
                        case 0:
                            _mutation_saltation(ref src[rind].info);
                            break;
                        case 1:
                            _mutation_dot(ref src[rind].info);
                            break;
                    }
                    src[rind].cost = 0;
                    mutationCounter++;
                    ed.Add(rind);
                }
            }
        }
    }
}
