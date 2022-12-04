package main

import (
	"bufio"
	"flag"
	"fmt"
	"log"
	"os"
)

const (
	a = iota
	b
	c
	d
	e
	f
	g
	h
	i
	j
	k
	l
	m
	n
	o
	p
	q
	r
	s
	t
	u
	v
	w
	x
	y
	z
	A
	B
	C
	D
	E
	F
	G
	H
	I
	J
	K
	L
	M
	N
	O
	P
	Q
	R
	S
	T
	U
	V
	W
	X
	Y
	Z
)

var fieldMap = map[string]int{
	"a": a,
	"b": b,
	"c": c,
	"d": d,
	"e": e,
	"f": f,
	"g": g,
	"h": h,
	"i": i,
	"j": j,
	"k": k,
	"l": l,
	"m": m,
	"n": n,
	"o": o,
	"p": p,
	"q": q,
	"r": r,
	"s": s,
	"t": t,
	"u": u,
	"v": v,
	"w": w,
	"x": x,
	"y": y,
	"z": z,
	"A": A,
	"B": B,
	"C": C,
	"D": D,
	"E": E,
	"F": F,
	"G": G,
	"H": H,
	"I": I,
	"J": J,
	"K": K,
	"L": L,
	"M": M,
	"N": N,
	"O": O,
	"P": P,
	"Q": Q,
	"R": R,
	"S": S,
	"T": T,
	"U": U,
	"V": V,
	"W": W,
	"X": X,
	"Y": Y,
	"Z": Z,
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
	total := 0
	total2 := 0
	count := 0
	var group = []string{}
	defer readFile.Close()
	for fileScanner.Scan() {
		count++
		group = append(group, fileScanner.Text())
		length := len(fileScanner.Text()) / 2
		first := []rune(fileScanner.Text())[0:length]
		second := []rune(fileScanner.Text())[length:]
		total += 1 + fieldMap[checkForCompartments(getSingleRunes(first), getSingleRunes(second))]
		if count == 3 {
			total2 += 1 + fieldMap[checkForCompartments2(getSingleRunes([]rune(group[0])), getSingleRunes([]rune(group[1])), getSingleRunes([]rune(group[2])))]
			group = []string{}
			count = 0
		}
	}
	log.Print(total)
	log.Print(total2)
}

func getSingleRunes(s []rune) []rune {
	var singleRunes []rune
	total := make(map[rune]int)
	for _, ru := range s {
		total[ru]++
	}
	for ru, _ := range total {
		singleRunes = append(singleRunes, ru)
	}
	return singleRunes
}

func checkForCompartments(s1 []rune, s2 []rune) string {
	for _, ru := range s1 {
		for _, ru2 := range s2 {
			if ru == ru2 {
				return string(ru)
			}
		}
	}
	return ""
}

func checkForCompartments2(s1 []rune, s2 []rune, s3 []rune) string {
	for _, ru := range s1 {
		for _, ru2 := range s2 {
			for _, ru3 := range s3 {
				if ru == ru2 && ru == ru3 && ru3 == ru2 {
					return string(ru)
				}
			}
		}
	}
	return ""
}
