#pragma once
#include "Header.h"
#include "individ_Data.h"

class info_Data
{
public:
	int** stream;																								//D-matrix
	int** price;																								//F-matix
	int** position_cost;																						//C-matrix
	int problem_size;																							//n

	void fileInput(const char* fname);																			//general constructor from file
	
	info_Data();																								//empty constructor
	~info_Data();
};

