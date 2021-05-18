namespace Solution
{
    public interface IOptions : IName
    {
        void Serielize(string path);
        int GetSeed();
        string GetValues();
        string GetValuesNames();
    }

    public partial class EvolutionAlgorithm : AAlgorithm
    {
        public EvolutionAlgorithm(IProblem problem) : base(problem) { }

        public override string Name() => "EvolutionBased";
        public static string getName(bool b) => "EvolutionBased";


        public new static IOptions GetOptionsSet(string path) => new Options(path);
    }
}
