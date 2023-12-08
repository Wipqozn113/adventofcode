using AOC2023.Day5;
using AOC2023.Day6;
using AOC2023.Day8;

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

    Console.WriteLine("Which day would you like to run?  Enter EXIT to end program.\n");
    day = Console.ReadLine();
}