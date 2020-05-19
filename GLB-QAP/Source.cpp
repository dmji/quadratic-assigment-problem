#include "Header.h"
#include "individ_Data.h"
#include "info_Data.h"

#include <iostream>
#include <fstream>
using namespace std;

double cost(individ_Data individSrc, info_Data &dataSrc)
{
	double res=0;
	for (int i = 0; i < individSrc.point.size(); i++)
		for (int j = 0; j < individSrc.point.size(); j++)
			res = res + (double)dataSrc.stream[individSrc.point[i]][individSrc.point[j]] * dataSrc.price[i][j] + dataSrc.position_cost[i][individSrc.point[j]];
	return res;
}

int calcL(int* a, int* b, int &size, int& out_a, int& out_b)
{
	int *min = new int[size - 1], *max = new int[size - 1], temp=0, k=0;
	for (int i = 0; i < size; i++)
	{
		if (i != out_a)
			min[k++] = a[i];
		if (i != out_b)
			max[temp++] = b[i];
	}

	for (int i = 0; i < size - 2; i++)
		if (min[i] > min[i + 1])
		{
			temp = min[i];
			min[i] = min[i + 1];
			min[i + 1] = temp;
			i -= 1;
		}
	for (int i = 0; i < size - 2; i++)
		if (max[i] < max[i + 1])
		{
			temp = max[i];
			max[i] = max[i + 1];
			max[i + 1] = temp;
			i-=1;
		}

	int res=0;
	for (int i = 0; i < size - 1; i++)
		res = res + min[i] * max[i];
	return res;
}

int main(int argc, const char* argv[])
{
	srand(time(0));
	info_Data data = info_Data();
	data.fileInput("ex1.txt");

	int ** L = new int*[data.problem_size];
	for(int i=0;i<data.problem_size;i++)
		L[i] = new int[data.problem_size];
	for (int i = 0; i < data.problem_size; i++)
		for (int j = 0; j < data.problem_size; j++)
			L[i][j] = calcL(data.price[i], data.stream[j], data.problem_size,i,j);

	for (int i = 0; i < data.problem_size; i++) {
		for (int j = 0; j < data.problem_size; j++)
			cout << L[i][j] << " ";
		cout << ";\n";
	}
	return 0;
}
