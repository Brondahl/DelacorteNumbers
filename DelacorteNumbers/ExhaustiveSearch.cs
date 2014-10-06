using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace DelacorteNumbers
{
    public class ExhaustiveSearch
    {
        private readonly GridGenerator generator;
        private DelacorteGrid bestGrid;
        private DelacorteGrid worstGrid;
        private int bestScore;
        private int worstScore;


        public ExhaustiveSearch(int n) : this (n, int.MinValue, int.MaxValue)
        {
        }

        public ExhaustiveSearch(int n, int upperTarget, int lowerTarget)
        {
            generator = new GridGenerator(n);

            bestGrid = null;
            worstGrid = null;
            bestScore = upperTarget;
            worstScore = lowerTarget;
        }

        public void RunWithDuplicates()
        {
            Run(true);
        }

        public void RunWithoutDuplicates()
        {
            Run(false);
        }

        private void Run(bool displayGridsMatchingBest)
        {
            Parallel.ForEach(generator.GenerateAllGridsFromScratch(), grid =>
            {
                var result = new DelacorteGridEvaluator(grid).Evaluate();

                if (result > bestScore || (displayGridsMatchingBest && result == bestScore))
                {
                    bestScore = result;
                    bestGrid = grid;
                    Console.WriteLine(grid + "   ==  " + result);
                }
                if (result < worstScore || (displayGridsMatchingBest && result == worstScore))
                {
                    worstScore = result;
                    worstGrid = grid;
                    Console.WriteLine(grid + "   ==  " + result);
                }
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Best:");
            Console.WriteLine(bestGrid + "   ==  " + bestScore);
            Console.WriteLine("Worst:");
            Console.WriteLine(worstGrid + "   ==  " + worstScore);
        }
    }
}
