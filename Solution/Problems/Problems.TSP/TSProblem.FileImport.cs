using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP aData.</summary>
	public partial class CTSProblem
	{
		///<summary>Construct problem from file with formatting:<para>size()</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public CTSProblem(string fname)
		{
			Msg($"Start importing problem from file: {fname}");
			TestSystem.CFile file = new TestSystem.CFile(fname);
			string buf= file.ReadToEnd(), ext = file.GetExt();
			if(ext == "atsp")
				DeserializeATSP(buf);
			else
				return;				
			Msg($"Finish importing problem from file: {fname}");
		}

		void DeserializeATSP(string buf)
        {
			
			while(buf.Contains("  "))
				buf.Replace("  ", " ");
			string[] aData = buf.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        }
	}
}
