using System;
using System.Collections.Generic;

namespace AlgorithmsBase
{
    public partial class Evalution : Algorithms
    {
        public int CONSOLE_DEBUG = 1;
        const int _breakpointDef = 3;
        int _mutationCounter = 0, _breakvalue, _breakpoint= _breakpointDef;
        public List<int> curbest { get { return curbests[0]; } set { curbests[0] = value; } }
        public Evalution(Func<List<int>, int> calculate, int problem_size):base(calculate, problem_size) 
        {
            curbests.Add(randomPermutation(problem_size));
        }

        public void Start(int POPULATION_SIZE, double MUTATION_CHANCE)
        {
            statReset();
            curbests.Add(randomPermutation(problem_size));
            _mutationCounter = 0;
            List<List<int>> population = _generate_population(POPULATION_SIZE);           // start population
            List<List<int>> generation = new List<List<int>>();

            int step = 0;

            while (_breakpoint > 0)
            {
                List<List<int>> tempgen = _reproduction(population, POPULATION_SIZE);

                foreach (List<int> a in tempgen)
                    if (new Random().NextDouble() < MUTATION_CHANCE)
                        _mutation(a);

                tempgen.AddRange(population);
                while (tempgen.Count > 0)
                {
                    generation.Add(tempgen[0]);
                    tempgen.RemoveAll(x => tempgen[0].Equals(x) == true);
                }

                population = _selection(generation, POPULATION_SIZE, 15);

                if(CONSOLE_DEBUG>0) Console.WriteLine($"Step {step++}. Current best: {getPermutation(curbest)}, Cost: {calculate(curbest)} Calculations: {calculation_counter}");
                foreach (List<int> a in population)
                    Console.WriteLine($" # {getPermutation(a)} :: {calculate(a)}");
            }

            _timer.Stop();
        }
    }
}
