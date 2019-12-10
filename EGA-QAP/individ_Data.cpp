#include "individ_Data.h"

individ_Data::individ_Data()
{
}

individ_Data::individ_Data(int count)
{
	for (int i = 0; i < count; i++)
		point.push_back(-1);
}
