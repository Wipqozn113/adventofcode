What is this?
=============

A solution to Day 3 of the 2024 Advent of Code

What is Advent of Code?
=======================

Advent of Code is an annual "advent calender" of programming puzzles that started in 2015. Each day from December 1 - 25
a new programming problem is posted, and the problems increase in complexity with each day (with weekends being a difficulty spike). 
Each problem is divided into two parts, although part 2 isn't accessible until you solve part 1. Advent of Code was created 
by Eric Wastl, and is hosted at https://adventofcode.com/. 

What is the Day 3 problem?
==========================

The Day 3 problem involves parsing a string for a subtring in the format: mul(X,Y), where X and Y are 1-3 digit numbers. 
For example, mul(X,Y) could be mul(234,12). It could NOT be mul(3422,234) since 3422 is more than 3 digits long. 

Each mul(X,Y) represents a simple multiplication, where mul(X,Y) = X * Y. For example, mul(10,3) = 30.

The goal is to parse a string for all occurences of mul(X,Y), perform the multiplication operation, and then calculate the sum
of all mul(X,Y) in the string.

For example, given the string:

xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))

The sum would be 161 (2*4 + 5*5 + 11*8 + 8*5). 

That's the entirity of Part 1. 

Part 2 adds an additional complication where the string can also contain the substrings "do" and "don't". 
A "do" and a "don't" enable and disable the mul(X,Y) operations respectively. The closest "do" or "don't" 
before mul(X,Y) is the one which determines if the mul(X,Y) is enabled or disabled. In the event no such 
"do" or "don't" exists, then the mul(X,Y) is considered  to be enabled (that is, they're enabled by default).

For example, given the string:

xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))

The sum would be 48 (2*4 + 8*5). The mul(5,5) and mul(11,8) are ignored since they're considered to have been disabled by a "don't".

Day 3 is viewable here: https://adventofcode.com/2024/day/3

How would I run this?
=====================

You'd need to have .NET 8 installed. You'd then run the AOTC2024 project. Once that's booted, you'll be prompted for an input, 
and you'll want to enter "3" for Day 3. 