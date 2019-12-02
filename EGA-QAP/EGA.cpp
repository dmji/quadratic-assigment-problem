#include "EGA.h"

using namespace std;

FILE *outfile;

int main(int argc, const char * argv[]) // system_path FILE_NAME POPULATION_SIZE_DEFINE FIRST_GENERATION_DEFINE CROSSOVER_DEFINE MUTATION_DEFINE SELECTION_DEFINE SHOW_CONSOLE_OUT FILE_ENABLE LOOP_COUNT
{
	srand((unsigned int)time(NULL));
	info_Data info = info_Data();
	string outfPath;
	int POPULATION_SIZE_DEFINE = -1, FIRST_GENERATION_DEFINE = -1, CROSSOVER_DEFINE = -1, MUTATION_DEFINE = -1,	SELECTION_DEFINE = -1, SHOW_CONSOLE_OUT = 1, FILE_DISABLE=1, LOOP_COUNT = 1;
	if (argc > 1)
	{
		if (strlen(argv[1]) > 2) START_POPULATION_DEFINE = stoi(string(argv[1]).substr(2)); else START_POPULATION_DEFINE = atoi(argv[1]); // start SELECTION_DEFINE = {1,2}
		if (argc > 2)
			if (strlen(argv[2]) > 2) CROSSOVER_DEFINE = stoi(string(argv[2]).substr(2)); else CROSSOVER_DEFINE = atoi(argv[2]); // crossover SELECTION_DEFINE = {1,2}
		if (argc > 2)
			if (strlen(argv[3]) > 2) MUTATION_DEFINE = stoi(string(argv[3]).substr(2)); else MUTATION_DEFINE = atoi(argv[3]); // mutation SELECTION_DEFINE = {1,2}
		if (argc > 3)
			if (strlen(argv[4]) > 2) SELECTION_DEFINE = stoi(string(argv[4]).substr(2)); else SELECTION_DEFINE = atoi(argv[4]); // selection SELECTION_DEFINE = {1,2}
		if (argc > 4)
			if (strlen(argv[5]) > 2) POPULATION_SIZE_DEFINE = stoi(string(argv[5]).substr(2)); else POPULATION_SIZE_DEFINE = atoi(argv[5]); // start population = {1,2,...}
		if (argc > 6)
			if (strlen(argv[6]) > 2) LOOP_COUNT = stoi(string(argv[6]).substr(2)); else LOOP_COUNT = atoi(argv[6]); // loop count
		if (argc > 7)
			if (strlen(argv[7]) > 2) SHOW_CONSOLE_OUT = stoi(string(argv[7]).substr(2)); else SHOW_CONSOLE_OUT = atoi(argv[7]); // SHOW_CONSOLE_OUT
		if (argc > 8)
			if (strlen(argv[8]) > 2) FILE_DISABLE = stoi(string(argv[8]).substr(2)); else FILE_DISABLE = atoi(argv[8]); // SHOW_CONSOLE_OUT
		outfPath = outfPath + "test" + '\\' + "outFile " + argv[1] + " " + argv[2] + " " + argv[3] + " " + argv[4] + " " + argv[5] + " " + argv[6] + " " + argv[7];
	}
	if(FILE_DISABLE==0)																									//
		fopen_s(&outfile, (outfPath + ".txt").c_str(), "w+");															//open out file stream
																						//
//	vector<individ_Data> population = vector<individ_Data>();
//	vector<individ_Data> reproducts = vector<individ_Data>();															//
	info.fileInput(argv[1]);																							//input from file
	if (POPULATION_SIZE_DEFINE == -1)																					//
	{
		cout << "Enter start population size: ";
		scanf_s("%d", &POPULATION_SIZE_DEFINE);
	}
	int step = 0, now_best=0, prev_best=0, static_best=0;
	/*
	for (int LOOP_COUNTER = 0; LOOP_COUNTER < LOOP_COUNT; LOOP_COUNTER++)
	{
		step = 0; now_best = 0; prev_best = 0, static_best=0;
		if (START_POPULATION_DEFINE == -1)
		{
			cout << "Enter start population operator:\n 1-rand w/ DELETE_DEFINITION_WTR \n 2-jadnii danciga\n";
			scanf_s("%d", &START_POPULATION_DEFINE);
		}
		for(int i=0;i<POPULATION_SIZE_DEFINE;i++)																		//
			switch (START_POPULATION_DEFINE)																			//start population generating
			{																											//
			case 1:
				population.push_back(info.individ_generate_type1());
				break;
			case 2:
				population.push_back(info.individ_generate_type2());
				break;
			}
		while (true)																									//GENERAL EVOLUTION CYCLE
		{
			if (SHOW_CONSOLE_OUT)
			{
				if (FILE_DISABLE == 0)
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
				if (FILE_DISABLE == 0)
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
				if (FILE_DISABLE == 0)
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
		if(FILE_DISABLE)
			generation_status("result population", population, info, step);											//RESULT OUT
		else
			generation_status(outfile,"result population", population, info, step);
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
	*/
	return 0;
}