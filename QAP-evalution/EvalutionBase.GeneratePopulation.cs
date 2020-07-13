using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Evalution
    {
        protected List<Individ> _generate_population(int count)
        {
            List<Individ> res = new List<Individ>();
            for(int i=0;i<count;i++)
                res.Add(new Individ(problem.problem_size));
            return res;
        }
    }
}
