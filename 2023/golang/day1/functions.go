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
