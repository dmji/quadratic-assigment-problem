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
            public void Serialize(string path)
            {
                new TestSystem.CFile(path).WriteTotal(System.Text.Json.JsonSerializer.Serialize<Options>(this));
            }
            public bool Deserialize(string path)
            {
                Init(System.Text.Json.JsonSerializer.Deserialize<Options>(new TestSystem.CFile(path).ReadToEnd()));
                m_name = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);
                return true;
            }

            public Options()
            {
                B_FULLIFY = false;
            }

            public Options(string path)
            {
                Deserialize(path);
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