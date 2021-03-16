using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP data.</summary>
	public partial class CQAPProblem
	{
		/// <summary>
		/// Export current problem to txt
		/// <para>if current problem is test generated to save param in file path-name</para>
		/// </summary>
		public void export_txt(CPermutation p = null, int omeg = -1, int z = -1)
		{
			msg("ExportTxt begin");
			string buf = m_ProblemSize.ToString() + "\n";
			for(int i = 0; i < m_ProblemSize; i++)
				for(int j = 0; j < m_ProblemSize; j++)
				{
					if(j == m_ProblemSize - 1)
						buf = buf + m_tDistance[i, j] + "\n";
					else
						buf = buf + m_tDistance[i, j] + " ";
				}
			buf = buf + '\n';
			for(int i = 0; i < m_ProblemSize; i++)
				for(int j = 0; j < m_ProblemSize; j++)
				{
					if(j == m_ProblemSize - 1)
						buf = buf + m_tFlow[i, j] + "\n";
					else
						buf = buf + m_tFlow[i, j] + " ";
				}
			buf = buf + '\n';
			for(int i = 0; i < m_ProblemSize; i++)
				for(int j = 0; j < m_ProblemSize; j++)
				{
					if(j == m_ProblemSize - 1)
						buf = buf + m_tPositionCost[i, j] + "\n";
					else
						buf = buf + m_tPositionCost[i, j] + " ";
				}
			if(p != null)
			{
				buf = buf + "\np={";
				for(int i = 0; i < m_ProblemSize; i++)
					buf = buf + p[i] + ", ";
				buf = buf + "}";
			}
			StreamWriter file;
			string pathFile = omeg == -1 ? $"ex_{m_ProblemSize}_{DateTime.Now}.bin" : pathFile = $"ex_{m_ProblemSize} {omeg} {z}_{DateTime.Now}.bin";
			if(!System.IO.File.Exists(pathFile))
				System.IO.File.Create(pathFile).Close();
			file = new StreamWriter(pathFile);
			file.WriteLine(buf);
			file.Close();
			msg("ExportTxt end");
		}
	}
}