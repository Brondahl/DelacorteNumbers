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
        private readonly IEnumerable<DelacorteGrid> gridsGenerated;
        private DelacorteGrid bestGrid;
        private DelacorteGrid worstGrid;
        public int BestScore;
        public int WorstScore;


        public ExhaustiveSearch(int n) : this (n, int.MinValue, int.MaxValue)
        {
        }

        public ExhaustiveSearch(int n, int upperTarget, int lowerTarget)
        {
            gridsGenerated = new GridGenerator(n).GenerateAllGridsFromScratch();
            bestGrid = null;
            worstGrid = null;
            BestScore = upperTarget;
            WorstScore = lowerTarget;
        }

        public ExhaustiveSearch(DelacorteGrid startingGrid, int n, int upperTarget, int lowerTarget)
        {
            gridsGenerated = new GridGenerator(n).GenerateAllGridsGivenPartialGrid(startingGrid);
            bestGrid = null;
            worstGrid = null;
            BestScore = upperTarget;
            WorstScore = lowerTarget;
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
            Parallel.ForEach(gridsGenerated, grid =>
            {
                var result = new DelacorteGridEvaluator(grid).Evaluate();

                if (result > BestScore || (displayGridsMatchingBest && result == BestScore))
                {
                    BestScore = result;
                    bestGrid = grid;
                    Console.WriteLine(grid + "   ==  " + result);
                }
                if (result < WorstScore || (displayGridsMatchingBest && result == WorstScore))
                {
                    WorstScore = result;
                    worstGrid = grid;
                    Console.WriteLine(grid + "   ==  " + result);
                }
            });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Best:");
            Console.WriteLine(bestGrid + "   ==  " + BestScore);
            Console.WriteLine("Worst:");
            Console.WriteLine(worstGrid + "   ==  " + WorstScore);
        }
    }
}
