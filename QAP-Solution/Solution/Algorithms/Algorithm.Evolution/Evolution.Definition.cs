namespace Solution
{
    public interface IOptions : IName
    {
        
        string GetValues();
        string GetValuesNames();
    }

    public partial class EvolutionAlgorithm : AAlgorithm
    {
        public EvolutionAlgorithm(IProblem problem) : base(problem) { }

        public override string Name() => "EvolutionBased";
        public static string Name(bool b) => "EvolutionBased";


        public static IOptions GetOptionsSet(string path) => new Options(path);
    }
}
