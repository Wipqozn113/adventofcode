package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
)

func main() {
	file, err := os.Open("input.in")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	total1 := 0
	total2 := 0
	// optionally, resize scanner's capacity for lines over 64K, see next example
	for scanner.Scan() {
		line := scanner.Text()
		first := FindFirst(line)
		last := FindLast(line)
		total1 += (first * 10) + last
		first = FindFirstWithText(line)
		last = FindLastWithText(line)
		total2 += (first * 10) + last
	}

	fmt.Println(total1)
	fmt.Println(total2)

	if err := scanner.Err(); err != nil {
		log.Fatal(err)
	}
}
