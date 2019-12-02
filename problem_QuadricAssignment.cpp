#include "problem_QuadricAssignment.h"

info_Data::info_Data()
{
	count = 0;
	stream = NULL;
	price = NULL;
}

individ_Data info_Data::individ_generate_type1()
{
	return individ_Data();
}

individ_Data info_Data::individ_generate_type2()
{
	return individ_Data();
}

info_Data::info_Data(int t_count)
{
	count = t_count;
	stream = new int*[count];
	price = new int*[count];
	for (int i = 0; i < count; i++)
	{
		stream[i] = new int[count];
		price[i] = new int[count];
	}
}

info_Data::~info_Data()
{
	for (int i = 0; i < count; i++)
		delete[] stream[i], price[i];
	delete[] stream, price;
}

void info_Data::fileInput(const char* fname, int SHOW_CONSOLE_OUT)
{
	FILE *file;
	char temp, *buf, test[10];
	int BUF_SIZE = 1, uk_w = 0, uk_p = 0, uk_d = 0, diver = 0;
	fopen_s(&file, "ex1.txt", "r");
	if (file == NULL)
	{
		printf("Cannot open file.\n");
		exit(1);
	}
	else
	{
		while(true)
		{
			temp = fgetc(file);
			if (temp != EOF)
			{
				BUF_SIZE++;
				if (temp == ' ' || temp == '\n' || temp == '\0')
					uk_d++;
			}
			else
				break;
		}
		buf = new char[BUF_SIZE];
		rewind(file);
		fread(buf, BUF_SIZE - 1, sizeof(char), file);
		fclose(file);
		/*
		weight = new int[count];
		price = new int[count];
		test[0] = '\0';
		for (int i = 0; i < count; i++)
		{
			if ((buf[i] == ' ' || buf[i] == '\0') && test[0] != '\0')
			{
				switch (diver % 3)
				{
				case 0:
					break;
				case 1:
					price[uk_p++] = atoi(test);
					break;
				case 2:
					weight[uk_w++] = atoi(test);
					break;
				}
				diver++;
				uk_d = 0;
				for (int o = 0; o < 10; o++)
					test[o] = 0;
				test[0] = '\0';
			}
			if (buf[i] >= 48 && buf[i] <= 57)
			{
				test[uk_d++] = buf[i];
			}
		}
		if (SHOW_CONSOLE_OUT == 1)
		{
			printf("");
		}
	}*/
	delete[] file, buf;
}

individ_Data::individ_Data()
{

}

individ_Data::individ_Data(int count)
{
	itemCount = count;
	for (int i = 0; i < count; i++)
		items.push_back(0);
}

individ_Data& individ_Data::copy()						//UNIVERSAL
{
	individ_Data *res = new individ_Data(itemCount);
	res->itemCount = itemCount;
	for (int i = 0; i < itemCount; i++)
		res->items[i] = items[i];
	return *res;
}

int& individ_Data::operator[](int index)				//UNIVERSAL
{
	return items[index];
}

bool individ_Data::operator==(individ_Data &src)		//UNIVERSAL + CORRECT
{
	for (int i = 0; i < itemCount; i++)
		if (src[i] != items[i])
		{
			return false;
		}
	return true;
}

int individ_Data::individ_calc(int *data)
{
	//
	//TODO
	//
	return 0;
}

void individ_Data::individ_correcting(info_Data &info)
{
	int temp_individ_calc = individ_calc(info.weight),
		temp,
		uk_t;
	do {
		temp = -1; uk_t = -1;
		for (int p = 0; p < info.count; p++)
			if (items[p] == 1)
			{
				if (temp == -1)
					temp = p;
				else if (info.price[temp] / info.weight[temp] < info.price[p] / info.weight[p])
					temp = p;
				if (temp_individ_calc - info.weight[p] <= info.maxWeight)
				{
					if (uk_t == -1)
						uk_t = p;
					else if (info.price[uk_t] < info.price[p])
						uk_t = p;
				}
			}
		if (uk_t == -1)
			items[temp] = 0;
		else
			items[uk_t] = 0;
		temp_individ_calc = individ_calc(info.weight);
	} while (temp_individ_calc > info.maxWeight);
}

individ_Data individ_Data::generate_type1(info_Data &info)
{
	int temp;
	do
	{
		temp = rand() % (int)pow(2.0, (double)info.count);
		for (int ir = 0; ir < info.count; ir++)
		{
			items[info.count - ir - 1] = temp % 2;
			temp /= 2;
		}
	} while (individ_calc(info.weight) >= info.maxWeight);
	return *this;
}

individ_Data individ_Data::generate_type2(info_Data & info)
{
	int *sort = new int[info.count],
		*checked = new int[info.count],
		evristic_probability = 60;							// 0.6
	for (int i = 0; i < info.count; i++)
	{
		sort[i] = -1;
		checked[i] = 0;
	}
	for (int i = 0; i < info.count; i++)
	{
		for (int ir = 0; ir < info.count; ir++)
		{
			if (checked[ir] == 0)
			{
				if (sort[i] == -1)
				{
					sort[i] = ir;
				}
				else if (sort[i] != -1)
				{
					if (double(info.price[sort[i]]) / info.weight[sort[i]] < double(info.price[ir]) / info.weight[ir])
					{
						sort[i] = ir;
					}
				}
			}
		}
		checked[sort[i]] = 1;
	}
	for (int ir = 0; ir < info.count; ir++)
	{
		if (rand() % 100 < evristic_probability && individ_calc(info.weight) + info.weight[sort[ir]] <= info.maxWeight)
		{
			items[sort[ir]] = 1;
		}
		else
			items[sort[ir]] = 0;
	}
	delete[] sort, checked;
	return *this;
}

void individ_Data::mutation_type3() // saltation - trash
{
	int uk1, uk2, temp;
	uk1 = rand() % itemCount;
	do {
		uk2 = rand() % itemCount;
	} while (uk1 == uk2);
	temp = items[uk1];
	items[uk1] = items[uk2];
	items[uk2] = temp;
}

void individ_Data::mutation_type2() // inversion
{
	int uk1, uk2;
	uk1 = rand() % (itemCount / 2);
	uk2 = uk1 + (1 + rand() % (itemCount / 2));
	for (int ir = uk1; ir < uk2; ir++)
		items[ir] = (items[ir] + 1) % 2;
}

void individ_Data::mutation_type1() // inversion
{
	int uk1, uk2;
	uk1 = rand() % (itemCount / 2);
	uk2 = uk1 + (1 + rand() % (itemCount / 2));
	for (int ir = 0; ir < itemCount; ir++)
		if (ir<uk1 || ir>uk2)
			items[ir] = (items[ir] + 1) % 2;
}

void crossover_type1(std::vector<individ_Data> &reproducts, individ_Data & src1, individ_Data & src2)
{
	int	t_rand1, t_rand2, force_control = 1;
	std::vector<individ_Data> vec_indiv = std::vector<individ_Data>();
	t_rand1 = 1 + (rand() % (src1.itemCount / 2));
	t_rand2 = (src1.itemCount / 2) + (rand() % (src1.itemCount / 2));
	for (int j = 0; j < 8; j++)
		vec_indiv.push_back(src1.copy());
	for (int i = 0; i < src1.itemCount; i++)
	{
		if (i<t_rand1 || i>t_rand2)
		{
			for (int j = 0; j < vec_indiv.size();)
			{
				for (int pos = 0; pos < vec_indiv.size() / pow(2, force_control); pos++)
				{
					vec_indiv[j++][i] = src1[i];
				}
				for (int pos = 0; pos < vec_indiv.size() / pow(2, force_control); pos++)
				{
					vec_indiv[j++][i] = src2[i];
				}
			}
			force_control++;
		}
		else if (i < t_rand2)
		{
			for (int j = 0; j < vec_indiv.size();)
			{
				for (int pos = 0; pos < vec_indiv.size() / pow(2, force_control); pos++)
				{
					vec_indiv[j++][i] = src2[i];
				}
				for (int pos = 0; pos < vec_indiv.size() / pow(2, force_control); pos++)
				{
					vec_indiv[j++][i] = src1[i];
				}
			}
			force_control++;
		}
	}
	for (int i = 0; i < 8; i++)
		//if (population_variety_control(reproducts, vec_indiv[i]))
		reproducts.push_back(vec_indiv[i]);
}

void crossover_type3(std::vector<individ_Data> &reproducts, individ_Data & src1, individ_Data & src2)
{
	int	t_rand;
	individ_Data temp_indiv1 = individ_Data(src1.itemCount);
	individ_Data temp_indiv2 = individ_Data(src1.itemCount);
	t_rand = 3 + (rand() % (src1.itemCount - 6));
	for (int j = 0; j < src1.itemCount; j++)
	{
		if (j < t_rand)
		{
			temp_indiv1[j] = src1[j];
			temp_indiv2[j] = src2[j];
		}
		else
		{
			temp_indiv1[j] = src1[j];
			temp_indiv2[j] = src2[j];
		}
	}
	//std::cout << "\n $$% " << reproducts.size() << std::endl;
	//if (population_variety_control(reproducts, temp_indiv1))
	reproducts.push_back(temp_indiv1);
	//if (population_variety_control(reproducts, temp_indiv2))
	reproducts.push_back(temp_indiv2);
	//std::cout << "\n $$% " << reproducts.size() << " OK "<<std::endl;
}

void crossover_type2(std::vector<individ_Data> &reproducts, individ_Data & src1, individ_Data & src2)
{
	std::vector<individ_Data> vec_indiv = std::vector<individ_Data>();
	int force_control = 1, size, del_hem = 0, rand_hem;
	if (Hemming(src1, src2) > 0)
	{
		if (Hemming(src1, src2) > 2)
		{
			rand_hem = 2 + (rand() % (1 + ((Hemming(src1, src2) - 2) / 2)));
			size = pow(2, rand_hem);
		}
		else
		{
			rand_hem = Hemming(src1, src2);
			size = pow(2, rand_hem);
		}
		bool checkCycle = true;
		for (int i = 0; i < size; i++)
			vec_indiv.push_back(individ_Data(src1.itemCount));
		for (int i = 0; i < src1.itemCount; i++)
		{
			if (src1[i] == src2[i])
			{
				for (int j = 0; j < vec_indiv.size(); j++)
					vec_indiv[j][i] = src1[i];
			}
			else
			{
				if (Hemming(src1, src2) - del_hem <= rand_hem || rand() % 10 < 6)
				{
					for (int j = 0; j < vec_indiv.size();)
					{
						for (int pos = 0; pos < vec_indiv.size() / pow(2, force_control); pos++)
						{
							vec_indiv[j++][i] = 0;
						}
						for (int pos = 0; pos < vec_indiv.size() / pow(2, force_control); pos++)
						{
							vec_indiv[j++][i] = 1;
						}
					}
					force_control++;
					del_hem++;
				}
				else
					for (int j = 0; j < vec_indiv.size(); j++)
						vec_indiv[j][i] = src1[i];
			}
		}
		for (int i = 0; i < vec_indiv.size(); i++)
			//if (population_variety_control(reproducts, vec_indiv[i]))
			reproducts.push_back(vec_indiv[i]);
	}
}

bool population_variety_control(std::vector<individ_Data>& population, individ_Data & src)				//UNIVERSAL
{
	for (std::vector<individ_Data>::iterator ptr = population.begin(); ptr != population.end(); ptr++)
		if (ptr->operator==(src))
			return false;
	return true;
}

int Hemming(individ_Data &x, individ_Data &y)
{
	int res = 0;
	for (int i = 0; i < x.itemCount; i++)
		if (x[i] != y[i])
			res++;
	return res;
}

int find_best(std::vector<individ_Data> &population, info_Data &info)
{
	int control = -1;
	for (int i = 0; i < population.size(); i++)
	{
		if (control != -1)
		{
			if (population[control].individ_calc(info.price) < population[i].individ_calc(info.price))
				control = i;
		}
		else
		{
			control = i;
		}
	}
	return control;
}