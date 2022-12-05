package main

import (
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	data, err := os.ReadFile("/Users/simonohlmeier/Workspace/AoC2022/SimonOhlmeier/day4/input.txt")
	if err != nil {
		panic(err)
	}
	lines := strings.Split(string(data), "\n")

	sum := 0
	sum2 := 0
	for _, line := range lines {
		sections := strings.Split(line, ",")
		sectionOne := strings.Split(sections[0], "-")
		sectionTwo := strings.Split(sections[1], "-")
		startOne, _ := strconv.Atoi(sectionOne[0])
		endOne, _ := strconv.Atoi(sectionOne[1])
		startTwo, _ := strconv.Atoi(sectionTwo[0])
		endTwo, _ := strconv.Atoi(sectionTwo[1])

		rangeOne := makeRange(startOne, endOne)
		rangeTwo := makeRange(startTwo, endTwo)

		if containsSlice(rangeOne, rangeTwo) {
			sum++
		}
		if containsDuplicate(rangeOne, rangeTwo) {
			sum2++
		}

	}
	log.Println(sum)
	log.Println(sum2)
}

func makeRange(min, max int) []int {
	a := make([]int, max-min+1)
	for i := range a {
		a[i] = min + i
	}
	return a
}

func containsSlice(first, second []int) bool {
	sum := 0
	for _, elementA := range first {
		for _, elementB := range second {
			if elementA == elementB {
				sum++
			}
		}
	}
	if len(first) == sum {
		return true
	} else if len(second) == sum {
		return true
	}
	return false
}

func containsDuplicate(first, second []int) bool {
	for _, val := range first {
		for _, val2 := range second {
			if val == val2 {
				return true
			}
		}
	}
	return false
}
