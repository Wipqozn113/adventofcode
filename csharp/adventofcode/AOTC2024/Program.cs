using AOTC2024.Day1;
using AOTC2024.Day2;
using AOTC2024.Day3;
using AOTC2024.Day4;
using AOTC2024.Day5;
using AOTC2024.Day6;
using AOTC2024.Day7;
using AOTC2024.Day8;


Console.WriteLine("Which day would you like to run? Enter EXIT to end program.\n");
string? day = Console.ReadLine();
while (day is not null && day.ToLower() != "exit")
{
    if (day == "1")
    {
        D1.Part1();
        D1.Part2();
    }

    else if (day == "2")
    {
        D2.Part1();
        D2.Part2();
    }

    else if (day == "3")
    {
        D3.Part1();
        D3.Part2();
    }

    else if (day == "4")
    {
        D4.Part1();
        D4.Part2();
    }

    else if (day == "5")
    {
        D5.Part1();
        D5.Part2();
    }

    else if (day == "6")
    {
        D6.Part1();
        D6.Part2();
    }

    else if (day == "7")
    {
        D7.Part1();
        D7.Part2();
    }

    else if (day == "8")
    {
        D8.Part1();
        D8.Part2();
    }

    else
    {
        Console.WriteLine("Day not recoginzed.");
    }

    Console.WriteLine("Which day would you like to run?  Enter EXIT to end program.\n");
    day = Console.ReadLine();
}
