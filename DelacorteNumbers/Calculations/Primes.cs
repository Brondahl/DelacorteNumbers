using System.Collections.Generic;
using System.Linq;

namespace DelacorteNumbers.Calculations
{
    public static class Primes
    {
        private static bool[] NIsPrimeArray = new bool[626];
        private static int[] previousPrimeArray = new int[626];
        private static Dictionary<int, int> NthPrimeDictionary = new Dictionary<int, int>();
        private static Dictionary<int, int> primeIdDictionary = new Dictionary<int, int>();

        private static List<int> primesList = new List<int>
        {
2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79,83,89,97,101,103,107,109,113,127,131,137,139,
149,151,157,163,167,173,179,181,191,193,197,199,211,223,227,229,233,239,241,251,257,263,269,271,277,281,283,
293,307,311,313,317,331,337,347,349,353,359,367,373,379,383,389,397,401,409,419,421,431,433,439,443,449,457,
461,463,467,479,487,491,499,503,509,521,523,541,547,557,563,569,571,577,587,593,599,601,607,613,617,619,
        };

        static Primes()
        {
            var counter = 1;
            foreach (var prime in primesList)
            {
                NIsPrimeArray[prime] = true;
                NthPrimeDictionary.Add(counter, prime);
                primeIdDictionary.Add(prime, counter);
                counter++;
            }

            var latestPrime = 1;
            for (int i = 1; i < 625; i++)
            {
                if (i.IsPrime())
                {
                    latestPrime = i;
                }
                previousPrimeArray[i] = latestPrime;
            }
        }

        public static bool IsPrime(this int value)
        {
            return NIsPrimeArray[value];
        }

        /// <param name="upperLimit">Non-prime upper Limit of prime list</param>
        public static IEnumerable<int> LessThan(int upperLimit)
        {
            var lastPrime = previousPrimeArray[upperLimit];
            var howManyPrimes = primeIdDictionary[lastPrime];
            return primesList.Take(howManyPrimes);
        }

        public static int GetNth(int n)
        {
            return NthPrimeDictionary[n];
        }
    }
}
