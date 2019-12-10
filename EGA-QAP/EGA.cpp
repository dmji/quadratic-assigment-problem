#include "EGA.h"

using namespace std;

FILE *outfile;

int main(int argc, const char * argv[]) // 0-system_path 1-FILE_NAME 2-POPULATION_SIZE_DEFINE 3-FIRST_GENERATION_DEFINE 4-CROSSOVER_DEFINE 5-MUTATION_DEFINE 6-SELECTION_DEFINE 7-SHOW_CONSOLE_OUT 8-FILE_ENABLE 9-LOOP_COUNT
{
	srand((unsigned int)time(NULL));
	info_Data info = info_Data();
	string outfPath;
	int POPULATION_SIZE_DEFINE = -1, FIRST_GENERATION_DEFINE = -1, CROSSOVER_DEFINE = -1, MUTATION_DEFINE = -1,	SELECTION_DEFINE = -1, SHOW_CONSOLE_OUT = 1, FILE_ENABLE =1, LOOP_COUNT = 1;
	outfPath = outfPath + "tests" + '\\' + "outFile ";
	if (argc > 1)
	{
		outfPath = outfPath + argv[1];																															//FILE_NAME
		if (argc > 2)
		{
			outfPath = outfPath + " " + argv[2];
			if (strlen(argv[2]) > 2) POPULATION_SIZE_DEFINE = stoi(string(argv[2]).substr(2)); else POPULATION_SIZE_DEFINE = atoi(argv[2]);						//POPULATION_SIZE_DEFINE
			if (argc > 3)
			{
				outfPath = outfPath + " " + argv[3];
				if (strlen(argv[3]) > 2) FIRST_GENERATION_DEFINE = stoi(string(argv[3]).substr(2)); else FIRST_GENERATION_DEFINE = atoi(argv[3]);				//FIRST_GENERATION_DEFINE
				if (argc > 4)
				{
					outfPath = outfPath + " " + argv[4];
					if (strlen(argv[4]) > 2) CROSSOVER_DEFINE = stoi(string(argv[4]).substr(2)); else CROSSOVER_DEFINE = atoi(argv[4]);							//CROSSOVER_DEFINE
					if (argc > 5)
					{
						outfPath = outfPath + " " + argv[5];
						if (strlen(argv[5]) > 2) MUTATION_DEFINE = stoi(string(argv[5]).substr(2)); else MUTATION_DEFINE = atoi(argv[5]);						//MUTATION_DEFINE
						if (argc > 6)
						{
							outfPath = outfPath + " " + argv[4];
							if (strlen(argv[6]) > 2) SELECTION_DEFINE = stoi(string(argv[6]).substr(2)); else SELECTION_DEFINE = atoi(argv[6]);					//SELECTION_DEFINE
							if (argc > 7)
							{
								outfPath = outfPath + " " + argv[7];
								if (strlen(argv[7]) > 2) SHOW_CONSOLE_OUT = stoi(string(argv[7]).substr(2)); else SHOW_CONSOLE_OUT = atoi(argv[7]);				//SHOW_CONSOLE_OUT
								if (argc > 8)
								{
									outfPath = outfPath + " " + argv[8];
									if (strlen(argv[8]) > 2) FILE_ENABLE = stoi(string(argv[8]).substr(2)); else FILE_ENABLE = atoi(argv[8]);					//FILE_ENABLE
									if (argc > 9)
									{
										outfPath = outfPath + " " + argv[9];
										if (strlen(argv[9]) > 2) LOOP_COUNT = stoi(string(argv[9]).substr(2)); else LOOP_COUNT = atoi(argv[9]);					//LOOP_COUNT
									}
								}
							}
						}
					}
				}
			}
		}

	}
	if (FILE_ENABLE)																									//
	{																													//TODO check folder existing
		fopen_s(&outfile, (outfPath + ".txt").c_str(), "w+");															//open out file stream
	}																													//
	vector<individ_Data> population = vector<individ_Data>();
	vector<individ_Data> reproducts = vector<individ_Data>();															//
	if (argc > 1)
	{
		info.fileInput(argv[1]);															//input from file
	}
	else
	{
		printf("\nEnter file name ");
		cin >> outfPath;
		info.fileInput(outfPath.c_str());
	}
	if (POPULATION_SIZE_DEFINE == -1)																					//
	{
		cout << "Enter start population size: ";
		scanf_s("%d", &POPULATION_SIZE_DEFINE);
	}
	int step = 0, now_best=0, prev_best=0, static_best=0;
	
	for (int LOOP_COUNTER = 0; LOOP_COUNTER < LOOP_COUNT; LOOP_COUNTER++)
	{
		step = 0; now_best = 0; prev_best = 0, static_best = 0;
		if (POPULATION_SIZE_DEFINE == -1)
		{
			cout << "Enter start population operator:\n 1-NULL \n 2-NULL \n";
			scanf_s("%d", &POPULATION_SIZE_DEFINE);
		}
		for (int i = 0; i < POPULATION_SIZE_DEFINE; i++)																//
			switch (FIRST_GENERATION_DEFINE)																			//start population generating
			{																											//
			case 1:
				population.push_back(info.individ_generate_type1());
				break;
			case 2:
				population.push_back(info.individ_generate_type2());
				break;
			}
		/*
			while (true)																									//GENERAL EVOLUTION CYCLE
			{
				if (SHOW_CONSOLE_OUT)
				{
					if (FILE_ENABLE == 0)
						generation_status(outfile, "start population", population, info, step);
					else
						generation_status("start population", population, info, step);
				}
				if (CROSSOVER_DEFINE == -1)
				{
					cout << "Enter crossover operator:\n 1-one-point \n 2-rand each allel\n";
					scanf_s("%d", &CROSSOVER_DEFINE);
				}
				reproducts.clear();																							//
				population_crossover(CROSSOVER_DEFINE, population, reproducts);												//CROSSOVER
				if (SHOW_CONSOLE_OUT)																						//
				{
					if (FILE_ENABLE == 0)
						generation_status(outfile, "reproductions", reproducts, info, step);
					else
						generation_status("reproductions", reproducts, info, step);
				}
				if (MUTATION_DEFINE == -1)
				{
					printf("Enter mutation operator:\n 1- macromutation-saltation \n 2- chromosome-dopolnenie \n");
					scanf_s("%d", &MUTATION_DEFINE);
				}
				for (std::vector<individ_Data>::iterator ptr = reproducts.begin(); ptr != reproducts.end(); ptr++)			//
					if ((rand() % 100) < 17)																				//MUTATION
						switch (MUTATION_DEFINE)																			//
						{
						case 1:
							ptr->mutation_type1();
							break;
						case 2:
							ptr->mutation_type2();
							break;
						}
				if (SHOW_CONSOLE_OUT)
				{
					if (FILE_ENABLE == 0)
						generation_status(outfile,"mutations", reproducts, info, step);
					else
						generation_status("mutations", reproducts, info, step);
				}
				if (SELECTION_DEFINE == -1)
				{
					printf("Enter selection operator:\n 1- B-tournament \n 2- roullete no reverting  \n");
					scanf_s("%d", &SELECTION_DEFINE);
				}

				for (std::vector<individ_Data>::iterator ptr = reproducts.begin(); ptr != reproducts.end(); ptr++)		//
				{																										//REPRODUCTS CORRECTING
					//																									//
					//TODO
					//
				}
				individ_Data oldMax = individ_Data(info.count);															//
				oldMax = population[find_best(population, info)].copy();												//SAVE BEST
				population.erase(population.begin() + find_best(population, info));

				for (vector<individ_Data>::iterator ptr = population.begin(); ptr != population.end(); ptr++)
				{																										//
					if (population_variety_control(reproducts, ptr->copy()));											//P -> R copy
						reproducts.push_back(ptr->copy());																//
				}

				for (int i = 0; i < reproducts.size(); i++)																//
				{																										//delete copies
					for (int j = i; j < reproducts.size(); j++)															//
					{
						if (reproducts[i] == reproducts[j] && i != j)
						{
							reproducts.erase(reproducts.begin() + j);
							j--;
						}
					}
				}

				population.clear();
				switch (SELECTION_DEFINE)																				//SELETION
				{
				case 1:																									//Roullet
					selection_roullet(info, population,reproducts,POPULATION_SIZE_DEFINE);
					break;
				case 2:																									//B-tournament
					int B = 4;
					if (reproducts.size() < 7)
						B = reproducts.size() / 2;
					while (population.size() < POPULATION_SIZE_DEFINE - 1)
					{
						int t_max = -1, temp;
						for (int ir = 0; ir < B; ir++)
						{
							temp = rand() % reproducts.size();
							if (t_max == -1)
								t_max = temp;
								//
								//TODO
							else if ( true )
								//TODO
								//
								//reproducts[temp].individ_calc(info.price) > reproducts[t_max].individ_calc(info.price
								t_max = temp;
						}
						population.push_back(reproducts[t_max].copy());
						//reproducts.erase(reproducts.begin() + t_max);													// no returns
					}
					break;
				}
				population.push_back(oldMax.copy());																	//return old best
				step++;
				//now_best = population[find_best(population, info)].individ_calc(info.price);							//FIND BEST TODO
				if (now_best > prev_best)
				{
					static_best = 0;																					//
					prev_best = now_best;																				//STOP CHECK
				}																										//
				else
					static_best++;
				if (static_best > 10)																					//END COUNTER
					break;
			}
			if(FILE_ENABLE)
				generation_status("result population", population, info, step);											//RESULT OUT
			else
				generation_status(outfile,"result population", population, info, step);
	*/
	}
	if (outfile)
	{
		fclose(outfile);
		delete[] outfile;
	}
	printf("DONE");
	population.clear();
	reproducts.clear();
	delete[] argv;
	return 0;
}