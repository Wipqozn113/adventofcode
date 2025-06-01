What is this?
=============

A solution to Day 6 of the 2024 Advent of Code

What is Advent of Code?
=======================

Advent of Code is an annual "advent calender" of programming puzzles that started in 2015. Each day from December 1 - 25
a new programming problem is posted, and the problems increase in complexity with each day (with weekends being a difficulty spike). 
Each problem is divided into two parts, although part 2 isn't accessible until you solve part 1. Advent of Code was created 
by Eric Wastl, and is hosted at https://adventofcode.com/. 

What is the Day 6 problem?
==========================

The Day 6 problem involve simulating a guards patrol route through a location. 

The goal of Part 1 is to calculate the number of unique squares the guard visits on a given map before it leaves
the bounds of the map.

A map looks like this:

....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...

A # repsents an obstalce, meaning the guard can't enter that square. The ^ repsents the guards starting location.

On the example map provided above, the guard visits 41 unique squares, respresnted by an X below:

....#.....
....XXXXX#
....X...X.
..#.X...X.
..XXXXX#X.
..X.X.X.X.
.#XXXXXXX.
.XXXXXXX#.
#XXXXXXX..
......#X..

For Part 2 you need to simulate the guards patrol across numerous variations of provided map. For Part 2, you want to
test each variation of the map where an unblocked square is now blocked. For each of these variations, you want
to determine if the guard enters a patrol loop. The goal is to calculate the number of map variations produce a patrol
loop for the guard.

Day 6, 2024 is viewable here: https://adventofcode.com/2024/day/6


How would I run this?
=====================

You'd need to have .NET 8 installed. You'd then run the AOTC2024 project. Once that's booted, you'll be prompted for an input, 
and you'll want to enter "6" for Day 6. 