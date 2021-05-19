using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    public partial class EvolutionAlgorithm
    {
        double GenerationAvgCost(List<Individ> aPopulation)
        {
            long curGenCost = 0;
            foreach(Individ a in aPopulation)
                curGenCost += Calc(a);
            //вычисление среднего значения поколения
            return curGenCost / (double)aPopulation.Count;
        }

        public override IResultAlg Start(IOptions obj)
        {
            Options opt = (Options)obj;
            ResetDiagnostic();

            Individ bestIndivid = null;
            int POPULATION_ITERATION = 0            // всего итераций
                , CONTROL_ITERATION=0;              // итераций для выхода
            
            //генерация начальной популяции
            //GEENERETE_POPULATION - создает P_SIZEi перестановок с Хемминговым расстоянием не равным 0
            List<Individ> curGen = GEENERETE_POPULATION(opt.P_SIZEi, opt.H_MINi);
            double prevGenAvgCost = GenerationAvgCost(curGen);
            while (CONTROL_ITERATION <= opt.E_LIMi)
            {
                Msg($"Start. Iteration {++POPULATION_ITERATION} begin");
                //создание нового поколения
                List<Individ> nextGen = REPRODUCTION(curGen, opt.C_SIZEi, opt.C_CHANCEi);

                //мутация с вероятностью MUTATION_CHANCE всех индивидов нового поколения
                nextGen=MUTATION(nextGen, opt.M_SIZEi, opt.M_TYPEi, opt.M_CHANCEi, opt.M_SALT_SIZEi);
                
                if(opt.S_EXTENDb)
                    nextGen.AddRange(curGen); // использование прошлого поколения в селекции

                //очистка от дубликатов
                if(!opt.S_DUPLICATEb)
                {
                    List<Individ> aDuplicateless = new List<Individ>();
                    while(nextGen.Count > 0)
                    {
                        aDuplicateless.Add(nextGen[0]);
                        nextGen.RemoveAll(x => nextGen[0].Equals(x) == true);
                    }
                    nextGen = aDuplicateless;
                }

                //селекция
                curGen = SELECTION(nextGen, opt.P_SIZEi, opt.S_TOURNi);

                var min = curGen.Min(x => Calc(x));
                //поиск лучшего
                if(bestIndivid == null)
                    bestIndivid = curGen.Find(x=>Calc(x) == min);
                else if(bestIndivid != null && bestIndivid.Cost() > min)
                    bestIndivid = curGen.Find(x => Calc(x) == min);

                //вычисление суммы
                double curGenAvgCost = GenerationAvgCost(curGen);
                double delta = curGenAvgCost - prevGenAvgCost;
                Msg($"Start. Iteration {POPULATION_ITERATION}. AvgCost={curGenAvgCost}, delta={delta}, CurrentBest: {bestIndivid}");
                //проверка на увеличение среднего на 1%
                if(delta > prevGenAvgCost / 100)
                {
                    prevGenAvgCost = curGenAvgCost;
                    CONTROL_ITERATION = 0;
                }
                else
                    CONTROL_ITERATION++;
            }
            Result = bestIndivid;
            m_bFinish = true;
            return this;
        }
    }
}
