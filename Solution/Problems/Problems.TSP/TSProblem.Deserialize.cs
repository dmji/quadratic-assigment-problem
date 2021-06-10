using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP aData.</summary>
	public partial class CTSProblem
	{
		///<summary>Construct problem from file with formatting:<para>size()</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public override void Deserialize(string fname)
		{
			Msg($"Start importing problem from file: {fname}");
			TestSystem.CFile file = new TestSystem.CFile(fname);
			string buf= file.ReadToEnd(), ext = file.GetExt();
			if(ext == ".atsp")
				DeserializeATSP(buf);
			else
				return;				
			Msg($"Finish importing problem from file: {fname}");
		}

		void DeserializeATSP(string buf)
        {
			
			while(buf.Contains("  "))
				buf = buf.Replace("  ", " ");
			string[] aData = buf.Split('\n', StringSplitOptions.RemoveEmptyEntries);
			var aDataDimension = aData[3].Split(":");
			Init(System.UInt16.Parse(aDataDimension[1]));
			buf = buf.Substring(buf.IndexOf("EDGE_WEIGHT_SECTION") + "EDGE_WEIGHT_SECTION".Length);
			buf = buf.Replace("\n", " ");
			var aDist = buf.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			for(int i = 0; i < Size(); i++)
			{
				for(int j = 0; j < Size(); j++)
					SetDist(System.Int32.Parse(aDist[i * Size() + j]), i, j);
			}
        }
	}
}
