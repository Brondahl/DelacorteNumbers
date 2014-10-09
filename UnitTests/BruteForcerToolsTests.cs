using DelacorteNumbers;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class BruteForcerToolsTests
    {
        [Test]
        public void BruteForceFor3x3ShouldGiveCorrectValues()
        {
            var a = new ExhaustiveSearch(3);
            a.RunWithoutDuplicates();
            a.BestScore.Should().Be(180);
            a.WorstScore.Should().Be(126);
        }

    }
}
