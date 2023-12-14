using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            ValidCombinations = FindValidCombinations(0, groups, row);
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

        private long FindValidCombinations(Queue<int> groups, string row, int group)
        {
            // Leave the original lists unalterered 
            var newGroups = new Queue<int>(groups);
            long combinations = 0;

            // A "." is not part of a group
            if (row[0] == '.')
            {
                return FindValidCombinations(newGroups, row.Substring(1), group);
            }

            // There's not enough space left to fit this group along with
            // the remaining groups, so we can back out now.
            var maxLength = row.Length - newGroups.Sum();
            if(group >= maxLength)
            {
                return 0;
            }

            // Must be start of the group
            if (row[0] == '#')
            {
                var newGroup = newGroups.Dequeue();
                return FindValidCombinations(newGroups, row.Substring(group + 1), newGroup);
            }

            // May be start of the group
            if (row[0] == '?')
            {

            }

            return combinations;
        }
    }
}
