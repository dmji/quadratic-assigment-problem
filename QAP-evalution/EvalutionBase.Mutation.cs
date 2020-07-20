using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Evalution
    {
        protected void _mutation(Individ src)
        {
            _mutationCounter++;

            int temp1 = new Random().Next(problem.problem_size),
                temp2 = new Random().Next(problem.problem_size),
                save = src[temp1];
            while (temp2 == temp1) temp2 = new Random().Next(problem.problem_size);
            src[temp1] = src[temp2];
            src[temp2] = save;
        }
    }
}
