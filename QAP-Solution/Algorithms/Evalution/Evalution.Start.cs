using System;
using System.Collections.Generic;
using System.Linq;
using Problem;
namespace Algorithms
{
    public partial class EvalutionAlgorithm
    {
        double GenerationAvgCost(List<Individ> aPopulation)
        {
            long curGenCost = 0;
            foreach(Individ a in aPopulation)
                curGenCost += calc(a);
            //вычисление среднего значения поколения
            return curGenCost / (double)aPopulation.Count;
        }

        public override void Start(IOptions obj)
        {
            Options opt = (Options)obj;
            diagReset();
            rand.init(opt.DEFINE_RANDOM_SEED);

            START_TIMER();

            Individ bestIndivid = null;
            int POPULATION_ITERATION = 0, CONTROL_ITERATION=0;
            
            //генерация начальной популяции
            //GEENERETE_POPULATION - создает DEFINE_POPULATION_SIZE перестановок с Хемминговым расстоянием не равным 0
            List<Individ> curGen = GEENERETE_POPULATION(opt.DEFINE_POPULATION_SIZE);
            double prevGenAvgCost = GenerationAvgCost(curGen);
            while (CONTROL_ITERATION <= opt.DEFINE_STEP_MAXIMUM)
            {
                msg($"Start. Iteration {++POPULATION_ITERATION} begin");
                //создание нового поколения
                List<Individ> nextGen = REPRODUCTION(curGen, opt.DEFINE_COSSOVERING_SIZE);

                //мутация с вероятностью MUTATION_CHANCE всех индивидов нового поколения
                nextGen=MUTATION(nextGen, opt.DEFINE_MUTATING_SIZE, 0, opt.DEFINE_MUTATION_CHANCE);

                //nextGen.AddRange(curGen); // использование прошлого поколения в селекции

                //очистка от дубликатов
                if(opt.DEFINE_DELETE_DUPLICAET)
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
                curGen = SELECTION(nextGen, opt.DEFINE_POPULATION_SIZE, 2);

                var min = curGen.Min(x => calc(x));
                //поиск лучшего
                bestIndivid = curGen.Find(x=>calc(x) == min);

                //вычисление суммы
                double curGenAvgCost = GenerationAvgCost(curGen);
                double delta = curGenAvgCost - prevGenAvgCost;
                msg($"Start. Iteration {POPULATION_ITERATION}. AvgCost={curGenAvgCost}, delta={delta}, CurrentBest: {bestIndivid}");
                //проверка на увеличение среднего на 1%
                if(delta > prevGenAvgCost / 100)
                {
                    prevGenAvgCost = curGenAvgCost;
                    CONTROL_ITERATION = 0;
                }
                else
                    CONTROL_ITERATION++;
            }
            result = bestIndivid;
            STOP_TIMER();
            m_bFinish = true;
        }
    }
}
