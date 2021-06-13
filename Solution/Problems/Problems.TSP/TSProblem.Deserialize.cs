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
			string buf= file.ReadToEnd();

			while(buf.Contains("  "))
				buf = buf.Replace("  ", " ");
			string[] aData = buf.Split('\n', StringSplitOptions.RemoveEmptyEntries);

			ushort cDimension = 0;
			foreach(var s in aData)
			{
				if(s.Contains("DIMENSION"))
				{
					var str = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
					cDimension = UInt16.Parse(str[1].Trim());
					break;
				}
			}

			if(cDimension == 0)
            {
				throw new Exception("Deserialize Error dimension error");
            }
			Init(cDimension);

			if(buf.Contains("NODE_COORD_SECTION"))
			{
				buf = buf.Substring(buf.IndexOf("NODE_COORD_SECTION") + "NODE_COORD_SECTION".Length);
				DeserializeNodeCoord(buf);
			}
			else if(buf.Contains("EDGE_WEIGHT_FORMAT"))
			{
				string sType = "";
				foreach(var s in aData)
                {
					if(s.Contains("EDGE_WEIGHT_FORMAT"))
					{
						var str = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
						sType = str[1].Trim();
						break;
					}
				}
				buf = buf.Substring(buf.IndexOf("EDGE_WEIGHT_SECTION") + "EDGE_WEIGHT_SECTION".Length);
				buf = buf.Replace("\n", " ").Trim().TrimEnd().TrimStart();
				switch(sType)
                {
					case "FULL_MATRIX":
						DeserializeFullMatrix(buf);
						break;
					case "UPPER_ROW":
						DeserializePartMatrix(buf, false, false);
						break;
					case "LOWER_ROW":
						DeserializePartMatrix(buf, true, false);
						break;
					case "UPPER_DIAG_ROW":
						DeserializePartMatrix(buf, false, true);
						break;
					case "LOWER_DIAG_ROW":
						DeserializePartMatrix(buf, true, true);
						break;
					default:
						throw new Exception("Deserialize Error Unknown type");
                }

				
			}			
			Msg($"Finish importing problem from file: {fname}");
		}

		void DeserializeNodeCoord(string buf)
        {
			var aStr = buf.Split('\n', StringSplitOptions.RemoveEmptyEntries);
			System.Collections.Generic.List<double> x = new System.Collections.Generic.List<double>(), y = new System.Collections.Generic.List<double>();
			for(int i = 0; i < Size(); i++)
            {
				var aData = aStr[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
				if(aData.Length < 3)
                {
					i--;
					continue;
                }
				x.Add(double.Parse(aData[1].Trim().Replace('.', ',')));
				y.Add(double.Parse(aData[2].Trim().Replace('.', ',')));
            }

			for(int i = 0; i < Size(); i++)
			{
				for(int j = 0; j < Size(); j++)
				{
					double xp = System.Math.Abs(x[i] - x[j]); 
					double yp = System.Math.Abs(y[i] - y[j]); 
					SetDist((int)System.Math.Sqrt(xp * xp + yp * yp),i, j);
				}
			}
        }

		void DeserializePartMatrix(string buf, bool bLower, bool bDiag) 
		{
			var aDist = buf.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			int iDist = 0;
			for(int i = 0; i < Size(); i++)
			{
				for(int j = bLower ? 0 : i, n = bLower ? i : Size(); j < n; j++)
				{
					if(i == j)
					{
						SetDist(0, i, j);
						if(bDiag)
							iDist++;
					}
					else
					{
						SetDist(System.Int32.Parse(aDist[iDist]), i, j);
						SetDist(System.Int32.Parse(aDist[iDist]), j, i);
						iDist++;
					}					
				}
			}
		}

		void DeserializeFullMatrix(string buf)
        {
			var aDist = buf.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			int iDist = 0;
			var firstVal = System.Int32.Parse(aDist[iDist]);
			if(firstVal == Size())
				iDist++;
			for(int i = 0; i < Size(); i++)
			{
				for(int j = 0; j < Size(); j++)
					SetDist(System.Int32.Parse(aDist[iDist++]), i, j);
			}
        }
	}
}
