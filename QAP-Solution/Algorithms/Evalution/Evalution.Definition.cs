using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Problem;

namespace Algorithms
{
    public partial class EvalutionAlgorithm : Algorithm
    {
        public EvalutionAlgorithm(IProblem problem) : base(problem) { }

        public override string getName() => "EvalutionBased";
        public static string getName(bool b) => "EvalutionBased";


        public new static IOptions GetOptionsSet(string path) => new Options(path);

        public class Options : IOptions
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
            public int U_SEEDi { get; set; }

            string m_name;

            public string getName() => m_name;
            public int getSeed() => U_SEEDi;
            public void serielize(string path)
            {
                if(!System.IO.File.Exists(path))
                    System.IO.File.Create(path).Close();
                System.IO.StreamWriter export = new System.IO.StreamWriter(path);
                export.WriteLine(System.Text.Json.JsonSerializer.Serialize<Options>(this));
                export.Close();
            }
            
            public Options()
            {
                P_SIZEi = 0;
                C_SIZEi = 0;
                C_CHANCEi = 0;
                M_CHANCEi = 0;
                M_SIZEi = 0;
                E_LIMi = 0;
                S_DUPLICATEb = false;
                U_SEEDi = -1;
            }

            public Options(string path)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                string file = reader.ReadToEnd();
                init(System.Text.Json.JsonSerializer.Deserialize<Options>(file));
                m_name = path.Substring(path.LastIndexOf('\\')+1, path.LastIndexOf('.')-path.LastIndexOf('\\')-1); 
                reader.Close();
            }

            public void init(
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
                int E_LIMi,
                int U_SEEDi
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
                this.U_SEEDi = U_SEEDi;
            }

            public void init(Options obj)
            {
                init(obj.P_SIZEi,
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
                    obj.E_LIMi,
                    obj.U_SEEDi);
            }

            public string getValues() => $"{m_name};{P_SIZEi};{C_SIZEi};{C_CHANCEi};{M_CHANCEi};{M_SIZEi};{E_LIMi};{S_DUPLICATEb};{U_SEEDi}";
            public string getValuesNames() => "OPTIONSET_NAME;P_SIZE;C_SIZEi;C_CHANCEi;M_CHANCEi;M_SIZEi;E_LIMi;S_DUPLICATEb;U_SEEDi";

        }
    }
}
