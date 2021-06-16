namespace Solution
{
	public partial class CQAProblem
	{
		/// <summary>Comparer</summary>
		/// <returns>0 - equal, 1 - x>y, -1 - x<y </returns>
		public override int PermutationComparision(IPermutation x, IPermutation y) // decrease -> y-x // increase -> x-y
			//=> base.PermutationComparision(y, x);
			=> base.PermutationComparision(x, y);
	}
}