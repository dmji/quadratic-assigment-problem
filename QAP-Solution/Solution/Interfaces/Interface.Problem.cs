namespace Solution
{
    /// <summary>Class <c>CPermutation</c> models a single permutation in QAP (like in Evolution algorithm).</summary>
    public interface IProblem
    {
        long calc(IPermutation src);
        ushort size();
    }
}
