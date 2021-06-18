//using System;
//using System.Collections.Generic;
//using System.Xml;
//using Solution;

//namespace TestSystem
//{
//    public class CTestFullfoce : ATest
//    {
//        public CTestFullfoce(string path, bool bLog) : base(path, 1, bLog)
//        {
//            m_path = new CFile(path);
//        }

//        public override void Start()
//        {
//            List<CTestInfo> aTest = new List<CTestInfo>();

//            List<string> aTours;

//            string dirPath = m_path.Substring(0, m_path.LastIndexOf('\\') + 1);
//            {
//                // read xml starter
//                while(m_path.Contains('\"'))
//                {
//                    var i = m_path.IndexOf('\"') + 1;
//                    m_path = m_path.Substring(i, m_path.LastIndexOf('\"') - i);
//                }
//                XmlReader xml = XmlReader.Create(m_path);
//                xml.Read();

//                // seporate absolute path
//                m_path = m_path.Substring(0, m_path.LastIndexOf('\\') + 1);

//                List<string> aProblemFile = GetArrtibuteDirFiles(xml, "problems", m_path);
//                FileExtConverter(aProblemFile, ".bin", ".qap.exam");

//                aTours = GetArrtibuteDirFiles(xml, "problems", m_path, "tour");
//                InitProblem(aProblemFile[0]);
//                foreach(var file in aProblemFile)
//                {
//                    string localTour = file + ".tour";
//                    if(aTours.Contains(localTour))
//                        aTest.Add(new CTestInfo(file, file+".exam"));
//                }
//            }
//            ITabler table = null;
//            InitLogger(table);         

//            CTimer timer = new CTimer();
//            foreach(CTestInfo test in aTest)
//            { 
//                m_problem.Deserialize(test.pathProblem);
//                List<ushort> perm = new List<ushort>();
//                var buf = new CFile(test.pathProblem + ".tour").ReadToEnd().Replace("\r\n", " ").Replace("\n", " ");
//                buf = buf.Substring(buf.IndexOf("TOUR_SECTION") + "TOUR_SECTION".Length);
//                var aStr = buf.Split(" ", StringSplitOptions.RemoveEmptyEntries);
//                for(int i = 0;i < m_problem.Size();i++)
//                    perm.Add((ushort)(ushort.Parse(aStr[i])-1));
//                //perm.Add((ushort)(ushort.Parse(aStr[0])-1));
//                CPermutation t = new CPermutation(m_problem, perm);
//                long exam = 0;
//                test.Exam(ref exam);
//                Console.WriteLine($"problem {test.Name()}: tour_cost {t.Cost()} {(t.Cost() == exam ? "==" : t.Cost() < exam ? "<" : ">")} exam_cost {exam}");
//                //IAlgorithm ALG = new CFullforceAlgorithm(m_problem);
//                //EnableLog(m_problem, ALG);
//                //timer.Reset();
//                //ALG.Start(null);
//                  if(m_log != null)
//                //m_log.Msg($"{m_problem.Size()} {ALG.GetResultValue()}\n{ALG.Result.ToString()}");
//            }
//        }
//    }
//}
