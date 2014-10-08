using System.Collections.Generic;
using DelacorteNumbers;
using DelacorteNumbers.Calculations;
using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class PrimesTests
    {
        [Test]
        public void PrimesAreCorrectlyIdentified([Values(2, 3, 5, 41, 619)]int prime)
        {
            prime.IsPrime().Should().BeTrue();
        }

        [Test]
        public void NonPrimesAreCorrectlyIdentified([Values(4, 9, 12, 625)]int prime)
        {
            prime.IsPrime().Should().BeFalse();
        }

        [Test]
        public void OneIsNotPrime()
        {
            1.IsPrime().Should().BeFalse();
        }

        [Test,
        TestCase(1, 2),
        TestCase(2, 3),
        TestCase(3, 5),
        TestCase(20, 71),
        TestCase(114, 619),
        ]
        public void NthPrimeIsX(int n, int x)
        {
            Primes.GetNth(n).Should().Be(x);
        }

        [Test]
        public void PrimesLessThanXAreCorrect()
        {
            Primes.LessThan(8).Should().BeEquivalentTo(new[] { 2, 3, 5, 7 });
            Primes.LessThan(10).Should().BeEquivalentTo(new[] { 2, 3, 5, 7 });
            Primes.LessThan(12).Should().BeEquivalentTo(new[] { 2, 3, 5, 7, 11 });
            Primes.LessThan(100).Should().BeEquivalentTo(new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 });
        }
    }
}
