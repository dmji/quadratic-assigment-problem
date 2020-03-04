#include "info_Data.h"
#include <string>

void info_Data::fileInput(std::string &fname)
{
	FILE* file;
	char temp, * buf;
	std::string test="";
	int BUF_SIZE = 1, uk_w = 0, uk_p = 0, uk_d = 0, diver = 0,uk_t=0;
	fopen_s(&file, fname.c_str(), "r");
	if (file == NULL)
	{
		printf("Cannot open file.\n");
		exit(1);
	}
	else
	{
		while (true)
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
		fread(buf, BUF_SIZE, sizeof(char), file);
		fclose(file);
		buf[BUF_SIZE - 1] = '\0';
		while (true)
		{
			if (buf[uk_t] != ' ' && buf[uk_t] != '\n' && buf[uk_t] != '\0')
				test += buf[uk_t++];
			else
				break;
		}
		problem_size = stoi(test);
		stream = new int* [problem_size];
		price = new int* [problem_size];
		position_cost = new int* [problem_size];
		for (int i = 0; i < problem_size; i++)
		{
			stream[i] = new int[problem_size];
			price[i] = new int[problem_size];
			position_cost[i] = new int[problem_size];
		}
		test = "";
		uk_t++;
		for (;  uk_t < BUF_SIZE;  uk_t++)
		{
			if (buf[uk_t] != ' ' && buf[uk_t] != '\n' && buf[uk_t] != '\0')
				test += buf[uk_t];
			else
			{
					if (uk_w == problem_size)
					{
						uk_w = 0;
						uk_p++;
					}
					if (uk_p == problem_size)
					{
						uk_p = 0;
						diver++;
						//uk_t++;
					}				
					if (test.length() > 0)
					{
					switch (diver)
					{
					case 0:
						price[uk_p][uk_w++] = stoi(test);
						break;
					case 1:
						stream[uk_p][uk_w++] = stoi(test);
						break;
					case 2:
						position_cost[uk_p][uk_w++] = stoi(test);
						break;
					}
					test = "";
					}
			}
		}
		if (diver < 2)
			for (int i = 0; i < problem_size; i++)
				for (int ii = 0; ii < problem_size; ii++)
					position_cost[i][ii] = 0;

		//check out
		std::cout << "Size: " << problem_size << "\n";
		std::cout << "Price per stream point\n";
		for (int i = 0; i < problem_size; i++)
		{
			for (int ii = 0; ii < problem_size; ii++)
				std::cout << price[i][ii] << " ";
			std::cout << '\n';
		}
		std::cout << "Stream point problem_size\n";
		for (int i = 0; i < problem_size; i++)
		{
			for (int ii = 0; ii < problem_size; ii++)
				std::cout << stream[i][ii] << " ";
			std::cout << '\n';
		}
		std::cout << "Assigment cost\n";
		for (int i = 0; i < problem_size; i++)
		{
			for (int ii = 0; ii < problem_size; ii++)
				std::cout << position_cost[i][ii] << " ";
			std::cout << '\n';
		}
		//out end
		delete[] file, buf;
	}

}

info_Data::info_Data()
{
	problem_size = 0;
	stream = NULL;
	price = NULL;
	position_cost = NULL;
}

info_Data::~info_Data()
{
	if(problem_size !=0)
		for (int i = 0; i < problem_size; i++)
		{
			delete[] stream[i], price[i], position_cost[i];
		}
	delete[] stream, price, position_cost;
}
