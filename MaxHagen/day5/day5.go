package main

import (
	"bufio"
	"log"
	"os"
	"regexp"
	"strconv"
	"time"
)

func main() {
	lines := GetLines("/Users/maximilianhagen/Development/Camao/AoC2022/MaxHagen/day5/input.txt")
	start := time.Now()
	timeElapsed := time.Since(start)
	log.Printf("Part1: %s Part2: %s Took: %dms", part1(lines, getStacks()), part2(lines, getStacks()), timeElapsed.Milliseconds())
}

func part2(lines []string, stacks map[int][]string) string {
	for _, line := range lines {
		re := regexp.MustCompile("[0-9]+")
		numbers := re.FindAllString(line, -1)
		count, _ := strconv.Atoi(numbers[0])
		from, _ := strconv.Atoi(numbers[1])
		to, _ := strconv.Atoi(numbers[2])
		origin := stacks[from]
		destination := stacks[to]
		crates := origin[0:count]
		newOrigin := origin[count:]
		newDestination := append([]string{}, crates...)
		newDestination = append(newDestination, destination...)
		stacks[from] = newOrigin
		stacks[to] = newDestination
	}
	return getAnswer(stacks)
}

func part1(lines []string, stacks map[int][]string) string {
	for _, line := range lines {
		re := regexp.MustCompile("[0-9]+")
		numbers := re.FindAllString(line, -1)
		count, _ := strconv.Atoi(numbers[0])
		from, _ := strconv.Atoi(numbers[1])
		to, _ := strconv.Atoi(numbers[2])
		origin := stacks[from]
		destination := stacks[to]

		for i := 0; i < count; i++ {
			crate := origin[0]
			origin = origin[1:]
			destination = append([]string{crate}, destination...)
			stacks[from] = origin
			stacks[to] = destination
		}
	}
	return getAnswer(stacks)
}

func getAnswer(stacks map[int][]string) string {
	var answer string
	for i := 1; i <= len(stacks); i++ {
		answer += stacks[i][0]
	}
	return answer
}

func getStacks() map[int][]string {
	one := []string{"N", "D", "M", "Q", "B", "P", "Z"}
	two := []string{"C", "L", "Z", "Q", "M", "D", "H", "V"}
	three := []string{"Q", "H", "R", "D", "V", "F", "Z", "G"}
	four := []string{"H", "G", "D", "F", "N"}
	five := []string{"N", "F", "Q"}
	six := []string{"D", "Q", "V", "Z", "F", "B", "T"}
	seven := []string{"Q", "M", "T", "Z", "D", "V", "S", "H"}
	eight := []string{"M", "G", "F", "P", "N", "Q"}
	nine := []string{"B", "W", "R", "M"}

	reverse(one)
	reverse(two)
	reverse(three)
	reverse(four)
	reverse(five)
	reverse(six)
	reverse(seven)
	reverse(eight)
	reverse(nine)

	stacks := map[int][]string{
		1: one,
		2: two,
		3: three,
		4: four,
		5: five,
		6: six,
		7: seven,
		8: eight,
		9: nine,
	}
	return stacks
}

func GetLines(path string) []string {
	file, err := os.Open(path)
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()
	scanner := bufio.NewScanner(file)
	var lines []string
	for scanner.Scan() {
		lines = append(lines, scanner.Text())

	}
	return lines
}

func reverse(ss []string) {
	last := len(ss) - 1
	for i := 0; i < len(ss)/2; i++ {
		ss[i], ss[last-i] = ss[last-i], ss[i]
	}
}