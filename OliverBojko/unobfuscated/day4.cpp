#include <iostream>
#include <fstream>
#include <algorithm>

int main()
{
    std::ifstream file("../day4.txt");
    if (!file.is_open())
        return -1;


    std::string line;
    int count = 0;
    while (std::getline(file, line))
    {
        int a, b, c, d;
        sscanf(line.c_str(), "%d-%d,%d-%d", &a, &b, &c, &d);
        count += !(b < c || a > d);
    }

    std::cout << count << std::endl;
    return 0;
}

//part 1
//(a <= c && b >= d) || (c <= a && d >= b)
//part 2
//!(b < c || a > d)