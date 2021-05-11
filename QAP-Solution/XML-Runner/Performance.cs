using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solution;

namespace XML_Runner
{
    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        public void _all()
        {
            TestSystem.StartTestProblemPerformance("C:\\Users\\leysh\\source\\repos\\dmji\\quadratic-assigment-problem\\QAP-Solution\\Contest\\_all.xml");
        }
    }
}
