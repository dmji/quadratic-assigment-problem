using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace QAPenviron
{
	public partial class Individ
	{
		/// <summary>
		/// Comparer
		/// </summary>
		/// <returns>0 - equal, 1 - x>y, -1 - x<y </returns>
		public bool Equals([AllowNull] Individ other)
		{
			if (other == null)
				return false;
			for (int i = 0; i < p.Count; i++)
				if (p[i] != other[i])
					return false;
			return true;
			
		}

		/// <summary>
		/// premutation console one-line out
		/// </summary>
		public void console_print(int sign = -1)
		{
			for (int i = 0; i < p.Count; i++)
				Console.Write(p[i] + " ");
			if (sign == -1) Console.Write('\n');
		}

		/// <summary>
		/// print one-line permutation w/ spaces
		/// </summary>
		public override string ToString()
		{
			string result = "";
			for (int i = 0; i < p.Count; i++)
				result = result + p[i] + " ";
			return result;
		}
	}
}