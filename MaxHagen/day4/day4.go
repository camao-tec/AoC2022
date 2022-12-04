package main

import (
	"bufio"
	"flag"
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

var inputFile *string

func main() {
	inputFile = flag.String("inputFile", "", "")
	flag.Parse()
	readFile, err := os.Open(*inputFile)
	if err != nil {
		fmt.Println(err)
	}
	fileScanner := bufio.NewScanner(readFile)
	fileScanner.Split(bufio.ScanLines)
	defer readFile.Close()
	total := 0
	total2 := 0
	for fileScanner.Scan() {
		parts := strings.Split(fileScanner.Text(), ",")
		partOne := strings.Split(parts[0], "-")
		partTwo := strings.Split(parts[1], "-")

		firstStart, _ := strconv.Atoi(partOne[0])
		firstEnd, _ := strconv.Atoi(partOne[1])
		secondStart, _ := strconv.Atoi(partTwo[0])
		secondEnd, _ := strconv.Atoi(partTwo[1])

		first := []int{}
		second := []int{}
		for i := firstStart; i <= firstEnd; i++ {
			first = append(first, i)
		}
		for i := secondStart; i <= secondEnd; i++ {
			second = append(second, i)
		}
		if contains(first, second) {
			total++
		}
		if checkForDuplicates(first, second) {
			total2++
		}

	}
	log.Println(total)
	log.Println(total2)
}

func contains(a []int, b []int) bool {
	counter := 0
	for _, item := range a {
		for _, itemB := range b {
			if item == itemB {
				counter++
			}
		}
	}
	if counter == len(a) || counter == len(b) {
		return true
	}
	return false
}

func checkForDuplicates(a []int, b []int) bool {
	var c = []int{}
	c = append(c, a...)
	c = append(c, b...)
	total := make(map[int]uint)
	for _, v := range c {
		total[v]++
	}
	for i, _ := range total {
		if total[i] > 1 {
			return true
		}
	}
	return false
}
