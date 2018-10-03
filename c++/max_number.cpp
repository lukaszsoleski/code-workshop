#include "stdafx.h"
#include <iostream>
#include <cmath>
#include <string>
using namespace std;
	int ask_size();
	void ask_numbers(int* array, int size);
	int max(int* array, int size);
	void print_results(int * array, int size, int maximum);
	int main()
	{
		// size of the array
		int size = ask_size(); 
		// pointer to the dinamically allocated array
		int* array = new int[size]; 
		ask_numbers(array,size);
		int maximum = max(array, size); 
		print_results(array,size, maximum);
		delete[] array; // free allocated memory
		array = NULL;
		cout << "\n"; 
		system("pause"); 

	}

	int ask_size() {
		int size = 0;

		do {
			cout << "Enter the size of the array:  ";
			cin >> size; 
			
			// print information about inappropriate size of the array
			if (size <= 0) {
				cout << "\n" << "Number must be greater than zero ! ";
			}
			// if the size of the array isn't positive number try again.  
		} while (size <= 0);
		return size; 
	}

	void ask_numbers(int* array, int size) {
		int temp; 
		for (int i = 0; i < size; i++) {
			cout << "Insert " << i + 1<< " number: ";
			cin >> temp;
			array[i] = temp;
		}
	}


	int max(int * array, int size) {
		int maximum = array[0]; 

		for (int i = 1; i < size; i++)
		{
			if (array[i] > maximum) {
				maximum = array[i]; 
			}
		}
		return maximum; 
	}
	void print_results(int * array, int size, int maximum) {
		cout << "\Array size : " << size; 
		cout << "\nNumbers:\n"; 
		for (int i = 0; i < size; i++) cout << array[i] << " "; 
		cout << "\nMaximum : " << maximum; 
	}