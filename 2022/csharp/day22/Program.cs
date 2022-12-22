using System.Collections;
using System.Diagnostics;
using System.Text;

namespace day22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var grove = Utils.CreateGrove(false);
            var password = grove.ExecuteCommands();
            Console.WriteLine(password);
        }
    }
}