// Hangman.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string>
#include <iostream>
#include <vector>
#include <fstream>
#include <cstdlib>
#include <ctime>
#include <algorithm>
#include <windows.h>
using namespace std;





void PrintMessage(string message, bool printTop = true, bool printBottom = true) {
	if (printTop) {
		cout << "+---------------------------------+\n";
	}
	bool front = true;
	for (int i = message.length(); i < 33; i++) {
		if (front) message = " " + message;
		if (!front) message = message + " ";
		front = !front;
	}
	cout << "|" << message.c_str() << "|\n";

	if (printBottom) {
		cout << "+---------------------------------+\n";

	}
}
void DrawHangman(int guessCount) {
	if (guessCount < 0) guessCount = 0;
	if (guessCount > 10) guessCount = 10;
	string hangman[] = {

		"              |              ",//1
		"              |              ",//2
		"              O              ",//3
		"             /               ",//4
		"             /|              ",//5
		"             /|\\             ",//6
		"             /                ",//7
		"             / \\             ",//8
		"        +----------+         ",//9
		"        |          |         " //10
	};
	for (int i = 1; i <= guessCount; i++) {

		if (i == 4 && guessCount > 4) i++;
		if (i == 5 && guessCount > 5) i++;

		if (i == 7 && guessCount > 7) i++;
		PrintMessage(hangman[i - 1], false, false);
	}
}

void PrintWord(string word, bool guessed[]) {
	string output = "";
	for (int i = 0; i < word.length(); i++) {
		if (guessed[i]) {
			output += word[i];
		}
		else {
			output += " _ ";
		}
	}
	PrintMessage(output);
}
string Erase(string str, string occurrance) {
	str.erase(remove(str.begin(), str.end(), occurrance[0]), str.end());
	return str;
}
void ToUpper(string& input) {
	for (int i = 0; i < input.length(); i++) {
		input[i] = toupper(input[i]);
	}
}
void SkipBOM(std::ifstream &in)//https://stackoverflow.com/questions/10417613/c-reading-from-file-puts-three-weird-characters
{
	char test[3] = { 0 };
	in.read(test, 3);
	if ((unsigned char)test[0] == 0xEF &&
		(unsigned char)test[1] == 0xBB &&
		(unsigned char)test[2] == 0xBF)
	{
		return;
	}
	in.seekg(0);
}
vector<string> ReadFile(string path) {
	ifstream inFile(path.c_str());
	SkipBOM(inFile);
	vector<string> words;

	for (string line; getline(inFile, line);) {
		words.push_back(line);
	}
	inFile.close();
	return words;
}

static bool FirstSeed = true;
int RandomNumber(int high, int low = 0) {
	if (FirstSeed) {
		std::srand(std::time(0));
		FirstSeed = false;
	}
	if (low > high) return high;
	return low + (std::rand() % (high - low + 1));
}

string RandomizeString(string letters) {//unnecessery feature
	int length = letters.size(), i = 0, rnd1, rnd2;
	char *array = &letters[0];

	while (letters.length() > i) {
		rnd1 = RandomNumber(length - 1);
		rnd2 = RandomNumber(length - 1);

		if (rnd1 != rnd2)
		{
			char temp = array[rnd1];
			array[rnd1] = array[rnd2];
			array[rnd2] = temp;
		}
		i++;
	}
	return array;
}
string GetUserInput(string text) {
	string in;
	cout << text << " :";
	cin >> in;
	return in;
}
int ShowMenu() {

	PrintMessage("START GAME   play", true, false);
	PrintMessage("END GAME     exit", false, true);
	bool correct = false;
	int result = -1;


	do {
		string input = GetUserInput("What do you want to do?");

		if (input == "play") {
			result = 1;
			correct = true;
		}
		else if (input == "exit") {
			result = 0;
			correct = true;
		}
		if (!correct) PrintMessage("Incorect input", false, false);
	} while (!correct);


	return result;
}
bool IsInside(const std::string & str, char c)
{
	return str.find(c) != std::string::npos;
}
static vector<string> words = ReadFile("words.txt");
void RunGame() {
	bool isRunning = true, won = false;
	string temp;
	int range = words.size() - 1;
	int rnd = RandomNumber(range), mistakes = 0;
	string word = words[rnd], available = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
	cout << available;
	bool *guessed = new bool[word.length()];
	for (int i = 0; i < word.length(); i++) guessed[i] = false;
	// main loop
	while (isRunning == true && won == false)
	{
		system("cls");
		PrintMessage("HANGMAN");

		DrawHangman(mistakes);

		PrintWord(word, guessed);

		PrintMessage("Avaible letters: ", false, false);
		PrintMessage(available, false, false);
		bool letterOrWord = false;
		bool isLetter = false;

		while (!letterOrWord) {
			string dec = GetUserInput("(l)etter or (w)ord");
			if (dec == "l" || dec == "L") {
				letterOrWord = true;
				isLetter = true;
			}
			else if (dec == "w" || dec == "W") letterOrWord = true;
			if (!letterOrWord) PrintMessage("Correct your input to continue:)");
		}


		if (isLetter) {

			string l = GetUserInput("Write your letter");
			ToUpper(l);
			if (IsInside(available, l[0]) && IsInside(word, l[0])) {
				for (int i = 0; i < word.length(); i++) {
					if (word[i] == l[0]) guessed[i] = true;

				}
				available = Erase(available, l);
				PrintMessage("Nice Job");
				system("pause");
			}
			else
			{
				mistakes++;
				int left = 10 - mistakes;
				available = Erase(available, l);

				PrintMessage("Oops u have " + to_string(left) + " chances left");
				system("pause");

			}


		}
		else {
			string l = GetUserInput("Can u guess whole word? ");
			ToUpper(l);
			if (l == word) {
				won = true;
			}
			else {
				mistakes++;
				int left = 10 - mistakes;

				PrintMessage("Oops u have " + to_string(left) + " chances left");
				system("pause");
			}
		}


		bool allGood = true;
		for (int i = 0; i < word.length(); i++) {
			if (guessed[i] == false) {
				allGood = false;
				break;
			}
		}
		if (allGood) won = true;
		if (mistakes > 10) isRunning = false;

	}

	if (isRunning == false && won == false) {
		PrintMessage("unfortunately u lost 😞");
		PrintMessage("It was " + word);
	}
	else if (won == true) PrintMessage("Wooho Great Job! U guessed the word " + word);



}
void Start() {
	bool working = true;
	int menuChoise;
	PrintMessage("H A N G M A N");
	PrintMessage("MENU", false, false);

	while (working) {
		menuChoise = ShowMenu();
		switch (menuChoise) {
		case 0: working = false;
			break;
		case 1: RunGame();
			break;
		}
	}
}

int main()
{// todo:
 //  menu
 //  main loop


	Start();


	system("pause");
	getchar();
	return 0;
}



/*
+---------------------------------+
|             HANG MAN            |
+---------------------------------+
|               |                 |1
|               |                 |2
|               O                 |3
|              /|\                |4
|              / \                |5
|         +----------+            |6
|         |          |            |7
+---------------------------------+
|        Available letters        |
+---------------------------------+
|     A B C D E F G H I J K L M   |
|     N O P Q R S T U V W X Y Z   |
+---------------------------------+
|         Guess the word          |
+---------------------------------+
| _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ |
+---------------------------------+
>
*/