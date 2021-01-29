using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        protected List<Individ> _selection(List<Individ> src,int populationSize, int BtournamentSize=2)
        {
            Random rand = new Random();
            List<Individ> result = new List<Individ>();

            for (int i = 0; i < populationSize; i++)
            {
                int curIndex;
                int bestIndex = -1;
                for (int j = 0; j < BtournamentSize; j++)
                {
                    curIndex = rand.Next(src.Count);
                    if (src[curIndex].cost == 0)
                        src[curIndex].cost = calculate(src[curIndex].info);

                    if (bestIndex == -1 || src[bestIndex].cost > src[curIndex].cost)
                        bestIndex = curIndex;
                }
                result.Add(new Individ(src[bestIndex]));
            }
            return result;
        }
    }
}