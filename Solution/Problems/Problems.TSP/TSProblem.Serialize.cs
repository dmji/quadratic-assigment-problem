using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
	public partial class CTSProblem
	{
		/// <summary>
		/// Export current problem to txt
		/// <para>if current problem is test generated to save param in file path-name</para>
		/// </summary>
		public void Serialize()
		{
			if(m_log != null)
				Msg("ExportTxt begin");
			string buf = Size().ToString() + "\n";
			for(int i = 0; i < Size(); i++)
			{
				for(int j = 0; j < Size(); j++)
					buf = buf + GetDist(i, j) + (j == Size() - 1 ? "\n" : " ");
			}
			TestSystem.CFile file = new TestSystem.CFile($"ex_{Size()}_{DateTime.Now}.atsp");
			file.WriteTotal(buf);
			if(m_log != null)
				Msg("ExportTxt end");
		}
	}
}