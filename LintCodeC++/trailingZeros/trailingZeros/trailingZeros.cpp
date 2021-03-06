

#include "pch.h"
#include <iostream>
#include <vector>
using namespace std;

//零的个数取决于因数数列中5的个数
//计算5的个数：
//75 / 5 = 15
//15 / 5 = 3
//5的个数为15 + 3 = 17
//奇数计算用向下取整：
//11 / 5 = 2
//2 / 5 = 0
//5的个数为2 + 0 = 2
long long trailingZeros(long long n)
{
	long long count = 0;
	while (n >= 5)
	{
		count += (n = n / 5);
	}
	return count;
}
int main()
{
	long long n;
	std::cin >> n;
    std::cout << trailingZeros(n); 
}

