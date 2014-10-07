using DelacorteNumbers;
using DelacorteNumbers.Calculations;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GridSubValueTests
    {
        [Test,
        Combinatorial]
        public void GridSubValueComputesGCD([Values(1, 2, 3, 4, 5, 6, 7, 8, 9)]int x, [Values(1, 2, 3, 4, 5, 6, 7, 8, 9)]int y)
        {
            var a = new GridPoint(1, 4, x);
            var b = new GridPoint(3, 2, y);

            new GridSubValue(a, b).GCDValue.Should().Be(GCD.Of(x, y));
        }

        [Test,
        Combinatorial]
        public void GridSubValueComputesDistance([Values(1, 2, 3, 4, 5, 6, 7, 8, 9)]int x, [Values(1, 2, 3, 4, 5, 6, 7, 8, 9)]int y)
        {
            var a = new GridPoint(2, 4, 1);
            var b = new GridPoint(x, y, 2);

            new GridSubValue(a, b).DistanceValue.Should().Be(SquareDistance.FromPoints(a, b));
        }

        [Test]
        public void GridSubValueBuildsStringCorrectly()
        {
            var a = new GridPoint(1, 4, 4);
            var b = new GridPoint(3, 2, 5);

            new GridSubValue(a, b).ToString().Should().Be("|   4 ,   5 |   1 |    8 |       8 |");
        }

        [Test]
        public void GridSubValueBuildsStringCorrectlyWithLongValues()
        {
            var a = new GridPoint(1, 4, 561);
            var b = new GridPoint(3, 2, 120);

            new GridSubValue(a, b).ToString().Should().Be("| 561 , 120 |   3 |    8 |      24 |");
        }

        [Test]
        public void GridSubValueBuildsStringCorrectlyWithLongGCD()
        {
            var a = new GridPoint(1, 4, 560);
            var b = new GridPoint(3, 2, 280);

            new GridSubValue(a, b).ToString().Should().Be("| 560 , 280 | 280 |    8 |    2240 |");
        }

        [Test]
        public void GridSubValueBuildsStringCorrectlyWithLongDistance()
        {
            var a = new GridPoint(1, 1, 561);
            var b = new GridPoint(25, 25, 120);

            new GridSubValue(a, b).ToString().Should().Be("| 561 , 120 |   3 | 1152 |    3456 |");
        }

        [Test]
        public void GridSubValueBuildsStringCorrectlyWithLongProduct()
        {
            var a = new GridPoint(1, 1, 560);
            var b = new GridPoint(25, 25, 280);

            new GridSubValue(a, b).ToString().Should().Be("| 560 , 280 | 280 | 1152 |  322560 |");
        }

        [Test]
        public void TableHeaderShouldBeCorrectWidth()
        {
            var realColumnWidth = 36;
            var newlineWidth = 2;
            var numberOfLines = 3;
            var totalLength = (realColumnWidth + newlineWidth)*numberOfLines;
            GridSubValue.TableHeaderString().Length.Should().Be(totalLength);
        }
    }
}
