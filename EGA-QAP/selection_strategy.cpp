#include "EGA.h"

using namespace std;
/*
void selection_linear_rang(info_Data & info, vector<individ_Data>  & population, vector<individ_Data>  & reproducts)
{
	bool bsort = true;
	vector<int> valu=vector<int>();
	vector<int> prob=vector<int>();
	individ_Data test = individ_Data(reproducts[0].itemCount);
	for (int i = 0; i < test.itemCount; i++)
		test[i] = 1;
	int sprob = 0;
	cout << "\n$$\n";
	generation_status("check prev sort", reproducts, info, 0);
	for (int i = 0; i < reproducts.size(); i++)
	{
		int	max_index=i;
		for (int j = i; j < reproducts.size(); j++)
		{
			if (reproducts[j].individ_calc(info.price) < reproducts[max_index].individ_calc(info.price))
			{
				max_index = j;
			}
		}
		test = reproducts[max_index].copy();
		reproducts.erase(reproducts.begin() + max_index);
		reproducts.insert(reproducts.begin() + i, test);
	}
	//for (std::vector<int>::iterator ptr = valu.begin(); ptr != valu.end(); ptcr++)
	//	cout << *ptr << endl;
	cout << "\n$$\n";
	generation_status("check sort", reproducts, info, 0);
	for (int i = 0; i < reproducts.size(); i++)
	{
		//cout << endl << ((reproducts[0].individ_calc(info.price) + reproducts[reproducts.size()-1].individ_calc(info.price) - reproducts[0].individ_calc(info.price))*(i)) / (reproducts.size()-1);
		cout << endl << ((1 + (reproducts.size() - 1)*i) / (reproducts.size()-1));
	}//		/ reproducts.size()
	cout << "$$\n";
}

void selection_roullet(info_Data & info, vector<individ_Data>  & population, vector<individ_Data>  & reproducts, int &POPULATION_SIZE)
{
	vector<int> prob = vector<int>();
	int prob_max = 0,
	//	avg_m=0,
	//	min_m=-1,
		temp,
		rand_t;
	for (int i = 0; i < reproducts.size(); i++)
	{
	/*
		if (min_m == -1)
			min_m = reproducts[i].individ_calc(info.price);
		else if (reproducts[i].individ_calc(info.price) < min_m)
			min_m = reproducts[i].individ_calc(info.price);
	
		prob_max += reproducts[i].individ_calc(info.price);
		prob.push_back(reproducts[i].individ_calc(info.price));
	}
	while (population.size() < POPULATION_SIZE - 1)
	{
		rand_t = rand() % prob_max;
		temp = 0;
		for (int i = 0; i < reproducts.size(); i++)
		{
			if (temp + prob[i] > rand_t)
			{
				population.push_back(reproducts[i].copy());
				break;
			}
			else
				temp += prob[i];
		}
	}
}

*/