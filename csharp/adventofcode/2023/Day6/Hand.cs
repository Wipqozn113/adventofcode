using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AOC2023.Day6
{
    public class Hand : IComparable<Hand>
    {
        public Hand(string line, bool jokers = false)
        {
            var ln = line.Trim().Split(" ");
            Jokers = jokers;
            Cards = new List<Card>();
            foreach(var c in ln[0])
            {
                Cards.Add(new Card(c, jokers));
            }
            CalculateStrength();
            Bid = int.Parse(ln[1]);

        }

        public List<Card> Cards { get; set; }

        public int Strength { get; set; }

        public int Bid { get; set; }

        public bool Jokers { get; set; }

        public List<Card> SortHand()
        {
            return Cards.OrderBy(x => x.Value).ToList();
        }

        public int CompareTo(Hand? other)
        {
            if (other is null) return -1;


            if (Strength > other.Strength)
            {
                return 1;
            }

            if (Strength < other.Strength)
            {
                return -1;
            }

            if(Strength == other.Strength)
            {
                var cards = Cards;
                var otherCards = other.Cards;
                for(int i = 0; i < cards.Count; i++)
                {
                    if (cards[i].Value > otherCards[i].Value) 
                    {
                        return 1;
                    }

                    if (cards[i].Value < otherCards[i].Value)
                    {
                        return -1;
                    }
                }
            }

            // Hands are equal
            return 0;
        }

        private void CalculateStrength()
        {
            if (Jokers && Cards.Any(x => x.IsJoker))
            {
                var highestStrength = 1;
                for(int i = 2; i <= 14; i++)
                {
                    if (i == 11)
                        continue;

                    var cards = Cards.ToList();
                    foreach(var card in cards.Where(x => x.IsJoker))
                    {
                        card.Value = i;
                    }

                    var strength = CalculateStrength(cards);
                    if(strength > highestStrength)
                        highestStrength = strength;

                    foreach (var card in cards)
                    {
                        card.ResetValue();
                    }
                }
                Strength = highestStrength;
            }
            else
            {
                Strength = CalculateStrength(Cards);
            }
        }

        private int CalculateStrength(List<Card> cards)
        {
            if(IsFive(cards))
            {
                return 7;
            }
            else if(IsFour(cards))
            {
                return 6;
            }
            else if(IsFull(cards))
            {
                return 5;
            }
            else if(IsThree(cards))
            {
                return 4;
            }
            else if(IsTwoPair(cards))
            {
                return 3;
            }
            else if(IsPair(cards))
            {
                return 2;
            }

            return 1;
        }

        private bool IsFive(List<Card> cards)
        {
            return cards.DistinctBy(x => x.Value).ToList().Count == 1;
        }

        private bool IsFour(List<Card> cards)
        {
            var distinct = cards.DistinctBy(x => x.Value).ToList();
            if (distinct.Count != 2)
                return false;

            foreach(var card in distinct)
            {
                if(cards.Where(x => x.Value == card.Value).Count() == 4)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsFull(List<Card> cards) 
        {
            var distinct = cards.DistinctBy(x => x.Value).ToList();
            if (distinct.Count != 2)
                return false;

            return (cards.Where(x => x.Value == distinct[0].Value).Count() == 2 &&
                cards.Where(x => x.Value == distinct[1].Value).Count() == 3) ||
                (cards.Where(x => x.Value == distinct[0].Value).Count() == 3 &&
                cards.Where(x => x.Value == distinct[1].Value).Count() == 2);

        }

        private bool IsThree(List<Card> cards) 
        {
            var distinct = cards.DistinctBy(x => x.Value).ToList();
            if (distinct.Count != 3)
                return false;

            foreach (var card in distinct)
            {
                if (cards.Where(x => x.Value == card.Value).Count() == 3)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsTwoPair(List<Card> cards)
        {
            var distinct = cards.DistinctBy(x => x.Value).ToList();
            if (distinct.Count != 3)
                return false;

            foreach (var card in distinct)
            {
                if (cards.Where(x => x.Value == card.Value).Count() == 3)
                {
                    return false;
                }
            }


            return true;
        }

        private bool IsPair(List<Card> cards)
        {
            return cards.DistinctBy(x => x.Value).Count() == 4;
        }


    }
}
