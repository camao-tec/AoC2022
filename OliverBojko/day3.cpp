#include <iostream>
#include <fstream>
#include <algorithm>
#include <vector>

int main()
{
    std::ifstream file("../day3.txt");
    if (!file.is_open())
        return -1;

    // for part 2 :P
    int sum = 0, c, i = 0, aaamakaka = 0;
    std::vector<std::string> wtfff;
    for (std::string line; std::getline(file, line); i++)
        if (wtfff.push_back(line), i % 3 == 2)
            c = (u_char)*std::find_if(line.begin(), line.end(), [&wtfff, &aaamakaka](char a) -> bool
            {
                for (size_t g = 0; g < wtfff.size() - 1; g++) aaamakaka += std::find(wtfff[g].begin(), wtfff[g].end(), a) != wtfff[g].end();
                return aaamakaka == (aaamakaka = 0, wtfff.size()) - 1;
            }), wtfff.clear(), sum += isupper(c) ? c - '&' : c - '`';

    // make syntax highlighting work
    if (0)
    {
        // for part 1
        //int sum = 0, c;
        for (std::string line; std::getline(file, line);)
            c = (u_char)*std::find_if(line.begin(), line.begin() + (int)line.size(), [&line](char a) -> bool
            { return std::find(line.begin() + (int)line.size() / 2, line.end(), a) != line.end(); }), sum += isupper(c) ? c - '&' : c - '`';
    }

    std::cout << sum << std::endl;
    return 0;
}
