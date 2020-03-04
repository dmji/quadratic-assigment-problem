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

int main(int argc, const char* argv[])
{
	int i = 0;
	string path = "tests\\";
	srand(time(0));
	info_Data data = info_Data();
	data.fileInput(path.operator+= (argv[argc - 1]));

	vector<int> resCOST;
	vector<int> resCOUNT;
	
	fstream fin;
	std::string excel_file = "F_"+string(argv[argc-1])+"_result.tsv";
	fin.open(excel_file.c_str(), ios::out);

	if (fin.is_open()) {
		for (int ITER_COUNT = 0; ITER_COUNT < atoi(argv[1]); ITER_COUNT++)
		{
			i = 0;
			individ_Data permutation = individ_Data(data.problem_size);
			permutation.fill_random();
			
			//permutation.point = { 2,20,25,5,26,6,23,15,22,19,12,14,9,11,3,8,18,13,1,24,4,7,10,16,17,21 };
			//permutation.point = { 10,6,2,12,11,3,5,8,9,1 };
			//permutation.point = { 2,4,9,3,6,1,8,10,5,7 };
			//for (int u = 0; u < permutation.point.size() - 1; u++)
			//	permutation.point[u]--;
			
			individ_Data temp = individ_Data(permutation);
			individ_Data min = individ_Data(permutation);
			do {
				permutation = min;
				for (int u = 0; u < permutation.point.size() - 1; u++)
				{
					for (int y = u + 1; y < permutation.point.size(); y++)
					{
						i++;
						temp = permutation;
						int swap = temp.point[y];
						temp.point[y] = temp.point[u];
						temp.point[u] = swap;
						if (cost(temp, data) < cost(min, data))	
							min = temp;
					}
				}
				cout << endl << "# "<< cost(permutation, data);
			} while (cost(permutation, data) != cost(min, data));
			cout << "\nResult: ";
			permutation.print();
			printf("\n");
			int result = cost(permutation, data);
			/*
			for (int i = 0; i < permutation.point.size(); i++)
				fin << permutation.point[i] << " ";
			fin << "\tCOST\t" << result << "\tCALCUL_DIFF\t" << i << endl;
			*/
			cout << "\tCOST\t" << result << "\tCALCUL_DIFF\t" << i << endl;
			
			bool check = true;
			for (int i = 0; i < resCOST.size(); i++)
			{
				if (resCOST[i] == result)
				{
					resCOUNT[i]++;
					check = false;
				}
			}
			if (check)
			{
				resCOST.push_back(result);
				resCOUNT.push_back(1);
			}
		}
		//fin << endl;
		//for (int i = 0; i < resCOST.size(); i++)
		//	fin << resCOST[i] << "\t" << resCOUNT[i] << endl;
		//fin.close();
	}
	else {
		cout << "Failed To Open File" << endl;
	}
	return 1;
}
