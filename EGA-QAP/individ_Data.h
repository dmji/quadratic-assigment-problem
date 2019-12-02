#pragma once
#include "Header.h"

struct individ_Data
{
	std::vector<int> point = std::vector<int>();
	int itemCount = 0;

	individ_Data();
	individ_Data(int count);
	individ_Data& copy();
	int& operator[](int index);
	bool operator==(individ_Data& src);
	int individ_calc(int* data);																				//TODO
	void individ_correcting(info_Data& info);
	void mutation_type1();
	void mutation_type2();
	void mutation_type3();
};

void crossover_type1(std::vector<individ_Data> &reproducts, individ_Data &src1, individ_Data &src2);
void crossover_type2(std::vector<individ_Data> &reproducts, individ_Data &src1, individ_Data &src2);
bool population_variety_control(std::vector<individ_Data> &population, individ_Data &src);
int Hemming(individ_Data &x, individ_Data &y);
int find_best(std::vector<individ_Data> &population, info_Data &info);