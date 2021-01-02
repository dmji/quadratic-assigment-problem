using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmsBase;
using QAPenviron;

namespace QAP_EVALRANDOM
{
    class QAP_EVALRANDOM : Algorithms
    {
        double[,] rMatr;
        int bestCost = 0;
        public List<int> curbest { get { return curbests[0]; } set { curbests[0] = value; } }
        public QAP_EVALRANDOM(Func<List<int>, int> calculate, int problem_size) : base(calculate, problem_size)
        {
            rMatr = new double[problem_size, problem_size];
            for (int i = 0; i < problem_size; i++)
                for (int j = 0; j < problem_size; j++)
                    rMatr[i, j] = 1.0 / problem_size;
        }

        public void Start(int maxStep,double epselon)
        {
            statReset();
            curbests.Add(randomPermutation(problem_size));
            bestCost = calculate(curbest);

            do {
                List<int> result = new List<int>();
                for (int i = 0; i < problem_size; i++)
                    result.Add(-1);
                for (int i = 0; i < problem_size; i++)
                {
                    int curInd = -1, iRand=0;
                    while (result[iRand] != -1) 
                        iRand = new Random().Next(problem_size);

                    double maxRand = 0, curSum = 0;
                    for (int j = 0; j < problem_size; j++)
                        maxRand += rMatr[iRand, j];
                    maxRand *= new Random().NextDouble();
                    for (int j = 0; j < problem_size; j++)
                        if (result.Contains(j) == false)
                        {
                            if (curInd==-1 || curSum + rMatr[iRand, j] <= maxRand)
                            {
                                curSum += rMatr[iRand, j];
                                curInd = j;
                            }
                            else
                                break;
                        }
                        else
                        {
                            curSum += rMatr[iRand, j];
                        }
                    result[iRand]=curInd;
                }
                int curCost = calculate(result);
                if (curCost < bestCost)
                {
                    bestCost = curCost;
                    curbest = result;
                    for (int i = 0; i < problem_size; i++)
                        for (int j = 0; j < problem_size; j++)
                        {
                            if (j == result[i])
                                rMatr[i, j] += rMatr[i, result[i]]*epselon;
                            else
                                rMatr[i, j] -= rMatr[i, result[i]] * (epselon / problem_size);
                        }
                }
                else
                {
                    for (int i = 0; i < problem_size; i++)
                        for (int j = 0; j < problem_size; j++)
                        {
                            if (j == result[i])
                                rMatr[i, j] -= rMatr[i, result[i]] * epselon;
                            else
                                rMatr[i, j] += rMatr[i, result[i]] * (epselon / problem_size);
                        }
                }
                Console.WriteLine("rStep: "+maxStep + "\t:: " +getPermutation(result) + "::"+ bestCost);
                if(maxStep%1 == 0)
                {
                    for (int i = 0; i < problem_size; i++)
                    {
                        for (int j = 0; j < problem_size; j++)
                            Console.Write($"{Math.Round(rMatr[i, j],5)} ");
                        Console.Write('\n');
                    }
                    Console.Write("\n");
                }
            } while (maxStep-- != 0);
        } 
    } 
}
