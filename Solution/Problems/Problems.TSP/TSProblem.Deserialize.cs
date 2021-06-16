using System;
using System.IO;

namespace Solution
{
	/// <summary>Class <c>Info</c> is all-in one QAP aData.</summary>
	public partial class CTSProblem
	{
		///<summary>Construct problem from file with formatting:<para>size()</para><para>F-matrix</para><para>D-matrix</para><para>C-matrix</para></summary>
		/// <param name="fname">path to file w/ problem</param>
		public override bool Deserialize(string fname)
		{
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
			string sWeightType = "";
			foreach(var s in aData)
			{
				if(s.Contains("EDGE_WEIGHT_TYPE"))
				{
					var str = s.Split(':', StringSplitOptions.RemoveEmptyEntries);
					sWeightType = str[1].Trim();
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
				return DeserializeNodeCoord(buf, sWeightType);
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
						return DeserializeFullMatrix(buf);
						break;
					case "UPPER_ROW":
						return DeserializePartMatrix(buf, false, false);
						break;
					case "LOWER_ROW":
						return DeserializePartMatrix(buf, true, false);
						break;
					case "UPPER_DIAG_ROW":
						return DeserializePartMatrix(buf, false, true);
						break;
					case "LOWER_DIAG_ROW":
						return DeserializePartMatrix(buf, true, true);
						break;
					default:
						throw new Exception("Deserialize Error Unknown type");
                }
			}	
			else
				throw new Exception("Deserialize Error Unknown listing");
		}

		bool DeserializeNodeCoord(string buf, string type)
		{
			var aStr = buf.Split('\n', StringSplitOptions.RemoveEmptyEntries);
			switch(type)
			{
				case "EUC_2D":
					{
						System.Collections.Generic.List<double> x = new System.Collections.Generic.List<double>()
							, y = new System.Collections.Generic.List<double>();
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
								var xp = x[i] - x[j];
								var yp = y[i] - y[j];
								SetDist((int)(System.Math.Sqrt(xp * xp + yp * yp) + 0.5), i, j);
							}
						}
						break;
					}
				case "GEO":
                    {
						return false;
						System.Collections.Generic.List<double> x = new System.Collections.Generic.List<double>(), y = new System.Collections.Generic.List<double>();
						var PI = 3.141592;
						for(int i = 0; i < Size(); i++)
						{
							var aData = aStr[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
							if(aData.Length < 3)
							{
								i--;
								continue;
							}
							var curX = double.Parse(aData[1].Trim().Replace('.', ','));
							var degX = (int)curX;
							var minX = curX - degX;
							x.Add(PI * (degX + 5.0 * minX / 3.0) / 180.0);
							var curY = double.Parse(aData[2].Trim().Replace('.', ','));
							var degY = (int)curY;
							var minY = curY - (int)curY;
							y.Add(PI * (degY + 5.0 * minY / 3.0) / 180.0);
						}

						var RRR = 6378.388;
						for(int i = 0; i < Size(); i++)
						{
							for(int j = 0; j < Size(); j++)
							{
								var q1 = Math.Cos(y[i] - y[j]);
								var q2 = Math.Cos(x[i] - x[j]);
								var q3 = Math.Cos(x[i] + x[j]);
								SetDist((int)(RRR * Math.Acos(0.5 * ((1.0 + q1) * q2 - (1.0 - q1) * q3)) + 1.0), i, j);
							}
						}
						break;
					}
				case "ATT":
					{
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
								double xd = x[i] - x[j];
								double yd = y[i] - y[j];
								var rij = Math.Sqrt((xd * xd + yd * yd) / 10.0);
								var tij = (int)(rij + 0.5);
								SetDist((int)( tij < rij ? tij + 1 : tij), i, j);
							}
						}
						break;
					}
			}
			return true;
        }

		bool DeserializePartMatrix(string buf, bool bLower, bool bDiag) 
		{
			var aDist = buf.Split(" ", StringSplitOptions.RemoveEmptyEntries);
			int iDist = 0;
			int nDiag = 0;
			for(int i = 0; i < Size(); i++)
			{
				for(int j = bLower ? 0 : i, n = bLower ? i+1 : Size(); j < n; j++)
				{
					if(i == j)
					{
						nDiag++;
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
			return true;
		}

		bool DeserializeFullMatrix(string buf)
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
			return true;
        }
	}
}
