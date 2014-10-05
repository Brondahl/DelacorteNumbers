using System;
using System.Collections.Generic;
using System.Linq;

namespace DelacorteNumbers
{
    public static class NumberPermuter
    {
        public static IEnumerable<IEnumerable<int>> GeneratePermutationsLists(int n)
        {
            var numbers = Enumerable.Range(1, n).ToList();

            return RecursivePermute(numbers);
        }

        private static IEnumerable<IEnumerable<int>> RecursivePermute(List<int> input)
        {
            if (input.Count() == 1)
            {
                yield return input;
                yield break;
            }

            foreach (var selected in input)
            {
                var output = new List<int>();
                output.Add(selected);
                var inputMinusSelected = input.Except(new List<int> {selected}).ToList();
                foreach (var subPermute in RecursivePermute(inputMinusSelected))
                {
                    yield return output.Concat(subPermute);
                }
            }
        }
    }
}