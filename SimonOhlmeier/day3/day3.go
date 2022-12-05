package main

import (
	"errors"
	"fmt"
	"os"
	"strings"
	"unicode"
)

func main() {
	data, err := os.ReadFile("/Users/simonohlmeier/Workspace/AoC2022/SimonOhlmeier/day3/input.txt")
	if err != nil {
		panic(err)
	}
	lines := strings.Split(string(data), "\n")
	sum := 0
	for _, line := range lines {
		compOne := line[0 : len(line)/2]
		compTwo := line[len(line)/2:]
		duplicate, err := findDuplicate(compOne, compTwo)
		if err == nil {
			val, err := calculateValue(duplicate)
			if err == nil {
				sum += val
			}
		}
	}
	sum2 := 0
	for i := 0; i < len(lines)-2; i += 3 {
		groupType, err := findGroupType(lines[i], lines[i+1], lines[i+2])
		if err == nil {
			val, err := calculateValue(groupType)
			if err == nil {
				sum2 += val
			}
		}

	}
	fmt.Println(sum)
	fmt.Println(sum2)
}

func findDuplicate(rucksackOne, rucksackTwo string) (rune, error) {
	for _, char := range rucksackOne {
		if strings.ContainsRune(rucksackTwo, char) {
			return char, nil
		}
	}
	return 0, errors.New("no duplicate")
}

func findGroupType(rucksackOne, rucksackTwo, rucksackThree string) (rune, error) {
	for _, char := range rucksackOne {
		if strings.ContainsRune(rucksackTwo, char) && strings.ContainsRune(rucksackThree, char) {
			return char, nil
		}
	}
	return 0, errors.New("no duplicate")
}

func calculateValue(char rune) (int, error) {
	if unicode.IsLower(char) {
		return int(char) - 96, nil
	} else {
		return int(char) - 38, nil
	}
	return 0, errors.New("invalid input")
}
