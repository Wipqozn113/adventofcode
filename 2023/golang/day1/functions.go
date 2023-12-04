package main

import (
	"log"
	"strconv"
	"strings"
)

func FindFirst(line string) (firstnum int) {
	nums := [9]string{"1", "2", "3", "4", "5", "6", "7", "8", "9"}
	firstpos := 999999999999999999

	for _, num := range nums {
		if first := strings.Index(line, num); first < firstpos && first > -1 {
			firstpos = first
			fn, err := strconv.Atoi(num)
			firstnum = fn
			if err != nil {
				log.Fatal(err)
			}
		}
	}

	return
}

func FindLast(line string) (lastnum int) {
	nums := [9]string{"1", "2", "3", "4", "5", "6", "7", "8", "9"}
	lastpos := -1

	for _, num := range nums {
		if last := strings.LastIndex(line, num); last > lastpos && last > -1 {
			lastpos = last
			fn, err := strconv.Atoi(num)
			lastnum = fn
			if err != nil {
				log.Fatal(err)
			}
		}
	}

	return
}

func FindFirstWithText(line string) (firstnum int) {
	nums := [18]string{"1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"}
	firstpos := 999999999999999999

	for _, num := range nums {
		if first := strings.Index(line, num); first < firstpos && first > -1 {
			firstpos = first
			firstnum = MapTextToNumbers(num)
		}
	}

	return
}

func FindLastWithText(line string) (lastnum int) {
	nums := [18]string{"1", "2", "3", "4", "5", "6", "7", "8", "9", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"}
	lastpos := -1

	for _, num := range nums {
		if last := strings.LastIndex(line, num); last > lastpos && last > -1 {
			lastpos = last
			lastnum = MapTextToNumbers(num)
		}
	}

	return
}

func MapTextToNumbers(text string) int {
	mappings := map[string]int{
		"one":   1,
		"two":   2,
		"three": 3,
		"four":  4,
		"five":  5,
		"six":   6,
		"seven": 7,
		"eight": 8,
		"nine":  9,
		"1":     1,
		"2":     2,
		"3":     3,
		"4":     4,
		"5":     5,
		"6":     6,
		"7":     7,
		"8":     8,
		"9":     9,
	}

	return mappings[text]
}
