#include <iostream>
#include <fstream>
#include <algorithm>
#include <bitset>


int main()
{
    std::ifstream file("../day6.txt");

    std::string line;
    std::getline(file, line);

    int numDifferent = 14; //part1 = 4, part2 = 14

    for (size_t i = numDifferent - 1; i < line.size(); i++)
    {
        std::bitset<256> chars;
        for (size_t o = 0; o < numDifferent; o++)
            chars.set(line[i - o]);

        if (chars.count() == numDifferent)
        {
            std::cout << i + 1 << std::endl;
            return 0;
        }
    }

    return 0;
}
