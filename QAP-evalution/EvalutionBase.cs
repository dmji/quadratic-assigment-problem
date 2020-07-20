using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Evalution
    {
        protected Info problem;
        
        Individ curbest;

        int _mutationCounter = 0;
        public Evalution(Info src)
        {
            problem = src;
        }

        public void Start(int POPULATION_SIZE, double MUTATION_CHANCE)
        {
            _mutationCounter = 0;
            int _breakpoint = 50;
            List<Individ> population = _generate_population(POPULATION_SIZE);           // start population
            List<Individ> generation = new List<Individ>();

            int step = 0;

            while (true)
            {
                List<Individ> tempgen = _reproduction(population, POPULATION_SIZE);

                foreach (Individ a in tempgen)
                    if (new Random().NextDouble() < MUTATION_CHANCE)
                        _mutation(a);

                tempgen.AddRange(population);
                while (tempgen.Count > 0)
                {
                    generation.Add(tempgen[0]);
                    tempgen.RemoveAll(x => tempgen[0].Equals(x) == true);
                }

                population = _selection(generation, POPULATION_SIZE, 15);

                Console.WriteLine($"Step {step}. Current best: {curbest.ToString()}, Cost: {problem.cost(curbest)}");

                if (step++ > _breakpoint)
                    break;
            }
        }
    }
}
