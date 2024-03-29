﻿using AOC2023.Day5;
using AOC2023.Day6;
using AOC2023.Day8;
using AOC2023.Day10;
using AOC2023.Day11;
using AOC2023.Day12;
using AOC2023.Day14;
using AOC2023.Day16;
using AOC2023.Day17;
using AOC2023.Day18;
using AOC2023.Day19;
using AOC2023.Day20;
using AOC2023.Day21;
using AOC2023.Day22;
using System.ComponentModel.DataAnnotations;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Which day would you like to run? Enter EXIT to end program.\n");
string? day = Console.ReadLine();
while (day is not null && day.ToLower() != "exit")
{
    if (day == "5")
    {
        D5.Part2();
    }
    else if (day == "6")
    {
        D6.Part1();
        D6.Part2();
    }
    else if (day == "8")
    {
        D8.Part1();
        D8.Part2();
    }
    else if (day == "10")
    {
        D10.Part1();
        D10.Part2();
    }
    else if (day == "11")
    {
        D11.Part1();
        D11.Part2();
    }
    else if (day == "12")
    {
        D12.Part1();
        D12.Part2();
    }
    else if (day == "14")
    {
        D14.Part1();
        D14.Part2();
    }
    else if(day == "16")
    {
        D16.Part1();
        D16.Part2();
    }
    else if (day == "17")
    {
        D17.Part1();
        D17.Part2();
    }
    else if (day == "18")
    {
        D18.Part1();
        D18.Part2();
    }
    else if (day == "19")
    {
        D19.Part1();
        D19.Part2();
    }
    else if (day == "20")
    {
        D20.Part1();
        D20.Part2();
    }
    else if (day == "21")
    {
        D21.Part1();
        D21.Part2();
    }
    else if (day == "22")
    {
        D22.Part1();
        D22.Part2();
    }
    else
    {
        Console.WriteLine("Invalid input!");
    }

    Console.WriteLine("Which day would you like to run?  Enter EXIT to end program.\n");
    day = Console.ReadLine();
}