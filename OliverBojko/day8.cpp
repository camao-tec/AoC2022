#include <iostream>
#include <fstream>
#include <algorithm>
#include <vector>

struct Tree
{
    bool visible{};
    int height;

    [[nodiscard]] bool isVisibleFrom(Tree other) const
    {
        return other.visible && height > other.height;
    }
};

int main()
{
    std::ifstream file("../day8.txt");

    std::vector<Tree> map;

    std::vector<std::string> lines;
    for (std::string line; std::getline(file, line);)
        lines.push_back(line);

    size_t xSize = 0;
    size_t ySize = lines.size();

    for (size_t y = 0; y < lines.size(); y++)
    {
        xSize = std::max(lines[y].size(), xSize);
        for (size_t x = 0; x < lines[y].size(); x++)
            map.push_back(Tree{x == 0 || y == 0 || x == xSize - 1 || y == ySize - 1, lines[y][x] - '0'});
    }


    auto getTree = [xSize, ySize, &map](size_t x, size_t y) -> Tree
    {
        if (x < 0 || x >= xSize || y < 0 || y >= ySize)
            return Tree{true, 0};
        return map[x + y * xSize];
    };

    auto setTree = [xSize, ySize, &map](size_t x, size_t y, Tree tree)
    {
        if (x < 0 || x >= xSize || y < 0 || y >= ySize)
            return;
        map[x + y * xSize] = tree;
    };

    //omgggg soo shitcode

    //part 1
    /*for (int y = 0; y < ySize; y++)
        for (int x = 0; x < xSize; x++)
        {
            Tree cur = getTree(x, y);

            if (cur.visible)
                continue;

            bool visible = false;

            bool a = true;
            for (int j = x - 1; j >= 0; j--)
                a &= getTree(j, y).height < cur.height;
            visible |= a;
            a = true;
            for (int j = y - 1; j >= 0; j--)
                a &= getTree(x, j).height < cur.height;
            visible |= a;
            a = true;
            for (int j = x + 1; j < xSize; j++)
                a &= getTree(j, y).height < cur.height;
            visible |= a;
            a = true;
            for (int j = y + 1; j < ySize; j++)
                a &= getTree(x, j).height < cur.height;
            visible |= a;


            cur.visible = visible;
            setTree(x, y, cur);
        }


    size_t count = 0;
    for (size_t y = 0; y < ySize; y++)
    {
        for (size_t x = 0; x < xSize; x++)
        {
            Tree cur = getTree(x, y);
            std::cout << (cur.visible ? "#" : ".");
            count += cur.visible;
        }

        std::cout << std::endl;
    }

    std::cout << count << std::endl;*/

    int maxScore = 0;

    //omgggg soo shitcode

    for (int y = 0; y < ySize; y++)
        for (int x = 0; x < xSize; x++)
        {
            Tree cur = getTree(x, y);

            if (cur.visible)
                continue;

            int score = 0;

            int a = 0;
            for (int j = x - 1; j >= 0; j--)
            {
                a++;
                if (getTree(j, y).height >= cur.height)
                    break;
            }
            score = a;
            a = 0;
            for (int j = y - 1; j >= 0; j--)
            {
                a++;
                if (getTree(x, j).height >= cur.height)
                    break;
            }
            score *= a;
            a = 0;
            for (int j = x + 1; j < xSize; j++)
            {
                a++;
                if (getTree(j, y).height >= cur.height)
                    break;
            }
            score *= a;
            a = 0;
            for (int j = y + 1; j < ySize; j++)
            {
                a++;
                if (getTree(x, j).height >= cur.height)
                    break;
            }
            score *= a;
            maxScore = std::max(score, maxScore);
        }

    std::cout << maxScore << std::endl;

    return 0;
}
