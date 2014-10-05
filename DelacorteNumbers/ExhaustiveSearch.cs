using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoreLinq;

namespace DelacorteNumbers
{
    public class ExhaustiveSearch
    {
        private readonly int N;
        private DelacorteGrid bestGrid;
        private DelacorteGrid worstGrid;
        private int bestScore;
        private int worstScore;


        public ExhaustiveSearch(int n) : this (n, int.MinValue, int.MaxValue)
        {}

        public ExhaustiveSearch(int n, int upperTarget, int lowerTarget)
        {
            N = n;

            bestGrid = null;
            worstGrid = null;
            bestScore = upperTarget;
            worstScore = lowerTarget;
        }

        internal void Run()
        {
            foreach (var grid in GenerateGrids())
            {
                var result = new DelacorteGridEvaluator(grid).Evaluate();

                if (result >= bestScore)
                {
                    bestScore = result;
                    bestGrid = grid;
                    Console.WriteLine(grid + "   ==  " + result);
                }
                if (result <= worstScore)
                {
                    worstScore = result;
                    worstGrid = grid;
                    Console.WriteLine(grid + "   ==  " + result);
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Best:");
            Console.WriteLine(bestGrid + "   ==  " + bestScore);
            Console.WriteLine("Worst:");
            Console.WriteLine(worstGrid + "   ==  " + worstScore);
        }

        private IEnumerable<DelacorteGrid> GenerateGrids()
        {
            foreach (var permutation in NumberPermuter.GeneratePermutationsLists(N*N))
            {
                yield return new DelacorteGrid(ListToGrid(permutation));
            }
        }

        private int[,] ListToGrid(IEnumerable<int> permutation)
        {
            var perm = permutation.ToList();
            var multiDimOutput = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    multiDimOutput[i, j] = perm[i * N + j];
                }
            }

            return multiDimOutput;
            //var jaggedArray = permutation.Batch(N).Select(Enumerable.ToArray).ToArray();
            //return JaggedToMultiDim(N, jaggedArray);
        }

        private int[][] MultiDimToJagged(int n, int[,] multiDimInput)
        {
            var jaggedOutput = new int[n][];
            for (int i = 0; i < n; i++)
            {
                jaggedOutput[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    jaggedOutput[i][j] = multiDimInput[i,j];
                }
            }

            return jaggedOutput;
        }

        private int[,] JaggedToMultiDim(int n, int[][] jaggedInput)
        {
            var multiDimOutput = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    multiDimOutput[i, j] = jaggedInput[i][j];
                }
            }

            return multiDimOutput;
        }
    }
}
