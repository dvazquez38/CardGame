using System.Collections.Generic;

namespace CardGame
{
    public static class Knapsack
    {
        private static List<List<int>> groups;

        public static List<List<int>> GetExactKnapsack(int goal, int currentSum, List<int> included, List<int> notIncluded, int startIndex)
        {
            groups = new List<List<int>>();
            ExactKnapsack(goal, currentSum, included, notIncluded, startIndex);
            return groups;
        }

        private static void ExactKnapsack(int goal, int currentSum, List<int> included, List<int> notIncluded, int startIndex)
        {
            for (int index = startIndex; index < notIncluded.Count; index++)
            {
                int nextValue = notIncluded[index];
                if (currentSum + nextValue == goal)
                {
                    groups.Add(new List<int>(included)
                    {
                        nextValue
                    });
                }
                else if (currentSum + nextValue < goal)
                {
                    List<int> nextIncluded = new List<int>(included);
                    nextIncluded.Add(nextValue);
                    List<int> nextNotIncluded = new List<int>(notIncluded);
                    nextNotIncluded.Remove(nextValue);

                    ExactKnapsack(goal, currentSum + nextValue, nextIncluded, nextNotIncluded, startIndex++);
                }
            }
        }
    }
}
