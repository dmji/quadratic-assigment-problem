using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    public partial class CEvolutionAlgorithm
    {
        public class COptions : IOptions
        {
            public int P_SIZEi { get; set; }
            public int H_MINi { get; set; }
            public int C_SIZEi { get; set; }
            public int C_CHANCEi { get; set; }
            public int M_SIZEi { get; set; }
            public int M_CHANCEi { get; set; }
            public int M_TYPEi { get; set; }
            public int M_SALT_SIZEi { get; set; }
            public bool S_EXTENDb { get; set; }
            public bool S_DUPLICATEb { get; set; }
            public int S_TOURNi { get; set; }
            public int E_LIMi { get; set; }

            string m_name;

            public string Name() => m_name;
            
            public bool Deserialize(string path)
            {
                Init(System.Text.Json.JsonSerializer.Deserialize<COptions>(new TestSystem.CFile(path).ReadToEnd()));
                m_name = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);
                return true;
            }

            public void Serialize(string path)
            {
                if(!System.IO.File.Exists(path))
                    System.IO.File.Create(path).Close();
                System.IO.StreamWriter export = new System.IO.StreamWriter(path);
                export.WriteLine(System.Text.Json.JsonSerializer.Serialize<COptions>(this));
                export.Close();
            }

            public COptions()
            {
                P_SIZEi = 0;
                C_SIZEi = 0;
                C_CHANCEi = 0;
                M_CHANCEi = 0;
                M_SIZEi = 0;
                E_LIMi = 0;
                S_DUPLICATEb = false;
            }

            public COptions(string path)
            {
                Deserialize(path);
            }

            public void Init(
                int P_SIZEi,
                int H_MINi,
                int C_SIZEi,
                int C_CHANCEi,
                int M_SIZEi,
                int M_CHANCEi,
                int M_TYPEi,
                int M_SALT_SIZEi,
                bool S_EXTENDb,
                bool S_DUPLICATEb,
                int S_TOURNi,
                int E_LIMi
                )
            {
                this.P_SIZEi = P_SIZEi;
                this.H_MINi = H_MINi;
                this.C_SIZEi = C_SIZEi;
                this.C_CHANCEi = C_CHANCEi;
                this.M_SIZEi = M_SIZEi;
                this.M_CHANCEi = M_CHANCEi;
                this.M_TYPEi = M_TYPEi;
                this.M_SALT_SIZEi = M_SALT_SIZEi;
                this.S_EXTENDb = S_EXTENDb;
                this.S_DUPLICATEb = S_DUPLICATEb;
                this.S_TOURNi = S_TOURNi;
                this.E_LIMi = E_LIMi;
            }

            public void Init(COptions obj)
            {
                Init(obj.P_SIZEi,
                    obj.H_MINi,
                    obj.C_SIZEi,
                    obj.C_CHANCEi,
                    obj.M_SIZEi,
                    obj.M_CHANCEi,
                    obj.M_TYPEi,
                    obj.M_SALT_SIZEi,
                    obj.S_EXTENDb,
                    obj.S_DUPLICATEb,
                    obj.S_TOURNi,
                    obj.E_LIMi);
            }

            public string GetValues() => $"{m_name};{P_SIZEi};{H_MINi};{C_SIZEi};{C_CHANCEi};{M_SIZEi};{M_CHANCEi};{M_TYPEi};{M_SALT_SIZEi};{S_EXTENDb};{S_DUPLICATEb};{S_TOURNi};{E_LIMi}";
            public string GetValuesNames() => "OPTION NAME;P_SIZEi;H_MINi;C_SIZEi;C_CHANCEi;M_SIZEi;M_CHANCEi;M_TYPEi;M_SALT_SIZEi;S_EXTENDb;S_DUPLICATEb;S_TOURNi;E_LIMi";
        }
    }
}
