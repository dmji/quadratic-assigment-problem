namespace Solution
{
	public partial class AProblem
	{
		/// <summary>Comparer</summary>
		/// <returns>0 - equal, 1 - x>y, -1 - x<y </returns>
		public virtual int PermutationComparision(IPermutation x, IPermutation y) // decrease -> y-x // increase -> x-y
		{
			if(x == null)
			{
				if(y == null)
					return 0;
				else
					return -1;
			}
			else
			{
				if(y == null)
					return 1;
				else
				{
					if(x.GetType() != y.GetType() || x.Size() != y.Size() || x.Size() != m_problemSize)
						return -2;
					long a = x.Cost(), b = y.Cost();
					if(a == b)
						return 0;
					else if(a > b)
						return 1;
					else
						return -1;
				}
			}
		}
	}
}