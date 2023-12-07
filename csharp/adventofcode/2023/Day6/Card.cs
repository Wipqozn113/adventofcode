using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day6
{
    public class Card
    {
        public Card(string symbol, bool jokers = false)
        {
            Symbol = symbol;
            Jokers = jokers;
            Value = ParseValue(Symbol);
        }

        public Card(char symbol, bool jokers = false)
        {
            Symbol = symbol.ToString();
            Jokers = jokers;
            Value = ParseValue(Symbol);
        }

        public string Symbol { get; set; }
        public int Value { get; set; }
        public bool Jokers { get; set; }
        public bool IsJoker
        {
            get
            {
                return Jokers && Symbol == "J";
            }
        }

        public void ResetValue()
        {
            if(IsJoker)
            {
                Value = 1;
            }
        }

        private int ParseValue(string symbol)
        {
            var mappings = new Dictionary<string, int>()
            {
                { "T", 10 },
                { "J", 11 },
                { "Q", 12 },
                { "K", 13 },
                { "A", 14 }
            };

            if(Jokers)
            {
                mappings["J"] = 1;
            }

            if(!int.TryParse(symbol, out var value))
            {
                value = mappings[symbol];
            }

            return value;
        }
    }
}
