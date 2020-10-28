using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CardGame
{
    public class Combinations
    {
        private static bool NextCombination(IList<int> numbers, int size, int takenAtATime)
        {
            var finished = false;
            var changed = false;

            if (takenAtATime <= 0) return false;

            for (var i = takenAtATime - 1; !finished && !changed; i--)
            {
                if (numbers[i] < size - 1 - (takenAtATime - 1) + i)
                {
                    numbers[i]++;
                    if (i < takenAtATime - 1)
                    {
                        for (var j = i + 1; j < takenAtATime; j++)
                        {
                            numbers[j] = numbers[j - 1] + 1;
                        }
                    }
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }

        public static IEnumerable GetCombinations(IEnumerable<int> elements, int takenAtATime)
        {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (takenAtATime > size) yield break;

            var numbers = new int[takenAtATime];
            for (var i = 0; i < takenAtATime; i++)
            {
                numbers[i] = i;
            }

            do
            {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, takenAtATime));
        }
    }
}
