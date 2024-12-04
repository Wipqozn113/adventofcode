using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using AOCUtils.MathUtils;

namespace AOTC2024.Day4
{
    public class Wordsearch
    {
        public Wordsearch(List<string> lines) 
        { 
            Lines = lines.ToList();
        }

        public List<string> Lines { get; set; }

        public int CountInstancesOfXMas()
        {
            var total = 0;
            for (var y = 0; y < Lines.Count; y++)
            {
                for (var x = 0; x < Lines[y].Length; x++)
                {
                    if (Lines[y][x] == 'A'
                        && y > 0 && y < Lines.Count - 1
                        && x > 0 && x < Lines[y].Length - 1)
                    {
                        if (Lines[y - 1][x - 1] == 'M' || Lines[y - 1][x - 1] == 'S')
                        {
                            var other = Lines[y - 1][x - 1] == 'M' ? 'S' : 'M';
                            if (Lines[y + 1][x + 1] == other)
                            {
                                if (Lines[y - 1][x + 1] == 'M' || Lines[y - 1][x + 1] == 'S')
                                {
                                    other = Lines[y - 1][x + 1] == 'M' ? 'S' : 'M';
                                    if (Lines[y + 1][x - 1] == other)
                                    {
                                        total += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return total;
        }

        public int CountInstancesOfWord(string word)
        {
            var total = 0;

            for (var y = 0; y < Lines.Count; y++)
            {
                var line = CreateHorizontalLine(y);
                total += CountInstancesOfWordInLine(word, line);
            }

            for (var x = 0; x < Lines[0].Length; x++)
            {
                var line = CreateVerticalLine(x);
                total += CountInstancesOfWordInLine(word, line);
            }

            for (var y = 0; y < Lines.Count; y++)
            {
                if (y > 0)
                {
                    var line = CreateDiagonalLine(0, y);
                    total += CountInstancesOfWordInLine(word, line);
                }
                else
                {
                    for (var x = 0; x < Lines.Count; x++)
                    {
                        var line = CreateDiagonalLine(x, y);
                        total += CountInstancesOfWordInLine(word, line);
                    }
                } 
            }
            for (var y = 0; y < Lines.Count; y++)
            {
                if (y > 0)
                {
                    var line = CreateDiagonalLine(Lines.Count - 1, y, true);
                    total += CountInstancesOfWordInLine(word, line);
                }
                else
                {
                    for (var x = 0; x < Lines.Count; x++)
                    {
                        var line = CreateDiagonalLine(x, y, true);
                        total += CountInstancesOfWordInLine(word, line);
                    }
                }
            }

            return total;
        } 

        private int CountInstancesOfWordInLine(string word, string line)
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

        private string CreateDiagonalLine(int startingX, int startingY, bool reverse = false)
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
                    x = reverse ? x - 1 : x + 1;
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
