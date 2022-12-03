#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>

int main()
{
    std::ifstream file("../day1.txt");
    if (!file.is_open())
    {
        return -1;
    }

    std::vector<int> calories;

    int tempCalories = 0;
    std::string line;
    while (std::getline(file, line)) {
        if(line.length() <= 0)
        {
            calories.push_back(tempCalories);
            tempCalories = 0;
            continue;
        }

        tempCalories += std::stoi(line);
    }

    if (calories.size() < 3){
        return -1;
    }

    std::sort(calories.begin(), calories.end(), std::greater<>());

    std::cout << calories[0] + calories[1] + calories[2] << std::endl;
    return 0;
}
