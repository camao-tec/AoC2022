package main

import (
	"bufio"
	"fmt"
	"strconv"
	"strings"
)

func main() {
	scanner := bufio.NewScanner(strings.NewReader(input))
	top := []int{0, 0, 0}
	sum := 0
	for scanner.Scan() {
		line := scanner.Text()
		if line != "" {
			calories, _ := strconv.Atoi(scanner.Text())
			sum += calories
		} else {
			for index, val := range top {
				if sum > val {
					top[index] = sum
					break
				}
			}
			sum = 0
		}
	}
	fmt.Println(top[0])
	fmt.Println(top[1])
	fmt.Println(top[2])
	fmt.Println(top[0] + top[1] + top[2])
}
