using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        protected List<List<int>> _selection(List<List<int>> src,int populationSize, int BtournamentSize=2)
        {
            Random rand = new Random();
            List<List<int>> result = new List<List<int>>();
            List<int> populationCost = new List<int>(src.Count);
            int bestOverAllIndex=-1,_breakCounter=0;

            for (int i = 0; i < src.Count; i++)
                populationCost.Add(Convert.ToInt32(-1));

            for (int i = 0; i < populationSize; i++)
            {
                int curIndex;
                int bestIndex = -1;
                for (int j = 0; j < BtournamentSize; j++)
                {
                    curIndex = rand.Next(src.Count);
                    if (populationCost[curIndex] == -1)
                        populationCost[curIndex] = calculate(src[curIndex]);

                    if (bestIndex == -1 || populationCost[bestIndex] > populationCost[curIndex])
                        bestIndex = curIndex;
                }

                if (bestOverAllIndex == -1 || populationCost[bestOverAllIndex] > populationCost[bestIndex])
                    bestOverAllIndex = bestIndex;
                //Проверяем захват популяции\
                if(populationCost[bestIndex] < _breakvalue)
                {
                    _breakCounter = 0;
                    _breakvalue = populationCost[bestIndex];
                }
                if (populationCost[bestIndex] == _breakvalue)
                    _breakCounter++;
                //Добавляем в популяцию
                result.Add(new List<int>(src[bestIndex]));
            }

            if (curbest==null || populationCost[bestOverAllIndex] < calculate(curbest))
                curbest = new List<int>(src[bestOverAllIndex]);
            //проверяем захват 4/5 популяции
            if (_breakCounter > (4/5) * populationSize)
                _breakpoint--;
            else
                _breakpoint = _breakpointDef;

            return result;
        }
    }
}
