using AOC2023.Day5;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Which day would you like to run? Enter EXIT to end program.\n");
string? day = Console.ReadLine();
while (day is not null && day.ToLower() != "exit")
{ 
    if (day == "5")
    {
        D5.Part2();
    }

    Console.WriteLine("Which day would you like to run? Choose no option to exit.\n");
    day = Console.ReadLine();
}