using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOTC2024.Day4
{
    public class Wordsearch
    {
        public Wordsearch(List<string> lines) 
        { 
            Lines = lines.ToList();
        }

        public List<string> Lines { get; set; } 

        public int CountInstancesOfWord(string word)
        {
            var total = 0;
            var y = 0;


            while(y < Lines.Count)
            { 
                var line = CreateHorizontalLine(y);
                total += CountInstancesOfWordInLine(word, line);

                for (var x = 0; x < line.Length; x++)                
                {
                    line = CreateVerticalLine(x);
                    total += CountInstancesOfWordInLine(word, line);

                    line = CreateDiagonalLine(x, y);
                    total += CountInstancesOfWordInLine(word, line);
                }

                y++;
            }

            return total;
        }

        private int CountInstancesOfWordInLine(string line, string word)
        {
            var count = 0;
            var matches = Regex.Matches(line, word, RegexOptions.IgnoreCase);
            count += matches.Count;

            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            var reversedWord = new string(charArray);
            var reversedMatches = Regex.Matches(line, reversedWord, RegexOptions.IgnoreCase);
            count += reversedMatches.Count;

            return count;
        }

        private string CreateHorizontalLine(int startingY)
        {
            return Lines[startingY];
        }

        private string CreateVerticalLine(int startingX)
        {
            var line = "";
            for(int i = 0; i < Lines.Count; i++)
            {
                line += Lines[i][startingX];
            }
            return line;
        }

        private string CreateDiagonalLine(int startingX, int startingY)
        {
            var line = "";
            var x = startingX;
            var y = startingY;
            while(true)
            {
                try
                {
                    var c = Lines[y][x];
                    line += c;
                    y++;
                    x++;
                }
                catch (Exception)
                {
                    break;
                }
            }
            return line;
        }
    }
}
