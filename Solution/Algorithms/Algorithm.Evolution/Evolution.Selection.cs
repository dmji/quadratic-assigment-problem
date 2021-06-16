using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
    public partial class CEvolutionAlgorithm
    {
        protected List<CIndivid> SELECTION(List<CIndivid> src,int populationSize, int BtournamentSize=2)
        {
            Random rand = new Random();
            List<CIndivid> aResult = new List<CIndivid>();
            for (int i = 0; i < populationSize; i++)
            {
                int bestIndex = -1;
                for (int iTour = 0; iTour < BtournamentSize; iTour++)
                {
                    int curIndex = rand.Next(src.Count);
                    if(bestIndex == -1 || m_problem.PermutationComparision(src[curIndex], src[bestIndex]) == 1)
                        bestIndex = curIndex;
                }
                aResult.Add(src[bestIndex]);
            }
            return aResult;
        }
    }
}