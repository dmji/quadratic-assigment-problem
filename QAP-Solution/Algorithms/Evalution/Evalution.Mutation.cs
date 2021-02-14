using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public partial class EvalutionAlgorithm
    {
        protected List<Individ> MUTATION(List<Individ> src, int count = 0, int type = 0, double mChance = 1)
        {
            int mutationCounter = 0;
            List<Individ> aResult=new List<Individ>(src.ToArray());
            List<int> aMutatedIndividsId = new List<int>();
            if(src.Count < count)
                count = src.Count;
            while (mutationCounter < count)
            {
                int iRnd = rand.next(src.Count);
                if (aMutatedIndividsId.Contains(iRnd) == false)
                {
                    switch (type)
                    {
                        case 0:
                            aResult[iRnd]._mutationSaltation();
                            break;
                        case 1:
                            aResult[iRnd]._mutationDot();
                            break;
                    }
                    mutationCounter++;
                    aMutatedIndividsId.Add(iRnd);
                }
            }
            return aResult;
        }
    }
}
