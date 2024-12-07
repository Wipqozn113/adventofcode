using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AOTC2024.Day7
{
    public class Equation
    {
        public Equation(string line) 
        {
            var ln = line.Trim().Split(":");
            Sum = long.Parse(ln[0]);
            var nums = ln[1].Trim().Split(" ");
            Numbers = nums.Select(x => long.Parse(x)).ToList();
        }

        public long Sum { get; set; }

        public List<long> Numbers { get; set; } 

        public bool IsValid(bool thirdOperator = false)
        {
            return thirdOperator ? CalcThird(Numbers, 0) : Calc(Numbers, 0);
        }

        private bool Calc(List<long> numbers, long total)
        {
            var num = numbers[0];
            var newNumbers = numbers.ToList();
            newNumbers.RemoveAt(0);

            // End of the list
            if(newNumbers.Count == 0)
            {
                return (total + num == Sum) || (total * num == Sum);
            }

            // Start of list
            if(numbers.Count == Numbers.Count)
            {
                return Calc(newNumbers, num);
            }

            return Calc(newNumbers, total * num) || Calc(newNumbers, total + num);
        }

        private bool CalcThird(List<long> numbers, long total)
        {
            var num = numbers[0];
            var newNumbers = numbers.ToList();
            newNumbers.RemoveAt(0);

            // End of the list
            if (newNumbers.Count == 0)
            {
                return (total + num == Sum) || (total * num == Sum) || ConcatenationOperator(total, num) == Sum;
            }

            // Start of list
            if (numbers.Count == Numbers.Count)
            {
                return CalcThird(newNumbers, num);
            }

            return CalcThird(newNumbers, total * num)
                || CalcThird(newNumbers, total + num)
                || CalcThird(newNumbers, ConcatenationOperator(total, num));
        }

        private long ConcatenationOperator(long num1, long num2)
        {
            return long.Parse((num1.ToString() + num2.ToString()));           
        }
    }
}
