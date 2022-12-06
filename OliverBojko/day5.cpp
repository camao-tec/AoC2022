#include <iostream>
#include <fstream>
#include <algorithm>
#include <vector>
#include <deque>

int main()
{
    std::ifstream file("../day5.txt");

    std::vector<std::deque<char>> blocks;
    bool state = false;

    for (std::string line; std::getline(file, line);)
    {
        if (!state)
        {
            if (line.empty())
            {
                state = true;
                continue;
            }

            for (size_t i = 0; i < line.size(); i++)
            {
                if (line[i] == '[')
                {
                    blocks.resize(std::max(blocks.size(), i / 4 + 1));
                    blocks[i / 4].push_front(line[i + 1]);
                }
                else
                    continue;
            }
        }
        else
        {
            int c, f, t;
            sscanf(line.c_str(), "move %d from %d to %d", &c, &f, &t);
            f--;
            t--;

            // part 1
            /*for (size_t i = 0; i < c; i++)
            {
                blocks[t].push_back(blocks[f].back());
                blocks[f].pop_back();
            }*/

            // part 2
            for (size_t i = 0; i < c; i++)
            {
                blocks[t].push_back(blocks[f][blocks[f].size() - c + i]);
            }

            for (size_t i = 0; i < c; i++)
                blocks[f].pop_back();
        }
    }

    for (auto &x: blocks)
        if (!x.empty())
            std::cout << x.back();

    return 0;
}
