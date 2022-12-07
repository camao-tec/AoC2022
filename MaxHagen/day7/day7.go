package main

import (
	"bufio"
	"flag"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Files map[string]*File

type File struct {
	Name     string
	Size     int
	Children Files
}

var inputFile *string

func main() {
	inputFile = flag.String("inputFile", "", "")
	flag.Parse()
	readFile, err := os.Open(*inputFile)
	if err != nil {
		fmt.Println(err)
	}
	scanner := bufio.NewScanner(readFile)
	var lines []string

	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}
	p1, p2 := solve(lines)
	fmt.Println("Part1:", p1)
	fmt.Println("Part2:", p2)
}

func sumSizes(dir *File) int {
	// It's a file
	if len(dir.Children) == 0 {
		return dir.Size
	}

	size := 0

	for k, file := range dir.Children {
		if k == ".." {
			continue
		}

		size += sumSizes(file)
	}

	dir.Size = size

	return size
}

func dfs(dir *File, total *int, spaceNeeded int, minSize *int) {
	if len(dir.Children) > 0 {
		if dir.Size <= 100000 {
			*total += dir.Size
		}
		if dir.Size >= spaceNeeded && dir.Size < *minSize {
			*minSize = dir.Size
		}
	}
	for k, file := range dir.Children {
		if k == ".." {
			continue
		}
		dfs(file, total, spaceNeeded, minSize)
	}
}

func solve(lines []string) (int, int) {
	current := &File{Name: "/", Children: Files{}}
	root := current
	for _, line := range lines[1:] {
		switch {
		case line[:4] == "$ cd":
			name := line[5:]
			current = current.Children[name]
		case line[:3] == "dir":
			name := line[4:]
			current.Children[name] = &File{
				Name:     name,
				Children: Files{"..": current},
			}
		case '0' <= line[0] && line[0] <= '9':
			split := strings.Split(line, " ")
			name := split[1]
			size, _ := strconv.Atoi(split[0])
			current.Children[name] = &File{Name: name, Size: size}
		}
	}

	sumSizes(root)
	total := 0
	freeSpace := 70000000 - root.Size
	spaceNeeded := 30000000 - freeSpace
	minSize := 1<<32 - 1
	dfs(root, &total, spaceNeeded, &minSize)

	return total, minSize
}
