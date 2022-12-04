package main

import (
	"fmt"
	"os"
	"strings"
)

var file *string

const (
	RockOpponent     = "A"
	PaperOpponent    = "B"
	ScissorsOpponent = "C"

	RockPlayer     = "X"
	PaperPlayer    = "Y"
	ScissorsPlayer = "Z"

	Lose = "X"
	Draw = "Y"
	Win  = "Z"
)

func main() {
	data, err := os.ReadFile("/Users/simonohlmeier/Workspace/AoC2022/SimonOhlmeier/day2/input.txt")
	if err != nil {
		panic(err)
	}
	lines := strings.Split(string(data), "\n")
	var score int
	for _, line := range lines {
		moves := strings.Split(line, " ")
		score += calculatePoints(moves[0], moves[1])
	}
	fmt.Println(score)
}

func calculateMove(opponent, result string) string {
	switch opponent {
	case RockOpponent:
		switch result {
		case Lose:
			return ScissorsPlayer
		case Draw:
			return RockPlayer
		case Win:
			return PaperPlayer
		}
	case PaperOpponent:
		switch result {
		case Lose:
			return RockPlayer
		case Draw:
			return PaperPlayer
		case Win:
			return ScissorsPlayer
		}
	case ScissorsOpponent:
		switch result {
		case Lose:
			return PaperPlayer
		case Draw:
			return ScissorsPlayer
		case Win:
			return RockPlayer
		}
	}
	return ""
}

func calculatePoints(opponent, result string) int {
	player := calculateMove(opponent, result)
	switch player {
	case RockPlayer:
		switch opponent {
		case ScissorsOpponent:
			return 1 + 6
		case PaperOpponent:
			return 1 + 0
		case RockOpponent:
			return 1 + 3
		}
	case PaperPlayer:
		switch opponent {
		case ScissorsOpponent:
			return 2 + 0
		case PaperOpponent:
			return 2 + 3
		case RockOpponent:
			return 2 + 6
		}
	case ScissorsPlayer:
		switch opponent {
		case ScissorsOpponent:
			return 3 + 3
		case PaperOpponent:
			return 3 + 6
		case RockOpponent:
			return 3 + 0
		}
	}
	return 0
}
