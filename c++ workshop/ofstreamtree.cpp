#include "stdafx.h"
#include <iostream>
#include <string>
#include <fstream>
using namespace std;
	int ask_size();
	string get_tree(int size);
	
	int main()
	{
		
		string result = get_tree(ask_size()); 
		
		cout << result;
		ofstream output("tree.txt");
		output << result; 
		cout << "Result saved to the following file: tree.txt ";
		cout << "\n"; 
		system("pause"); 
	}

	int ask_size() {
		int size = 0;

		do {
			cout << "Enter the size of the tree:  ";
			cin >> size; 
			
			// print information about inappropriate size of the array
			if (size <= 0) {
				cout << "\n" << "Number must be greater than zero ! ";
			}
			// if the size of the array isn't positive number ask again. 
		} while (size <= 0);
		return size; 
	}


	string get_tree(int size)
	{
		// l, r : left and right range for writing '*' 
		// i, j - 
		// row - number of rows
		// col - number of culumns
		int row = size, col, l, r, i = 1;
		// 
		string tree = "";
		

		col = row * 2;
		l = r = (col / 2);
		while (i <= row)
		{
			for (int j = 1; j <= r; j++) {
				if (j < l)
					tree.append(" ");
				else
					tree.append("*");
			}// end for 
			tree.append("\n");
			l--;
			r++;
			i++;
		}// end while 

		for (int k = 1; k < col; k++) {
			if (k == col / 2)
			{
				tree.append("#");
				break;
			}
			tree.append(" ");
		}


		tree.append("\n");
		return tree;

	}
