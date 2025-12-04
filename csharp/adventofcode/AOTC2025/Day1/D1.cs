namespace AOTC2025.Day1
{
    public class D1
    {
        public static void Part1()
        {
            var instructions = PopulateFromFile("input.txt");
            var dial = new Dial(50);
            foreach(var instruction in instructions)
            {
                dial.Rotate(instruction.Direction, instruction.Distance);
            }
            Console.WriteLine(dial.PointsAtZeroCount);
        }

        public static void Part2() 
        {
            var instructions = PopulateFromFile("input.txt");
            var dial = new Dial(50);      
            foreach (var instruction in instructions)
            {
                dial.RotateAlt(instruction.Direction, instruction.Distance);
            }
            Console.WriteLine(dial.PointsAtZeroCount);
        }

        private static List<Instruction> PopulateFromFile(string filename)
        {
            string path = $"{Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName}\\Day1\\{filename}";
            var lines = File.ReadLines(path);
            var instructions = new List<Instruction>(); 
            foreach (var line in lines)
            {
                var instruction = new Instruction();
                instruction.Direction = line[0] == 'L' ? Direction.Left : Direction.Right;
                instruction.Distance = int.Parse(line.Substring(1));
                instructions.Add(instruction);
            }

            return instructions;
        }

        private struct Instruction
        {
            public Direction Direction;
            public int Distance;
        }
    }
}
