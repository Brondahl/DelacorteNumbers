using System.Collections.Generic;
using System.Linq;
using DelacorteNumbers.Calculations;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class NumerPerumterWithRepeatsTests
    {
        private int[] EmptySingleArray = new int[0];
        private int[,] EmptyDoubleArray = new int[0, 0];
        private int[] ArrayRangeToN(int N) { return Enumerable.Range(1, N).ToArray(); }

        private static void TestOutputsMatch(IEnumerable<int[]> output, List<List<int>> predicted)
        {
            output.Count().Should().Be(predicted.Count(), "that's the right number of permutations to return");

            IEnumerable<bool> x = output.Zip(predicted, ((o, p) => o.SequenceEqual(p)));
            if (x.All(val => val))
            {
                Assert.Pass();
            }
            else
            {
                //This test is innaccurate, but gives a much more debuggable output.
                predicted.Should().Equal(output);
            }
        }

        [Test]
        public void NumberPermuterShouldWorkForSingleNumberNoRepeats()
        {
            var output = NumberPermuterWithRepeats.RecursivePermuteWithDepthAndArrayUnroll(ArrayRangeToN(1), EmptyDoubleArray, 1);
            var predicted = new[] { new[] { 1 }.ToList() }.ToList();

            TestOutputsMatch(output, predicted);
        }

        [Test]
        public void NumberPermuterShouldWorkForTwoNumbersNoRepeats()
        {
            var output = NumberPermuterWithRepeats.RecursivePermuteWithDepthAndArrayUnroll(ArrayRangeToN(2), EmptyDoubleArray, 2);
            var predicted = new[] { new[] { 1, 2 }.ToList(), new[] { 2, 1 }.ToList() }.ToList();

            TestOutputsMatch(output, predicted);
        }

        [Test]
        public void NumberPermuterShouldWorkForThreeNumbersNoRepeats()
        {
            var output = NumberPermuterWithRepeats.RecursivePermuteWithDepthAndArrayUnroll(ArrayRangeToN(3), EmptyDoubleArray, 3);
            var predicted = new[]
            {
                new[] { 1, 2, 3 }.ToList(),
                new[] { 1, 3, 2 }.ToList(),
                new[] { 2, 1, 3 }.ToList(),
                new[] { 2, 3, 1 }.ToList(),
                new[] { 3, 1, 2 }.ToList(),
                new[] { 3, 2, 1 }.ToList()
            }.ToList();

            TestOutputsMatch(output, predicted);
        }

        [Test]
        public void NumberPermuterShouldWorkForSingleNumberRepeated()
        {
            var output = NumberPermuterWithRepeats.RecursivePermuteWithDepthAndArrayUnroll(EmptySingleArray, new[,] { { 1, 4 } }, 4);
            var predicted = new[] { new[] { 1, 1, 1, 1 }.ToList() }.ToList();

            TestOutputsMatch(output, predicted);
        }


        [Test]
        public void NumberPermuterShouldWorkForSingleNumberRepeatedWithOtherNumbers()
        {
            var output = NumberPermuterWithRepeats.RecursivePermuteWithDepthAndArrayUnroll(ArrayRangeToN(1), new[,] { { 2, 2 } }, 3);
            var predicted = new[]
            {
                new[] { 1, 2, 2 }.ToList(),
                new[] { 2, 1, 2 }.ToList(),
                new[] { 2, 2, 1 }.ToList(),
            }.ToList();

            //TestOutputsMatch(output, predicted);
        }

        [Test]
        public void NumberPermuterShouldWorkForSingleNumberRepeatedWithOtherNumbers2()
        {
            var output = NumberPermuterWithRepeats.RecursivePermuteWithDepthAndArrayUnroll(ArrayRangeToN(2), new[,] { { 2, 3 }, {3,4} }, 7);
            var predicted = new[]
            {
                new[] { 1, 2, 2 }.ToList(),
                new[] { 2, 1, 2 }.ToList(),
                new[] { 2, 2, 1 }.ToList(),
            }.ToList();

            TestOutputsMatch(output, predicted);
        }


    }
}
