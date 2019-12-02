#pragma once
#include "EGA.h"
/*
void generation_status(FILE *outfile, const char *mode, std::vector<individ_Data> &population, info_Data &info, const int &step)
{
	//TODO
	
	int control = -1;
	fprintf(outfile, "Step: %d %s result:", step, mode);
	for (unsigned int i = 0; i < population.size(); i++)
	{
		if (control != -1)
		{
			if (population[control].individ_calc(info.price) < population[i].individ_calc(info.price))
				control = i;
		}
		else control = i;
		fprintf(outfile, "\n%3d: Contain[ ", i);
		for (int ir = 0; ir < info.count; ir++)
			fprintf(outfile, "%d ", population[i][ir]);
		fprintf(outfile, "]:Price=%d, Weight=%d", population[i].individ_calc(info.price), population[i].individ_calc(info.weight));
	}
	fprintf(outfile, "\nBest sample: %d Contain[ ", control);
	for (int ir = 0; ir < info.count; ir++)
		fprintf(outfile, "%d ", population[control][ir]);
	fprintf(outfile, "]:Price=%d, Weight=%d\n", population[control].individ_calc(info.price), population[control].individ_calc(info.weight));
	
}

//void generation_status(const char *mode, std::vector<individ_Data> &population, info_Data &info, const int &step)
{
	//TODO
	
	int control = -1;
	printf("\Step: %d %s result:", step, mode);
	for (unsigned int i = 0; i < population.size(); i++)
	{
		if (control != -1)
		{
			if (population[control].individ_calc(info.price) < population[i].individ_calc(info.price))
				control = i;
		}
		else control = i;
		printf("\n%3d: Contain[ ", i);
		for (int ir = 0; ir < info.count; ir++)
			printf("%d ", population[i][ir]);
		printf("]:Price=%d, Weight=%d", population[i].individ_calc(info.price), population[i].individ_calc(info.weight));
	}
	printf("\nBest sample: %d Contain[ ", control);
	for (int ir = 0; ir < info.count; ir++)
		printf("%d ", population[control][ir]);
	printf("]:Price=%d, Weight=%d\n", population[control].individ_calc(info.price), population[control].individ_calc(info.weight));
	
}
*/