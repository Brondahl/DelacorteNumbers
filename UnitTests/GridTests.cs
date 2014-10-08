using System.Collections.Generic;
using DelacorteNumbers;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class GridTests
    {
        [Test]
        public void GridsToStringIsAccurate()
        {
            var arrayInput = new int[3, 2] { { 6, 4 }, { 5, 1 }, { 2, 3 } };
            var grid = new DelacorteGrid(arrayInput);
            grid.ToString().Should().Be("( 6, 4),( 5, 1),( 2, 3);");
        }

        [Test]
        public void GridListConstructorShouldWorkOnSquareGrids()
        {
            var grid = new DelacorteGrid(3, 3, new List<int> { 2, 3, 4,     9, 0, 5,     0, 7, 0 });
            grid.Array.Should().Equal(new int[3, 3] { { 2, 3, 4 }, { 9, 0, 5 }, { 0, 7, 0 } });
        }

        [Test]
        public void GridListConstructorShouldWorkOnRectangularGrids()
        {
            var grid = new DelacorteGrid(3, 2, new List<int> { 2, 3,   4, 9,   0, 5 });
            grid.Array.Should().Equal(new int[3, 2] { { 2, 3 }, { 4, 9 }, { 0, 5 } });
        }

        [Test]
        public void GridShouldBeAbleToReportUnusedNumbers()
        {
            var arrayInput = new int[3, 3] { { 2, 3, 4 }, { 9, 0, 5 }, { 0, 7, 0 } };
            var grid = new DelacorteGrid(arrayInput);
            grid.IdentifyUnusedValues().ShouldBeEquivalentTo(new[] { 1, 6, 8 });
        }

        [Test]
        public void FilledGridShouldReportNoMissingNumbers()
        {
            var arrayInput = new int[3, 3] { { 2, 3, 4 }, { 9, DelacorteGrid.UNSPECIFIED, 5 }, { DelacorteGrid.UNSPECIFIED, 7, DelacorteGrid.UNSPECIFIED } };
            var providedPermutation = new[] { 1, 6, 8 };
            var filledGrid = new DelacorteGrid(arrayInput).FillToCreateNewGrid(providedPermutation);
            filledGrid.IdentifyUnusedValues().ShouldBeEquivalentTo(new int[] { });
        }

        [Test]
        public void GridFillShouldInsertNumbersInTheAppropriateOrder()
        {
            var arrayInput = new int[3, 3] { { 2, 3, 4 }, { 9, DelacorteGrid.UNSPECIFIED, 5 }, { DelacorteGrid.UNSPECIFIED, 7, DelacorteGrid.UNSPECIFIED } };
            var providedPermutation = new[] { 8, 1, 6 };
            var filledGrid = new DelacorteGrid(arrayInput).FillToCreateNewGrid(providedPermutation);
            filledGrid.Array.Should().Equal(new int[3, 3] { { 2, 3, 4 }, { 9, 8, 5 }, { 1, 7, 6 } });
        }
    }
}
