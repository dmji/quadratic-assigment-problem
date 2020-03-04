#pragma once
#include "Header.h"

struct individ_Data
{
	std::vector<int> point = std::vector<int>();

	individ_Data();
	individ_Data(int count);

	void fill_random();									//fill_random construct permutation
	void print();										//console out permutation

	void operator=(individ_Data &src);
};