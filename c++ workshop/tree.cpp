#include "stdafx.h"
#include <iostream>
#include <cmath>
using namespace std;


int main()
{
	// l, r : left and right range for writing '*' 
	// i, j - 
	// row - number of rows
	// col - number of culumns
	int row, col, l, r, i = 1;
	cout << "Xmas tree height : ";
	cin >> row;

	col = row * 2; 
	l = r = (col / 2); 
	while (i <= row)
	{
		for (int j = 1; j <= col; j++) {
			if (j < l || j > r)
				cout << " ";
			else
				cout << "*";
		}// end for 
		cout << endl;
		l--;
		r++;
		i++;
	}// end while 

	for (int k = 1; k < col; k++) {
		if (k == col / 2)
		{
			cout << "#"; 
			break;
		}
		cout << " "; 
	}

	
	cout << "\n"; 
	system("pause");

}