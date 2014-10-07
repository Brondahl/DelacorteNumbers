using System;
using System.Collections.Generic;
using System.Linq;

namespace DelacorteNumbers.Calculations
{
    public static class NumberPermuter
    {
        public static IEnumerable<IEnumerable<int>> GeneratePermutationsLists(int n)
        {
            var numbers = Enumerable.Range(1, n).ToArray();
            return GeneratePermutationsOfList(numbers);
        }

        internal static IEnumerable<IEnumerable<int>> GeneratePermutationsOfList(IEnumerable<int> valuesToPermute)
        {
            var valueArray = valuesToPermute.ToArray();
            return RecursivePermuteWithDepthAndArrayUnroll(valueArray, valueArray.Length);
        }

        private static IEnumerable<int[]> RecursivePermuteWithDepthAndArrayUnroll(int[] input, int depth)
        {
            if (depth == 1)
            {
                yield return input;
                yield break;
            }

            for (int i = 0; i < depth; i++)
            {
                var output = new int[depth];
                output[0] = input[i];


                // Copy input into reducedInput, excluding the i-th entry (which we just put into output) - LeftInline for performance
                var reducedInput = new int[depth - 1];
                for (int j = 0; j < i; j++)
                {
                    reducedInput[j] = input[j];
                }
                for (int j = i + 1; j < depth; j++)
                {
                    reducedInput[j - 1] = input[j];
                }


                foreach (var subPermute in RecursivePermuteWithDepthAndArrayUnroll(reducedInput, depth - 1))
                {
                    // Copy subPermute into output, after the first entry which we populated above - LeftInline for performance
                    for (int j = 0; j < depth - 1; j++)
                    {
                        output[j + 1] = subPermute[j];
                    }

                    yield return output;
                }
            }
        }

        //ncrunch: no coverage start
        [Obsolete("RecursivePermuteWithDepthAndArrayUnroll is a far more efficient Algorithm")]
        private static IEnumerable<int[]> RecursivePermuteWithDepthAndArrayCopy(int[] input, int depth)
        {
            if (depth == 1)
            {
                yield return input;
                yield break;
            }

            for (int i = 0; i < depth; i++)
            {
                var output = new int[depth];
                output[0] = input[i];

                // Copy input into reducedInput, excluding the i-th entry (which we just put into output)
                var reducedInput = new int[depth - 1];
                Array.Copy(input, 0, reducedInput, 0, i - 0);
                Array.Copy(input, i + 1, reducedInput, i, depth - (i + 1));

                foreach (var subPermute in RecursivePermuteWithDepthAndArrayCopy(reducedInput, depth - 1))
                {
                    Array.Copy(subPermute, 0, output, 1, depth - 1);
                    yield return output;
                }
            }
        }
        //ncrunch: no coverage end

        //ncrunch: no coverage start
        [Obsolete("RecursivePermuteWithDepthAndArrayUnroll is a far more efficient Algorithm")]
        private static IEnumerable<IEnumerable<int>> RecursivePermuteWithListOnly(List<int> input)
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
                var inputMinusSelected = input.Except(new List<int> { selected }).ToList();
                foreach (var subPermute in RecursivePermuteWithListOnly(inputMinusSelected))
                {
                    yield return output.Concat(subPermute);
                }
            }
        }
        //ncrunch: no coverage end

        //ncrunch: no coverage start
        [Obsolete("RecursivePermuteWithDepthAndArrayUnroll is a far more efficient Algorithm")]
        private static IEnumerable<IEnumerable<int>> RecursivePermuteWithDepth(List<int> input, int depth)
        {
            if (depth == 1)
            {
                yield return input;
                yield break;
            }

            for (int i = 0; i < depth; i++)
            {
                var output = new List<int>();
                output.Add(input[i]);
                var inputCopy = input.ToList();
                inputCopy.RemoveAt(i);
                foreach (var subPermute in RecursivePermuteWithDepth(inputCopy, depth - 1))
                {
                    yield return output.Concat(subPermute);
                }
            }
        }
        //ncrunch: no coverage end
    }
}