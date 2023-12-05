package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
)

type CubeSet struct {
	Red   int
	Blue  int
	Green int
}

type Game struct {
	Id       int
	Cubes    []CubeSet
	MaxRed   int
	MaxBlue  int
	MaxGreen int
}

func parseGame(line string) (game Game) {
	game.MaxRed = 0
	game.MaxBlue = 0
	game.MaxGreen = 0
	return
}

func setMaxiumums(game Game) {
	for _, cube := range game.Cubes {
		game.MaxRed = max(cube.Red, game.MaxRed)
		game.MaxBlue = max(cube.Blue, game.MaxBlue)
		game.MaxGreen = max(cube.Green, game.MaxGreen)
	}
}

func isPossible(game Game, red int, blue int, green int) bool {
	return (game.MaxRed >= red) &&
		(game.MaxBlue >= blue) &&
		(game.MaxGreen >= green)
}

func gamePower(game Game) int {
	return game.MaxBlue * game.MaxRed * game.MaxGreen
}

func main() {
	file, err := os.Open("test.in")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	total := 0
	totalpower := 0

	for scanner.Scan() {
		line := scanner.Text()
		game := parseGame(line)
		setMaxiumums(game)
		if isPossible(game, 12, 14, 13) {
			total += game.Id
		}
		totalpower += gamePower(game)
	}

	fmt.Println(total)
	fmt.Println(totalpower)
}
