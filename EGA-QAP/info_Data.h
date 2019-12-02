#pragma once
#include "Header.h"

class info_Data
{
public:
	int** stream;
	int** price;
	int** position_cost;
	int count;

	void fileInput(const char* fname);																			//general constructor from file
	//info_Data(int pos_count);																					//general constructor
	info_Data();																								//empty constructor

//	individ_Data individ_generate_type1();
//	individ_Data individ_generate_type2();

	~info_Data();
};

