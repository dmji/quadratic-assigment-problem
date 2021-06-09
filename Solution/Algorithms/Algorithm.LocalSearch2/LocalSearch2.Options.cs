using System.Collections.Generic;

namespace Solution
{
    public partial class CLocalSearchAlgorithm : AAlgorithm
    {
        public class Options : IOptions
        {
            public bool B_FULLIFY { get; set; }
            public IPermutation m_p;

            string m_name;

            public string Name() => m_name;
            public void Serielize(string path)
            {
                if(!System.IO.File.Exists(path))
                    System.IO.File.Create(path).Close();
                System.IO.StreamWriter export = new System.IO.StreamWriter(path);
                export.WriteLine(System.Text.Json.JsonSerializer.Serialize<Options>(this));
                export.Close();
            }

            public Options()
            {
                B_FULLIFY = false;
            }

            public Options(string path)
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(path);
                string file = reader.ReadToEnd();
                Init(System.Text.Json.JsonSerializer.Deserialize<Options>(file));
                m_name = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);
                reader.Close();
            }

            public void Init(bool B_FULLIFY)
            {
                this.B_FULLIFY = B_FULLIFY;
            }

            public void Init(Options obj)
            {
                Init(obj.B_FULLIFY);
            }

            public string GetValues() => $"{m_name};{B_FULLIFY}";
            public string GetValuesNames() => "OPTION NAME;B_FULLIFY";
        }
    }
}