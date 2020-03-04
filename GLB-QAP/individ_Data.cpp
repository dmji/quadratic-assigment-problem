#include "individ_Data.h"

individ_Data::individ_Data()
{
}

individ_Data::individ_Data(int count)
{
	for (int i = 0; i < count; i++)
		point.push_back(-1);
}

void individ_Data::fill_random()
{
	std::vector<int> pool;
	for (int i = 0; i < point.size(); i++)
		pool.push_back(i);
	for (int i = 0; i < point.size(); i++)
	{
		int temp = rand() % pool.size();
		point[i]=pool[temp];
		pool.erase(pool.begin() + temp);
	}
}

void individ_Data::print()
{
	printf("\n");
	for (int i = 0; i < point.size(); i++)
		printf("%d ", point[i]);
}

void individ_Data::operator=(individ_Data& src)
{
	for (int i = 0; i < point.size(); i++)
		point[i] = src.point[i];
			
}