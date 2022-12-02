package main

import (
	"bufio"
	"flag"
	"fmt"
	"os"
	"strings"
)

const (
	Rock = iota
	Paper
	Scissor
)

const (
	Draw = iota
	Loose
	Win
)

var input = map[string]int{
	ORock:    Rock,
	MRock:    Rock,
	OPaper:   Paper,
	MPaper:   Paper,
	OScissor: Scissor,
	MScissor: Scissor,
}

const (
	ORock    = "A"
	OPaper   = "B"
	OScissor = "C"
)
const (
	MRock    = "X"
	MPaper   = "Y"
	MScissor = "Z"
)

var compare = [...]int{
	Draw:  3,
	Loose: 0,
	Win:   6,
}

var modifyOutput = func(opponent string, result string) string {
	switch result {
	default:
		if opponent == ORock {
			return MRock
		}
		if opponent == OPaper {
			return MPaper
		}
		if opponent == OScissor {
			return MScissor
		}
		break
	case "Z":
		if opponent == ORock {
			return MPaper
		}
		if opponent == OPaper {
			return MScissor
		}
		if opponent == OScissor {
			return MRock
		}
		break
	case "X":
		if opponent == ORock {
			return MScissor
		}
		if opponent == OPaper {
			return MRock
		}
		if opponent == OScissor {
			return MPaper
		}
		break
	}
	return ""
}

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
	total1 := 0
	total2 := 0
	for fileScanner.Scan() {
		output := strings.Split(fileScanner.Text(), " ")
		total1 += compare[((input[output[0]]-input[output[1]])%3+3)%3]
		myself := modifyOutput(output[0], output[1])
		total2 += compare[((input[output[0]]-input[myself])%3+3)%3]
		switch output[1] {
		case MRock:
			total1 += 1
			break
		case MPaper:
			total1 += 2
			break
		case MScissor:
			total1 += 3
			break
		default:
			total1 += 0
		}

		switch myself {
		case MRock:
			total2 += 1
			break
		case MPaper:
			total2 += 2
			break
		case MScissor:
			total2 += 3
			break
		default:
			total2 += 0
		}
	}
	readFile.Close()
	fmt.Println(total1)
	fmt.Println(total2)
}
