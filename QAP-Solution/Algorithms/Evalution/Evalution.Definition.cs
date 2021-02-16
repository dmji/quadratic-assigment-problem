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
            public int DEFINE_RANDOM_SEED { get; set; }

            string m_name;

            public string getName() => m_name;
            public int getSeed() => DEFINE_RANDOM_SEED;
            public void serielize(string path)
            {
                if(!System.IO.File.Exists(path))
                    System.IO.File.Create(path).Close();
                System.IO.StreamWriter export = new System.IO.StreamWriter(path);
                export.WriteLine(System.Text.Json.JsonSerializer.Serialize<Options>(this));
                export.Close();
            }
            
            public Options()
            {
                DEFINE_POPULATION_SIZE = 0;
                DEFINE_COSSOVERING_SIZE = 0;
                DEFINE_CROSSOVER_CHANCE = 0;
                DEFINE_MUTATION_CHANCE = 0;
                DEFINE_MUTATING_SIZE = 0;
                DEFINE_STEP_MAXIMUM = 0;
                DEFINE_DELETE_DUPLICAET = false;
                DEFINE_RANDOM_SEED = -1;
            }

            public Options(string path)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                string file = reader.ReadToEnd();
                init(System.Text.Json.JsonSerializer.Deserialize<Options>(file));
                m_name = path.Substring(path.LastIndexOf('\\')+1, path.LastIndexOf('.')-path.LastIndexOf('\\')-1); 
                reader.Close();
            }

            public void init(
                int POPULATION_SIZE,
                int COSSOVERING_SIZE,
                double CROSSOVER_CHANCE,
                double MUTATION_CHANCE,
                int MUTATING_SIZE,
                int STEP_MAXIMUM,
                bool DELETE_DUPLICAET,
                int SEED
                )
            {
                DEFINE_POPULATION_SIZE = POPULATION_SIZE;
                DEFINE_COSSOVERING_SIZE = COSSOVERING_SIZE;
                DEFINE_CROSSOVER_CHANCE = CROSSOVER_CHANCE;
                DEFINE_MUTATION_CHANCE = MUTATION_CHANCE;
                DEFINE_MUTATING_SIZE = MUTATING_SIZE;
                DEFINE_STEP_MAXIMUM = STEP_MAXIMUM;
                DEFINE_DELETE_DUPLICAET = DELETE_DUPLICAET;
                DEFINE_RANDOM_SEED = SEED;
            }

            public void init(Options obj)
            {
                init(obj.DEFINE_POPULATION_SIZE,
                obj.DEFINE_COSSOVERING_SIZE,
                obj.DEFINE_CROSSOVER_CHANCE,
                obj.DEFINE_MUTATION_CHANCE,
                obj.DEFINE_MUTATING_SIZE,
                obj.DEFINE_STEP_MAXIMUM,
                obj.DEFINE_DELETE_DUPLICAET,
                obj.DEFINE_RANDOM_SEED);
            }

            public string getValues() => $"{m_name};{DEFINE_POPULATION_SIZE};{DEFINE_COSSOVERING_SIZE};{DEFINE_CROSSOVER_CHANCE};{DEFINE_MUTATION_CHANCE};{DEFINE_MUTATING_SIZE};{DEFINE_STEP_MAXIMUM};{DEFINE_DELETE_DUPLICAET};{DEFINE_RANDOM_SEED}";
            public string getValuesNames() => "OPTIONSET_NAME;DEFINE_POPULATION_SIZE;DEFINE_COSSOVERING_SIZE;DEFINE_CROSSOVER_CHANCE;DEFINE_MUTATION_CHANCE;DEFINE_MUTATING_SIZE;DEFINE_STEP_MAXIMUM;DEFINE_DELETE_DUPLICAET;DEFINE_RANDOM_SEED";

        }
    }
}
