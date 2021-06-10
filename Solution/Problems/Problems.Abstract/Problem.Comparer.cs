namespace Solution
{
	public partial class AProblem
	{
		/// <summary>Comparer</summary>
		/// <returns>0 - equal, 1 - x>y, -1 - x<y </returns>
		public virtual int PermutationComparision(IPermutation x, IPermutation y) // decrease -> y-x // increase -> x-y
		{
			string log = $"PermutationComparision input: ";
			if(x == null)
			{
				if(y == null)
				{
					Msg(log + $"NULL, NULL; output: 0");
					return 0;
				}
				else
				{
					Msg(log + $"NULL, {y.ToString()}; output: -1");
					return -1;
				}
			}
			else
			{
				if(y == null)
				{
					Msg(log + $"{x.ToString()}, NULL; output: 1");
					return 1;
				}
				else
				{
					log += $"{x.ToString()}, {y.ToString()}; output: ";
					if(x.GetType() != y.GetType() || x.Size() != y.Size() || x.Size() != m_problemSize)
					{
						Msg(log + "ERROR");
						return -2;
					}
					long a = Calc(x), b = Calc(y);
					if(a == b)
					{
						Msg(log + "EQUAL(0)");
						return 0;
					}
					else if(a > b)
					{
						Msg(log + $"x({a}) > y({b}) (1)");
						return 1;
					}
					else
					{
						Msg(log + $"x({a}) < y({b}) (-1)");
						return -1;
					}

				}
			}
		}
	}
}