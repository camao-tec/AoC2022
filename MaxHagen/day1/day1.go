package main

import (
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	data, err := os.ReadFile("/Users/maximilianhagen/Development/Camao/AoC2022/MaxHagen/day1/input.txt")
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
