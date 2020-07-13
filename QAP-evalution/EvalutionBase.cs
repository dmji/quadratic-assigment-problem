using System;
using System.Collections.Generic;
using System.Text;

namespace QAPenviron
{
    public partial class Evalution
    {
        protected Info problem;

        public Evalution(Info src)
        {
            problem = src;
        }

        public void Start(int POPULATION_SIZE)
        {
            List<Individ> population = _generate_population(POPULATION_SIZE);           // start population
        }
    }
}
