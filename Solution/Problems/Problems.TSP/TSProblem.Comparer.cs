namespace Solution
{
	public partial class CTSProblem
	{
		/// <summary>Comparer</summary>
		/// <returns>0 - equal, 1 - x>y, -1 - x<y </returns>
		public override int PermutationComparision(IPermutation x, IPermutation y) // decrease -> y-x // increase -> x-y
			=> (-1)*base.PermutationComparision(x, y);
	}
}