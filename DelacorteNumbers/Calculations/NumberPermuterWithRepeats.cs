using System;
using System.Collections.Generic;
using System.Linq;

namespace DelacorteNumbers.Calculations
{
    public class NumberPermuterWithRepeats
    {
        private readonly int totalSizeOfList;
        private readonly IEnumerable<int> valuesToPermute;
        private Dictionary<int, int> repeatedElements = new Dictionary<int, int>();
        private List<int> nonRepeatedElements = new List<int>();

        public NumberPermuterWithRepeats(IEnumerable<int> valuesToPermute)
        {
            this.valuesToPermute = valuesToPermute;
            totalSizeOfList = valuesToPermute.Count();
        }

        public IEnumerable<IEnumerable<int>> GeneratePermutationsOfList()
        {
            LocateRepeatedElements();

            return GeneratePermutationsOfListGroup();
        }

        public void LocateRepeatedElements()
        {
            repeatedElements = valuesToPermute
                .GroupBy(i => i)
                .ToDictionary(group => group.Key, group => group.Count());

            //Use a ToLookup thing to reduce to single enumeration if needed for performance
            foreach (var i in repeatedElements.Keys.ToList())
            {
                if (repeatedElements[i] == 1)
                {
                    nonRepeatedElements.Add(i);
                    repeatedElements.Remove(i);
                }
            }
        }

        private IEnumerable<IEnumerable<int>> GeneratePermutationsOfListGroup()
        {
            throw new Exception();
            //RecursivePermuteWithDepthAndArrayUnroll(nonRepeatedElements.ToArray(), repeatedElements.Select(innerList => innerList.ToArray()), totalSizeOfList);
        }

        public static IEnumerable<int[]> RecursivePermuteWithDepthAndArrayUnroll(int[] nonRepeated, int[,] repeated, int depth)
        {
            //Check for the end of the recursion.
            if (depth == 1)
            {
                yield return nonRepeated;
                yield break;
            }

            int initialNumberOfDistinctRepeatedElements = repeated.GetLength(0); // this reads only the long dimension of the array, which is what we want

            //Check for a reduciton to the simple case, and then defer to the more efficient algorithm.
            if (initialNumberOfDistinctRepeatedElements == 0)
            {
                foreach (var permutation in SimpleNumberPermuter.GeneratePermutationsOfList(nonRepeated))
                {
                    yield return permutation.ToArray();
                }
                yield break;
            }


            // First try selecting from RepeatedElements.
            for (int i = 0; i < initialNumberOfDistinctRepeatedElements; i++)
            {
                //we can't initialise and populate the output here, otherwise we'd return multiple references to the same array object.
                int chosenRepeatedNumber = repeated[i,0];

                bool chosenRepeatedNumberWasLastRepeat = (repeated[i,1] == 2);

                int[,] reducedRepeatedInput = null;

                if (chosenRepeatedNumberWasLastRepeat)
                {
                    //Then copy the repeated array, but skipping that element, and add the element to the nonRepeated collection instead.
                    reducedRepeatedInput = new int[initialNumberOfDistinctRepeatedElements - 1, 2];
                    for (int j = 0; j < i; j++)
                    {
                        reducedRepeatedInput[j, 0] = repeated[j, 0];
                        reducedRepeatedInput[j, 1] = repeated[j, 1];
                    }
                    for (int j = i + 1; j < initialNumberOfDistinctRepeatedElements-1; j++)
                    {
                        reducedRepeatedInput[j, 0] = repeated[j, 0];
                        reducedRepeatedInput[j, 1] = repeated[j, 1];
                    }
                    
                    //Then add the remaining single number to a new NonRepeated collection
                    int initialNumberOfNonRepeatedElements = nonRepeated.Length;
                    var increasedNonRepeated = new int[initialNumberOfNonRepeatedElements + 1];
                    for (int j = 0; j < initialNumberOfNonRepeatedElements; j++)
                    {
                        increasedNonRepeated[j] = nonRepeated[j];
                    }
                    increasedNonRepeated[initialNumberOfNonRepeatedElements] = chosenRepeatedNumber;

                    // And ... now recurse.
                    foreach (var subPermute in RecursivePermuteWithDepthAndArrayUnroll(increasedNonRepeated, reducedRepeatedInput, depth - 1))
                    {
                        var output = new int[depth];
                        output[0] = chosenRepeatedNumber;
                        // Copy subPermute into output, after the first entry which we just added - LeftInline for performance (Array.Copy() not performant for short arrays)
                        for (int j = 0; j < depth - 1; j++)
                        {
                            output[j + 1] = subPermute[j];
                        }

                        yield return output;
                    }



                }
                else
                {
                    //Then copy the repeated array, but decrement the count on that element.
                    reducedRepeatedInput = new int[initialNumberOfDistinctRepeatedElements, 2];
                    for (int j = 0; j < initialNumberOfDistinctRepeatedElements; j++)
                    {
                        reducedRepeatedInput[j, 0] = repeated[j, 0];
                        reducedRepeatedInput[j, 1] = repeated[j, 1];
                    }

                    reducedRepeatedInput[i, 1]--;

                    // And ... now recurse.
                    foreach (var subPermute in RecursivePermuteWithDepthAndArrayUnroll(nonRepeated, reducedRepeatedInput, depth - 1))
                    {
                        var output = new int[depth];
                        output[0] = chosenRepeatedNumber;
                        // Copy subPermute into output, after the first entry which we just added - LeftInline for performance (Array.Copy() not performant for short arrays)
                        for (int j = 0; j < depth - 1; j++)
                        {
                            output[j + 1] = subPermute[j];
                        }

                        yield return output;
                    }



                }
            }

            // end of select from RepeatedElements.

            // Then try selecting from NonRepeatedElements. Exactly the same algorithm as a normal permute but it recurses back into itself so not using the other one. Maybe refactor with Func<> pointer if performance hit is small.
            int initialNumNonRepeated = nonRepeated.Length;
            for (int i = 0; i < initialNumNonRepeated; i++)
            {
                //we can't initialise and populate the output here, otherwise we'd return multiple references to the same array object.
                int firstElementOfOutput = nonRepeated[i];

                // Copy input into reducedNonRepeatedInput, excluding the i-th entry (which we just put into output) - LeftInline for performance (Array.Copy() not performant for short arrays)
                var reducedNonRepeatedInput = new int[initialNumNonRepeated - 1];
                for (int j = 0; j < i; j++)
                {
                    reducedNonRepeatedInput[j] = nonRepeated[j];
                }
                for (int j = i + 1; j < initialNumNonRepeated; j++)
                {
                    reducedNonRepeatedInput[j - 1] = nonRepeated[j];
                }


                foreach (var subPermute in RecursivePermuteWithDepthAndArrayUnroll(reducedNonRepeatedInput, repeated, depth - 1))
                {
                    var output = new int[depth];
                    output[0] = firstElementOfOutput;

                    // Copy subPermute into output, after the first entry which we just added - LeftInline for performance (Array.Copy() not performant for short arrays)
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