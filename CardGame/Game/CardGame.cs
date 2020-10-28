using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    public static class CardGame
    {
        private static readonly string[] inputs = new string[5] { "1st", "2nd", "3rd", "4th", "5th" };
        private static Dictionary<int, int> cards = new Dictionary<int, int>();

        public static void PlayGame()
        {
            cards = new Dictionary<int, int>();
            int input = 0;
            while (input < inputs.Length)
            {
                var cardValue = ReadCard(input);
                if (cardValue.HasValue)
                {
                    var cardExists = cards.TryGetValue(cardValue.Value, out int amountOfCards);
                    if (amountOfCards == 4)
                    {
                        Console.WriteLine("Only four cards with the same number can be entered, please try again");
                    }
                    else
                    {
                        if (cardExists)
                        {
                            cards[cardValue.Value] += 1;
                        }
                        else
                        {
                            cards.Add(cardValue.Value, 1);
                        }

                        input++;
                    }
                }
            }

            PrintResult();
        }

        private static int? ReadCard(int input)
        {
            Console.WriteLine(string.Format("Please enter your {0} card", inputs[input]));
            var card = Console.ReadLine();
            var success = int.TryParse(card, out int cardValue);
            if (!success || cardValue < 1 || cardValue > 10)
            {
                Console.WriteLine("Invalid card, please try again");
                return null;
            }

            return cardValue;
        }

        private static void PrintResult()
        {
            Question1();
            Question2();
            Question3();

            Console.WriteLine();
            Console.WriteLine("Game over!");
        }

        private static void Question1()
        {
            int result = 0;
            foreach (var key in cards.Keys)
            {
                if (cards[key] > 1)
                {
                    result += cards[key];
                }
            }

            var listOfCards = cards.SelectMany(c => Enumerable.Repeat(c.Key, c.Value)).ToList();
            var groups = Knapsack.GetExactKnapsack(15, 0, new List<int>(), listOfCards, 0);
            result += groups.Count();

            Console.WriteLine();
            Console.WriteLine("=========== SCORE ===========");
            Console.WriteLine(string.Format("{0} {1}", result, result == 1 ? "Points" : "Point"));
            Console.WriteLine("=============================");
        }

        private static void Question2()
        {
            Console.WriteLine();
            Console.WriteLine("A set of five cards that score 0 points:");
            Console.WriteLine("1 2 8 9 10");
        }

        private static void Question3()
        {
            Console.WriteLine();
            Console.WriteLine("How many different sets of five cards are there where the sum of the values of all five cards is exactly 15?");
            List<int> deck = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                deck.AddRange(Enumerable.Repeat(i, 4));
            }
            List<List<int>> combinations = new List<List<int>>();
            foreach (IEnumerable<int> comb in Combinations.GetCombinations(deck, 5))
            {
                combinations.Add(comb.ToList());
            }
            var differentCombinations = combinations.Select(x => new HashSet<int>(x))
                   .Distinct(HashSet<int>.CreateSetComparer());
            Console.WriteLine(differentCombinations.Count(c => c.Sum() == 15));
        }
    }
}
