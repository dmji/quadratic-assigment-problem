using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsBase
{
    public partial class Evalution
    {
        protected void _mutation(List<int> src)
        {
            _mutationCounter++;

            int temp1 = new Random().Next(problem_size),
                temp2 = new Random().Next(problem_size),
                save = src[temp1];
            while (temp2 == temp1) temp2 = new Random().Next(problem_size);
            src[temp1] = src[temp2];
            src[temp2] = save;
        }
    }
}
