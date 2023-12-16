﻿using System;
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
            var group = groups.Dequeue();
            //ValidCombinations = FindValidCombinations(0, groups, row);
            ValidCombinations = FindValidCombinations(groups, row, group);
        }

        private Dictionary<string, long> Map = new Dictionary<string, long>();

        private string CreateCacheKey(int startIndex, Queue<int> groups, string row)
        {
            var key = "";
            var groupCopy = new Queue<int>(groups);
            while (groupCopy.Any())
            {
                var val = groupCopy.Dequeue();
                key += val.ToString() + ",";
            }
            key += "+" + row.Substring(startIndex);

            return key;
        }

        private void CacheResult(int startIndex, Queue<int> groups, string row, long result)
        {
            var key = CreateCacheKey(startIndex, groups, row);
            Map[key] = result;
        }

        private long GetCacheResult(int startIndex, Queue<int> groups, string row)
        {
            var key = CreateCacheKey(startIndex, groups, row);

            if (Map.ContainsKey(key))
                return Map[key];
            else
                return -1;
        }

        private long FindValidCombinations(int startIndex, Queue<int> groups, string row)
        {
            var cacheResult = GetCacheResult(startIndex, groups, row);
            if(cacheResult >= 0)
                return cacheResult; 

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

            CacheResult(startIndex, groups, row, combinations);
            return combinations;
        }

        private long FindValidCombinations(Queue<int> groups, string row, int group)
        {
            if(row == "?###????????")
            {
                var test = "ohno";
            }
            // An empty group without '#' means we found a valid pattern
            // An empty group with '#' means we didn't found a valid pattern
           /* if (groups.Count == 0 && row.Contains('#'))
                return 0;
            else if (groups.Count == 0)
                return 1;
           */
            // There's not enough space left to fit this group along with
            // the remaining groups, so we can back out now.
            // OR if we hit an empty string with groups left, again, we have an invalid path
            var maxLength = row.Length - groups.Sum();
            if (row.Length == 0 || group > maxLength || group > row.Length)
                return 0;

            // Leave the original lists unalterered 
            var newGroups = new Queue<int>(groups);
            long combinations = 0;

            // A "." is not part of a group
            if (row[0] == '.')
            {
                return FindValidCombinations(newGroups, row.Substring(1), group);
            }

            // Must be start of the group
            // WRONG WRONG WRONG: If we assumed a ? was an ., then we could be in the middle of a #
            if (row[0] == '#')
            {
                // ADD A CHECK TO ENSURE WE HAVE ENOGUH SPACE FOR A GROUP
                var testString = row.Substring(0, group);

                if (!testString.Contains('.') && (group == row.Length || row[group] != '#'))
                {
                    if (newGroups.Count == 0)
                        return 1;

                    var newGroup = newGroups.Dequeue();
                    return FindValidCombinations(newGroups, row.Substring(group + 1), newGroup);
                }
                else
                {
                    return FindValidCombinations(new Queue<int>(groups), row.Substring(1), group);
                }
            }

            // May be start of the group
            if (row[0] == '?')
            {
                // If there's any  '.' within range of this group, then 
                // we can't fit enough '#' there to satisfy the group
                // aka this '?' is forced into a state of '.'
                var testString = row.Substring(0, group);

                // If there are enough ?/# to fit the group, then we also need to ensure
                // there's a '.' present to act as a seperator (or we're at end of the string)
                if (!testString.Contains('.') && (group == row.Length || row[group] != '#'))
                {
                    if (newGroups.Count == 0)
                        return 1;

                    // First, let's assume there is a '#' here
                    var newGroup = newGroups.Dequeue();
                    combinations += FindValidCombinations(newGroups, row.Substring(group + 1), newGroup);

                    // Next, we assume there's a '.'
                    combinations += FindValidCombinations(new Queue<int>(groups), row.Substring(1), group);
                }
                // Nothing to act as a seperator, so this must be a '.'
                else
                {
                    return FindValidCombinations(new Queue<int>(groups), row.Substring(1), group);
                }


            }

            return combinations;
        }
    }
}
