#include <iostream>
#include <fstream>
#include <algorithm>

int main()
{
    std::ifstream file("../day2.txt");
    if (!file.is_open())
    {
        return -1;
    }

    int score = 0;
    int winstuff[3] = {
        1, 2, 0
    };
    int loosestuff[3] = {
        2, 0, 1
    };

    std::string line;
    while (std::getline(file, line))
    {
        int opponent = line[0] - 'A';
        //int input = line[2] - 'X';
        int input = line[2] == 'Y' ? opponent : (line[2] == 'X' ? loosestuff : winstuff)[opponent];

        bool win = (input == 0 && opponent == 2) || (input == 1 && opponent == 0) || (input == 2 && opponent == 1);
        score += input + 1 + (input == opponent ? 3 : (win ? 6 : 0));
    }

    std::cout << score << std::endl;
    return 0;
}
