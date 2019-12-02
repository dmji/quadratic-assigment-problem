#pragma once
#include "EGA.h"
/*
void population_crossover(int &SELECTION_DEFINE, std::vector<individ_Data> population, std::vector<individ_Data>  &reproducts)
{
	
	int rand_t = 0, hem_t;
	individ_Data fir_exemp = population.begin()->copy(), sec_exemp = population.begin()->copy();
	while (population.size() > 1)
	{
		//
		//PAIRS SELECT STRATEGY
		//
		hem_t = -1;
		rand_t = rand() % population.size();
		fir_exemp = population[rand_t];
		population.erase(population.begin() + rand_t);
		rand_t = -1;
		for (unsigned int i = 0; i < population.size(); i++)
			if (Hemming(fir_exemp, population[i]) > hem_t)
			{
				hem_t = Hemming(fir_exemp, population[i]);
				rand_t = i;
			}
		sec_exemp = population[rand_t];
		population.erase(population.begin() + rand_t);
		//
		//ABSTRACT CROSSOVER
		//
		switch (SELECTION_DEFINE)
		{
		case 1:
			crossover_type1(reproducts, fir_exemp, sec_exemp);
		break;
		case 2:
			crossover_type2(reproducts, fir_exemp, sec_exemp);
			break;
		}
	}
	population.clear();
	
	//TODO
}
*/