using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsBase
{
    public partial class Evalution : Algorithms
    {
        protected void printConsole(List<Individ> src, ref int step)
        {
            int stepAvg = 0, stepMin = 0;
            foreach (Individ a in src)
            {
                stepAvg += a.cost;
                if (a.cost < src[stepMin].cost)
                    stepMin = src.IndexOf(a);
            }
            //вывод популяции на каждом шаге
            Console.WriteLine($"Step {step++}. Calculations: {calculation_counter} AvgCost: {stepAvg / src.Count}");
            foreach (Individ a in src)
            {
                char sign = '#';
                if (src.IndexOf(a) == stepMin)
                    sign = '@';
                Console.Write($"{sign} {a.toStr()}\n");
            }
        }

        public void Start(
            int DEFINE_POPULATION_SIZE,
            int DEFINE_COSSOVERING_SIZE,
            double DEFINE_CROSSOVER_CHANCE,
            double DEFINE_MUTATION_CHANCE,
            int DEFINE_MUTATING_SIZE,
            int DEFINE_STEP_MAXIMUM
            )
        {
            statReset();
            curbests.Add(randomPermutation(problem_size));
            _mutationCounter = 0;

            Individ bestIndivid=null;
            int POPULATION_ITERATION = 0, CONTROL_ITERATION=0;
            decimal avg=0;
            //генерация начальной популяции
            //_generate_population - создает DEFINE_POPULATION_SIZE перестановок с Хемминговым расстоянием не равным 0
            List<Individ> population = _generate_population(DEFINE_POPULATION_SIZE);

            //выводим начальную популяцию
            if (CONSOLE_DEBUG > 0) printConsole(population, ref POPULATION_ITERATION);

            while (CONTROL_ITERATION <= DEFINE_STEP_MAXIMUM)
            {
                //создание нового поколения
                List<Individ> tempgen = _reproduction(population,DEFINE_COSSOVERING_SIZE);

                //мутация с вероятностью MUTATION_CHANCE всех индивидов нового поколения
                _mutation(population,DEFINE_MUTATING_SIZE);
                
                List<Individ> generation = new List<Individ>();
                
                //очистка от дубликатов
                //tempgen.AddRange(population);
                
                while (tempgen.Count > 0)
                {
                    generation.Add(tempgen[0]);
                    tempgen.RemoveAll(x => tempgen[0].Equals(x) == true);
                }

                population = _selection(generation, DEFINE_POPULATION_SIZE, 2);
                                
                if (CONSOLE_DEBUG > 0) printConsole(population, ref POPULATION_ITERATION);
                bestIndivid = population.Find(x=>x.cost == population.Min(x => x.cost));
                decimal cost = population.Sum(x => x.cost)/population.Count;
                if (avg - cost < -avg / 100)
                    CONTROL_ITERATION++;
                else
                    CONTROL_ITERATION = 0;
            }
            curbest = bestIndivid.info;
            //останавливаем таймер
            _timer.Stop();
        }
    }
}
