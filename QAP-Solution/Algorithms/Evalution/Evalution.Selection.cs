using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public partial class EvalutionAlgorithm
    {
        protected List<Individ> SELECTION(List<Individ> src,int populationSize, int BtournamentSize=2)
        {
            Random rand = new Random();
            List<Individ> aResult = new List<Individ>();

            for (int i = 0; i < populationSize; i++)
            {
                int bestIndex = -1;
                for (int iTour = 0; iTour < BtournamentSize; iTour++)
                {
                    int curIndex = rand.Next(src.Count);
                    if (bestIndex == -1 || src[bestIndex].cost() > src[curIndex].cost())
                        bestIndex = curIndex;
                }
                aResult.Add(src[bestIndex]);
            }
            return aResult;
        }
    }
}