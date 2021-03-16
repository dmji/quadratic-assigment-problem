using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
    public partial class EvolutionAlgorithm
    {
        protected List<Individ> SELECTION(List<Individ> src,int populationSize, int BtournamentSize=2)
        {
            Random rand = new Random();
            List<Individ> aResult = new List<Individ>();
            if(src.Count == 0)
                msg("T");
            for (int i = 0; i < populationSize; i++)
            {
                int bestIndex = -1;
                for (int iTour = 0; iTour < BtournamentSize; iTour++)
                {
                    int curIndex = rand.Next(src.Count);
                    if (bestIndex == -1 || calc(src[bestIndex]) > calc(src[curIndex]))
                        bestIndex = curIndex;
                }
                aResult.Add(src[bestIndex]);
            }
            return aResult;
        }
    }
}