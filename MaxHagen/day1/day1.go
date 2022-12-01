package main

import (
	"flag"
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

var inputFile *string

func main() {
	inputFile = flag.String("inputFile", "", "")
	flag.Parse()
	data, err := os.ReadFile(*inputFile)
	if err != nil {
		panic(err)
	}
	inputs := strings.Split(string(data), "\n")
	var totalCalories = make([]int, 239)
	elf := 0
	for _, v := range inputs {
		var calories int
		calories, err = strconv.Atoi(v)
		if err != nil {
			elf++
			continue
		}
		totalCalories[elf] += calories
	}
	sort.Ints(totalCalories)
	log.Printf("first: %d", totalCalories[len(totalCalories)-1])
	log.Printf("second: %d", totalCalories[len(totalCalories)-2])
	log.Printf("third: %d", totalCalories[len(totalCalories)-3])
}
