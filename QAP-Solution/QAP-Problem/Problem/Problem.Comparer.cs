using System;

namespace Problem
{
	public partial class AProblem
	{
		/// <summary>Comparer</summary>
		/// <returns>0 - equal, 1 - x>y, -1 - x<y </returns>
		protected int CPermutationComparision(IPermutation x, IPermutation y) // decrease -> y-x // increase -> x-y
		{
			string log = $"CPermutationComparision input: ";
			if(x == null)
			{
				if(y == null)
				{
					msg(log + $"NULL, NULL; output: 0");
					return 0;
				}
				else
				{
					msg(log + $"NULL, {y.ToString()}; output: -1");
					return -1;
				}
			}
			else
			{
				if(y == null)
				{
					msg(log + $"{x.ToString()}, NULL; output: 1");
					return 1;
				}
				else
				{
					log += $"{x.ToString()}, {y.ToString()}; output: ";
					if(x.GetType() != y.GetType() || x.size() != y.size() || x.size() != m_ProblemSize)
					{
						msg(log + "ERROR");
						return -2;
					}
					long a = calc(x), b = calc(y);
					if(a == b)
					{
						msg(log + "EQUAL(0)");
						return 0;
					}
					else if(a > b)
					{
						msg(log + $"x({a}) > y({b}) (1)");
						return 1;
					}
					else
					{
						msg(log + $"x({a}) < y({b}) (-1)");
						return -1;
					}

				}
			}
		}
	}
}