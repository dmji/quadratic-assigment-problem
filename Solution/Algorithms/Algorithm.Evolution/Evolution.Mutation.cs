using System;
using System.Collections.Generic;
using System.Text;

namespace Solution
{
    public partial class CEvolutionAlgorithm
    {
        protected List<CIndivid> Mutation(List<CIndivid> src, int M_SIZEi = 0, int M_TYPEi = 0, double M_CHANCEi = 1, int M_SALT_SIZEi = 4)
        {
            Random rand = new Random();
            int mutationCounter = 0;
            List<CIndivid> aResult = new List<CIndivid>(src.ToArray());
            List<int> aMutatedIndividsId = new List<int>();
            int size = (src.Count < M_SIZEi) ? src.Count : M_SIZEi;
            while (mutationCounter < size)
            {
                int iRnd = rand.Next(src.Count);
                if (aMutatedIndividsId.Contains(iRnd) == false && M_CHANCEi >= rand.Next(101))
                {
                    switch (M_TYPEi)
                    {
                        case 0:
                            aResult[iRnd].MutationSaltation(M_SALT_SIZEi);
                            break;
                        case 1:
                            aResult[iRnd].MutationDot();
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
