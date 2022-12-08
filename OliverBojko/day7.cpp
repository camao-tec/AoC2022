#include <iostream>
#include <fstream>
#include <algorithm>
#include <vector>
#include <deque>
#include <memory>
#include <functional>

struct DirEntry
{
    std::shared_ptr<DirEntry> parent{};
    std::string name;
    size_t size{};
    bool directory{};
    std::vector<std::shared_ptr<DirEntry>> files;

    std::shared_ptr<DirEntry> findFile(const std::string &n)
    {
        for (auto &x: files)
        {
            if (x->name == n)
                return x;
        }
        return nullptr;
    }

    size_t getTotalSize()
    {
        size_t childSize = 0;
        for (auto &x: files)
        {
            childSize += x->getTotalSize();
        }

        return size + childSize;
    }

    // does not include itself :(
    void iterateAllFiles(const std::function<void(std::shared_ptr<DirEntry> entry)> &func)
    {
        for (auto &x: files)
        {
            func(x);
            x->iterateAllFiles(func);
        }
    }
};

int main()
{
    std::ifstream file("../day7.txt");

    std::shared_ptr<DirEntry> root = std::make_shared<DirEntry>();
    root->name = "root";
    std::shared_ptr<DirEntry> wd = root;

    for (std::string line; std::getline(file, line);)
    {
        if (wd == nullptr)
        {
            std::cout << "fucked" << std::endl;
            continue;
        }

        auto pos = line.find("$ cd ");
        if (pos != std::string::npos)
        {
            std::string fileName = line.substr(pos + 5);

            if (fileName == "/")
                wd = root;
            else if (fileName == "..")
                wd = wd->parent; //pls no nullptr
            else
                wd = wd->findFile(fileName);

        }

        if (line[0] != '$')
        {
            if (line.find("dir") == 0)
            {
                std::string fileName = line.substr(pos + 5);
                std::shared_ptr<DirEntry> entry = std::make_shared<DirEntry>();
                entry->parent = wd;
                entry->name = fileName;
                entry->size = 0;
                entry->directory = true;

                wd->files.push_back(entry);
            }
            else if (line.find(' ') != std::string::npos)
            {
                size_t fileSize = std::stoi(line.substr(0, line.find(' ')));
                std::string fileName = line.substr(line.find(' ') + 1);

                std::shared_ptr<DirEntry> entry = std::make_shared<DirEntry>();
                entry->parent = wd;
                entry->name = fileName;
                entry->size = fileSize;

                wd->files.push_back(entry);
            }
        }
    }

    // part 1
    /*size_t totalSize = 0;
    root->iterateAllFiles([&totalSize](const std::shared_ptr<DirEntry> &entry)
                          {
                              if (!entry->directory)
                                  return;

                              size_t entrySize = entry->getTotalSize();
                              totalSize += entrySize <= 100000 ? entrySize : 0;
                          });

    std::cout << totalSize << std::endl;*/

    // part 2
    size_t minSize = INT32_MAX;
    root->iterateAllFiles([&minSize](const std::shared_ptr<DirEntry> &entry)
                          {
                              size_t entrySize = entry->getTotalSize();
                              if (entrySize > 8381165)
                                  minSize = std::min(minSize, entrySize);

                          });

    std::cout << minSize << std::endl;

    return 0;
}
