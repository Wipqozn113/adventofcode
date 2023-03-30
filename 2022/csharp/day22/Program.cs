using System.Collections;
using System.Diagnostics;
using System.Text;

namespace day22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }

        static void Part1(bool test = false)
        {
            var grove = Utils.CreateGrove(test);
            var password = grove.ExecuteCommands();
            Console.Write("Part 1: ");
            Console.WriteLine(password);
        }

        static void Part2(bool test = false)
        {
            var cube = CubeUtils.GetCube(test);
            var password = cube.ExecuteCommands();
            Console.Write("Part 2: ");
            Console.WriteLine(password);
        }
    }
}