using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day12
{
    public class SpringRow
    {
        public SpringRow(string line )
        {
            ParseLine(line);
        }

        private void ParseLine(string line)
        {
            var ln = line.Trim().Split(' ');
            Row = ln[0];
            var groups = ln[1].Split(',');
            Groups = new Queue<int>();
            foreach (var group in groups)
            {
                Groups.Enqueue(int.Parse(group));
            }
        }

        public string Row { get; set; } = "";

        public Queue<int> Groups { get; set; } = new Queue<int>();

        public long ValidCombinations { get; set; }

        public void Rollout()
        {
            var row = Row;
            Row = row + "?" + row + "?" + row + "?" + row + "?" + row;
            var groups = Groups.ToList();
            groups.AddRange(Groups);
            groups.AddRange(Groups);
            groups.AddRange(Groups);
            groups.AddRange(Groups);
            Groups = new Queue<int>(groups);
        }

        public void CalculateValidCombinations()
        {
            var groups = new Queue<int>(Groups);
            var row = Row.ToString();
            //ValidCombinations = FindValidCombinations(0, groups, row);
            ValidCombinations = FindValidCombinations(groups, row,  new StringBuilder(row));
        }

        private Dictionary<string, long> Map = new Dictionary<string, long>();

        private string CreateCacheKey(Queue<int> groups, string row)
        {
            var key = "";
            var groupCopy = new Queue<int>(groups);
            while (groupCopy.Any())
            {
                var val = groupCopy.Dequeue();
                key += val.ToString() + ",";
            }
            key += "+" + row;
  
            return key;
        }

        private void CacheResult(Queue<int> groups, string row, long result)
        {
            var key = CreateCacheKey(groups, row);
            Map[key] = result;
        }

        private long GetCacheResult(Queue<int> groups, string row)
        {
            var key = CreateCacheKey(groups, row);

            if (Map.ContainsKey(key))
                return Map[key];
            else
                return -1;
        }

        private long FindValidCombinations(int startIndex, Queue<int> groups, string row)
        {
            // Leave the original lists unalterered 
            var newGroups = new Queue<int>(groups);
            var group = newGroups.Dequeue();
            long combinations = 0;

            // Can't exist beyond this value or there wouldn't be enough space
            // left in the string for the remaining groups
            var maxLength = row.Length - newGroups.Sum();
            for(int i = startIndex; i < maxLength; i++)
            {
                if (row[i] == '?' || row[i] == '#')
                {
                    // Candidate found 
                    if((i + group - 1 < maxLength) && 
                        !row.Substring(i, group).Contains('.'))
                    {
                         if (i + group == maxLength ||
                            row[i + group] == '.' || 
                            row[i + group] == '?' &&
                            (i == 0 || (i > 0 && row[i - 1] != '#')))
                        {
                            StringBuilder newRow = new StringBuilder(row);
                            for (int j = i; j < i + group; j++)
                            {
                                newRow[j] = '#';
                            }

                            if (i + group < maxLength)
                            {
                                newRow[i + group] = '.';
                            }

                            // We need to dive further if any groups still exist
                            if (newGroups.Any())
                            {
                                combinations += FindValidCombinations(i + group + 1, newGroups, newRow.ToString());
                            }
                            // We've popualted all groups, so just increment combinations
                            else if(newRow.ToString().Count(x => x == '#') == Groups.Sum())
                            {
                                combinations += 1;
                            }
                        }
                    }                  
                }
            }
  
            return combinations;
        }

        private long FindValidCombinations(Queue<int> groups, string row, StringBuilder fullrow)
        {
            var result = GetCacheResult(groups, row);
            if(result >= 0)
                return result;

            if(row == "?###????????")
            {
                var test = "ohno";
            }

            // An empty group without '#' means we found a valid pattern
            // An empty group with '#' means we didn't found a valid pattern
            if (groups.Count == 0 && row.Contains('#'))
            {
               // Console.WriteLine(fullrow);
                return 0;
            }
            else if (groups.Count == 0)
            {
                Console.WriteLine(fullrow);
                return 1;
            }

            // There's not enough space left to fit this group along with
            // the remaining groups, so we can back out now.
            // OR if we hit an empty string with groups left, again, we have an invalid path
            var group = groups.Peek();
            var maxLength = row.Length - groups.Sum() + group;
            if (row.Length == 0 || group > maxLength || group > row.Length)
                return 0;

            // Leave the original lists unalterered 
            long combinations = 0;

            // A "." is not part of a group
            if (row[0] == '.')
            {
                combinations += FindValidCombinations(new Queue<int>(groups), row.Substring(1), fullrow);
            }

            // Might be start of a group
            else if (row[0] == '#')
            {
                // ADD A CHECK TO ENSURE WE HAVE ENOGUH SPACE FOR A GROUP
                var testString = row.Substring(0, group);

                if (!testString.Contains('.') && (group == row.Length || row[group] != '#'))
                {
                    var newGroups = new Queue<int>(groups);
                    newGroups.Dequeue();
                    var newRow = group == row.Length ? row.Substring(group) : row.Substring(group + 1);
                    combinations += FindValidCombinations(newGroups, newRow, fullrow);
                }
                else
                {
                    combinations += FindValidCombinations(new Queue<int>(groups), row.Substring(1), fullrow);
                }
            }

            // May be start of the group
            else if (row[0] == '?')
            {
                // If there's any  '.' within range of this group, then 
                // we can't fit enough '#' there to satisfy the group
                // aka this '?' is forced into a state of '.'
                var testString = row.Substring(0, group);

                // If there are enough ?/# to fit the group, then we also need to ensure
                // there's a '.' present to act as a seperator (or we're at end of the string)
                if (!testString.Contains('.') && (group == row.Length || row[group] != '#'))
                {
                    // First, let's assume there is a '#' here
                    var newGroups = new Queue<int>(groups);
                    newGroups.Dequeue();
                    var i = fullrow.Length - row.Length;
                    fullrow[i] = '#';
                    var newRow = group == row.Length ? row.Substring(group) : row.Substring(group + 1);
                    combinations += FindValidCombinations(newGroups, newRow, fullrow);
                    fullrow[i] = '.';

                    // Next, we assume there's a '.'
                    combinations += FindValidCombinations(new Queue<int>(groups), row.Substring(1), fullrow);
                    fullrow[i] = '?';
                }
                // Nothing to act as a seperator, so this must be a '.'
                else
                {
                    var i = fullrow.Length - row.Length;
                    fullrow[i] = '.';
                    combinations += FindValidCombinations(new Queue<int>(groups), row.Substring(1), fullrow);
                    fullrow[i] = '?';
                }
            }

            //CacheResult(groups, row, combinations);
            return combinations;
        }
    }
}
