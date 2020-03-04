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
	/*
	cout << "\n###\n";
	for (int i = 0; i < size - 1; i++)
		cout << min[i] << " ";
	cout << "\n###\n";
	for (int i = 0; i < size - 1; i++)
		cout << max[i] << " ";
	cout << "\n###\n";
	*/
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
	/*
	cout << "\n###\n";
	for (int i = 0; i < size-1; i++)
		cout << min[i] <<" ";
	cout << "\n###\n";
	for (int i = 0; i < size - 1; i++)
		cout << max[i] << " ";
	cout << "\n###\n";
	*/

	int res=0;
	for (int i = 0; i < size - 1; i++)
		res = res + min[i] * max[i];
	return res;
}

int main(int argc, const char* argv[])
{
	srand(time(0));
	info_Data data = info_Data();
	data.fileInput(argv[argc-1]);

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
	/*
	fstream fin;
	std::string excel_file = "LSA_toSolve.tsv";
	fin.open(excel_file.c_str(), ios::out);
	
	if (fin.is_open()) {
		for (int i = 0; i < data.problem_size; i++)
		{
			for (int j = 0; j < data.problem_size; j++)
			{
				fin << (float)L[i][j] << "\t";
			}
			fin << endl;
		}
		fin << endl;
		for (int i = 0; i < data.problem_size; i++)
		{
			for (int j = 0; j < data.problem_size; j++)
				fin << 0 << "\t";
			fin << "\t" << 1 << endl;
		}
		fin.close();
	}
	else
		cout << "FIN CORRUPTED";
	*/
	int min = 0,temp=-1, countZERO=0;
	

	cout << endl;
	for (int i = 0; i < data.problem_size; i++)
	{
		for (int j = 0; j < data.problem_size; j++)
				printf("%3d ", L[i][j]);
		cout << endl;
	}


	individ_Data permutation = individ_Data(data.problem_size);
	bool* flagX = new bool[data.problem_size],*flagY = new bool[data.problem_size];

	for (int i = 0; i < data.problem_size; i++)
	{
		flagX[i] = true;
		flagY[i] = true;
	}

	while (true) {
		for (int i = 0; i < data.problem_size; i++)
		{
			min = -1;
			for (int j = 0; j < data.problem_size; j++)
				//			if (permutation.point[i] == -1 && flag[j] == true)
			{
				if (min == -1)
					min = j;
				else if (L[i][min] > L[i][j])
					min = j;
			}
			for (int j = 0; j < data.problem_size; j++)
				//			if (permutation.point[i] == -1 && flag[j] == true)
				L[i][j] -= L[i][min];
		}

		for (int j = 0; j < data.problem_size; j++)
		{
			min = -1;
			for (int i = 0; i < data.problem_size; i++)
				//			if (permutation.point[i] == -1 && flag[j] == true)
			{
				if (min == -1)
					min = i;
				else if (L[min][j] > L[i][j])
					min = i;
			}
			for (int i = 0; i < data.problem_size; i++)
				//		if (permutation.point[i] == -1 && flag[j] == true)
				L[i][j] -= L[min][j];
		}



		cout << endl;
		for (int i = 0; i < data.problem_size; i++)
		{
			for (int j = 0; j < data.problem_size; j++)
				//			if (permutation.point[i] == -1 && flag[j] == true)
				printf("%3d ", L[i][j]);
			cout << endl;
		}



		//while (true) {
		cout << endl;
		for (int i = 0; i < data.problem_size; i++)
		{
			for (int j = 0; j < data.problem_size; j++)
				if (permutation.point[i] == -1 && flagX[j] == true && flagY[i] == true)
					printf("%3d ", L[i][j]);
			cout << endl;
		}

		for (int i = 0; i < data.problem_size; i++)
		{
			min = 0;
			temp = -1;
			for (int j = 0; j < data.problem_size; j++)
				if (flagX[j] == true && flagY[i] == true && L[i][j] == 0)
				{
					temp = j;
					min++;
				}
			if (min == 1)
			{
				flagX[temp] = false;
				flagY[i] = false;
				permutation.point[i] = temp;
				i = -1;
				permutation.print();
			}
		}

		for (int i = 0; i < data.problem_size; i++)
		{
			min = 0;
			temp = -1;
			for (int j = 0; j < data.problem_size; j++)
				if (flagX[i] == true && flagY[j] == true && L[j][i] == 0)
				{
					temp = j;
					min++;
				}
			if (min == 1)
			{
				flagX[i] = false;
				flagY[temp] == false;
				permutation.point[temp] = i;
				i = -1;
				permutation.print();
			}
		}


		min = 0;
		for (int i = 0; i < data.problem_size; i++)
			if (permutation.point[i] == -1)
				min++;

		if (min == 0)
			break;
	}


	permutation.print();

	cout << endl;
	for (int i = 0; i < data.problem_size; i++)
	{
		for (int j = 0; j < data.problem_size; j++)
		//	if (permutation.point[i] == -1 && flag[j] == true)
				printf("%3d ", L[i][j]);
		cout << endl;
	}


	cout << endl <<cost(permutation, data);

	
	return 0;
}
