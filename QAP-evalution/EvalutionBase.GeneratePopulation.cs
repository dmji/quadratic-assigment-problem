using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        protected List<List<int>> _generate_population(int count)
        {
            List<List<int>> res = new List<List<int>>();
            for(int i=0;i<count;i++)
                res.Add(randomPermutation(problem_size));
            return res;
        }
    }
}
