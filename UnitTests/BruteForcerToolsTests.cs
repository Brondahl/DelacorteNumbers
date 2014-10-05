using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DelacorteNumbers;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    class BruteForcerToolsTests
    {
        [Test]
        public void NumberPermuterShouldWorkForSingleNumber()
        {
            var output = NumberPermuter.GeneratePermutationsLists(1);
            var predicted = new[] { new[] { 1 }.ToList() }.ToList();

            output.Count().Should().Be(predicted.Count());

            IEnumerable<bool> x = output.Zip(predicted, ((o, p) => o.SequenceEqual(p)));
            if (x.All(val => val))
            {
                Assert.Pass();

            }
            else
            {
                predicted.Should().Equal(output);
            }
        }

        [Test]
        public void NumberPermuterShouldWorkForTwoNumbers()
        {
            var output = NumberPermuter.GeneratePermutationsLists(2);
            var predicted = new[] { new[] { 1, 2 }.ToList(), new[] { 2, 1 }.ToList() }.ToList();

            output.Count().Should().Be(predicted.Count());

            IEnumerable<bool> x = output.Zip(predicted, ((o, p) => o.SequenceEqual(p)));
            if (x.All(val => val))
            {
                Assert.Pass();

            }
            else
            {
                predicted.Should().Equal(output);
            }
        }

        [Test]
        public void NumberPermuterShouldWorkForThreeNumbers()
        {
            var output = NumberPermuter.GeneratePermutationsLists(3);
            var predicted = new[]
            {
                new[] { 1, 2, 3 }.ToList(),
                new[] { 1, 3, 2 }.ToList(),
                new[] { 2, 1, 3 }.ToList(),
                new[] { 2, 3, 1 }.ToList(),
                new[] { 3, 1, 2 }.ToList(),
                new[] { 3, 2, 1 }.ToList()
            }.ToList();

            output.Count().Should().Be(predicted.Count());

            IEnumerable<bool> x = output.Zip(predicted, ((o, p) => o.SequenceEqual(p)));
            if (x.All(val => val))
            {
                Assert.Pass();

            }
            else
            {
                predicted.Should().Equal(output);
            }
        }

    }
}
