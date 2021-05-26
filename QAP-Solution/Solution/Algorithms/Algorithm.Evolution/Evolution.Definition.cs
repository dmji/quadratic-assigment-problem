namespace Solution
{
    public interface IOptions : IName
    {
        string GetValues();
        string GetValuesNames();
    }

    public partial class CEvolutionAlgorithm : AAlgorithm
    {
        public CEvolutionAlgorithm(IProblem problem) : base(problem) { }

        public override string Name() => "EvolutionBased";
        public static string Name(bool b) => "EvolutionBased";


        public static IOptions GetOptionsSet(string path) => new COptions(path);
    }
}
