using System;
using DelacorteNumbers;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GridCalculationTests
    {
        [Test]
        public void ZimmermansGridValueIsCalculatedCorrectlyFromArray()
        {
            var arrayInput = new int[3, 2] { { 6, 4 }, { 5, 1 }, { 2, 3 } };
            var grid = new DelacorteGridEvaluator(arrayInput);
            grid.Evaluate().Should().Be(53);
        }

        [Test]
        public void ZimmermansGridValueIsCalculatedCorrectlyFromGridObject()
        {
            var arrayInput = new int[3, 2] { { 6, 4 }, { 5, 1 }, { 2, 3 } };
            var grid = new DelacorteGridEvaluator(new DelacorteGrid(arrayInput));
            grid.Evaluate().Should().Be(53);
        }

        [Test,
         TestCase(1, 2, 1),
         TestCase(3, 12, 3),
         TestCase(12, 15, 3),
         TestCase(152, 136, 8),
         TestCase(378, 588, 42)]
        public void GCDCalculatorShouldWork(int a, int b, int gcd)
        {
            GCD.Of(a, b).Should().Be(gcd);
            GCD.Of(b, a).Should().Be(gcd);
        }

        [Test,
         TestCase(0, 1),
         TestCase(3, 12),
         TestCase(12, 15),
         TestCase(18, 25),
         TestCase(0, 25)]
        public void SquareDistanceCalculatorShouldWorkForOffsets(int a, int b)
        {
            Func<int, int, int> Computed = ((x, y) => x*x+y*y);
            SquareDistance.FromOffsets(a, b).Should().Be(Computed(a, b));
            SquareDistance.FromOffsets(b, a).Should().Be(Computed(a, b));
            SquareDistance.FromOffsets(b, a).Should().Be(Computed(b, a));
            SquareDistance.FromOffsets(a, b).Should().Be(Computed(b, a));
        }
    }
}
