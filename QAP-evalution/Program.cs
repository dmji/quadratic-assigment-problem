using System;
using QAPenviron;

namespace AlgorithmsBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Info problem = new Info("..\\..\\..\\..\\QAP-CONTEST\\contest\\ex1.dat");
            Evalution prog = new Evalution(problem.calculate,problem.problem_size);
            prog.Start(
                DEFINE_POPULATION_SIZE: problem.problem_size*80,
             DEFINE_COSSOVERING_SIZE: problem.problem_size*800,
             DEFINE_CROSSOVER_CHANCE: 1,
             DEFINE_MUTATION_CHANCE: 0.1,
             DEFINE_MUTATING_SIZE: problem.problem_size*5,
             DEFINE_STEP_MAXIMUM: problem.problem_size*2
                );
            Console.WriteLine(prog.ToStr());
        }
    }
}
