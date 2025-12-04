using AOTC2025.Day1;
using AOTC2025.Day2;

Console.WriteLine("Which day would you like to run? Enter EXIT to end program.\n");
string? day = Console.ReadLine();
while (day is not null && day.ToLower() != "exit")
{
    if (day == "1")
    {
        D1.Part1();
        D1.Part2();
    }

    if (day == "2")
    {
        D2.Part1();
        D2.Part2();
    }


    Console.WriteLine("Which day would you like to run?  Enter EXIT to end program.\n");
    day = Console.ReadLine();
}
