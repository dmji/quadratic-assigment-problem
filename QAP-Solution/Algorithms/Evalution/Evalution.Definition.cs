using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Problem;

namespace Algorithms
{
    public partial class EvalutionAlgorithm : Algorithm
    {
        int DEFINE_START_POPULATION_MIN_HDIST = 0;

        public EvalutionAlgorithm(IProblem problem) : base(problem) { }

        public override string getName() => "EvalutionBased";
        public static string getName(bool b) => "EvalutionBased";


        public new static IOptions GetOptionsSet(string path) => new Options(path);

        public class Options : IOptions
        {
            public int DEFINE_POPULATION_SIZE { get; set; }
            public int DEFINE_COSSOVERING_SIZE { get; set; }
            public double DEFINE_CROSSOVER_CHANCE { get; set; }
            public double DEFINE_MUTATION_CHANCE { get; set; }
            public int DEFINE_MUTATING_SIZE { get; set; }
            public int DEFINE_STEP_MAXIMUM { get; set; }
            public bool DEFINE_DELETE_DUPLICAET { get; set; }

            public void serielize(string path)
            {
                //if(!System.IO.File.Exists(path))
                //    System.IO.File.Create(path);
                System.IO.StreamWriter export = new System.IO.StreamWriter(path);
                export.WriteLine(System.Text.Json.JsonSerializer.Serialize(this));
                export.Close();
            }
            
            public Options(string path)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                System.Text.Json.JsonSerializer.Deserialize<Options>(reader.ReadToEnd());
                reader.Close();
            }

            public Options(
                int POPULATION_SIZE,
                int COSSOVERING_SIZE,
                double CROSSOVER_CHANCE,
                double MUTATION_CHANCE,
                int MUTATING_SIZE,
                int STEP_MAXIMUM,
                bool DELETE_DUPLICAET
                )
            {
                DEFINE_POPULATION_SIZE = POPULATION_SIZE;
                DEFINE_COSSOVERING_SIZE = COSSOVERING_SIZE;
                DEFINE_CROSSOVER_CHANCE = CROSSOVER_CHANCE;
                DEFINE_MUTATION_CHANCE = MUTATION_CHANCE;
                DEFINE_MUTATING_SIZE = MUTATING_SIZE;
                DEFINE_STEP_MAXIMUM = STEP_MAXIMUM;
                DEFINE_DELETE_DUPLICAET = DELETE_DUPLICAET;
            }
        }
    }
}
