using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Evalution
    {
        protected List<Individ> _selection(List<Individ> src,int populationSize, int BtournamentSize=2)
        {
            Random rand = new Random();
            List<Individ> result = new List<Individ>();
            List<double> populationCost = new List<double>(src.Count);
            int bestOverAllIndex=-1;

            for (int i = 0; i < src.Count; i++)
                populationCost.Add(Convert.ToDouble(-1));

            for (int i = 0; i < populationSize; i++)
            {
                int curIndex;
                int bestIndex = -1;
                for (int j = 0; j < BtournamentSize; j++)
                {
                    curIndex = rand.Next(src.Count);
                    if (populationCost[curIndex] == -1)
                        populationCost[curIndex] = problem.cost(src[curIndex]);

                    if (bestIndex == -1 || populationCost[bestIndex] > populationCost[curIndex])
                        bestIndex = curIndex;
                }

                if (bestOverAllIndex == -1 || populationCost[bestOverAllIndex] > populationCost[bestIndex])
                    bestOverAllIndex = bestIndex;

                result.Add(new Individ(src[bestIndex]));
            }

            if (curbest==null || populationCost[bestOverAllIndex] < problem.cost(curbest))
                curbest = new Individ(src[bestOverAllIndex]);
            return result;
        }
    }
}
