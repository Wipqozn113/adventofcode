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
	total := 0
	// optionally, resize scanner's capacity for lines over 64K, see next example
	for scanner.Scan() {
		line := scanner.Text()
		first := FindFirst(line)
		last := FindLast(line)
		total += (first * 10) + last
	}

	fmt.Println(total)

	if err := scanner.Err(); err != nil {
		log.Fatal(err)
	}
}
