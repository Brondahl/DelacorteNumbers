using System;
using System.Collections.Generic;
using System.Linq;
using DelacorteNumbers.Calculations;
using MoreLinq;

namespace DelacorteNumbers
{
    public class GridGenerator
    {
        private readonly int N;
        public GridGenerator(int n)
        {
            N = n;
        }

        public IEnumerable<DelacorteGrid> GenerateAllGridsFromScratch()
        {
            foreach (var permutation in SimpleNumberPermuter.GeneratePermutationsLists(N * N))
            {
                yield return new DelacorteGrid(ListToGridByTripleLoop(permutation));
            }
        }

        public IEnumerable<DelacorteGrid> GenerateAllGridsGivenPartialGrid(DelacorteGrid startingGrid)
        {
            List<int> remainingValuesToBeFilled = startingGrid.IdentifyUnusedValues();
            foreach (var permutation in SimpleNumberPermuter.GeneratePermutationsOfList(remainingValuesToBeFilled))
            {
                yield return startingGrid.FillToCreateNewGrid(permutation);
            }
        }

        private int[,] ListToGridByTripleLoop(IEnumerable<int> permutation)
        {
            var permEnum = permutation.GetEnumerator();
            var multiDimOutput = new int[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    permEnum.MoveNext();
                    multiDimOutput[i, j] = permEnum.Current;
                }
            }
            return multiDimOutput;
        }

        //ncrunch: no coverage start
        [Obsolete("ListToGridByTripleLoop is a more efficient Algorithm")]
        private int[,] ListToGridByCalculatingGridCell(IEnumerable<int> permutation)
        {
            var multiDimOutput = new int[N, N];
            int xCounter = 0;
            int yCounter = 0;
            foreach (var value in permutation)
            {
                multiDimOutput[xCounter, yCounter] = value;
                yCounter++;
                if (yCounter == N)
                {
                    yCounter = 0;
                    xCounter++;
                }
            }
            return multiDimOutput;
        }
        //ncrunch: no coverage end

        //ncrunch: no coverage start
        [Obsolete("ListToGridByTripleLoop is a more efficient Algorithm")]
        private int[,] ListToGridByCalculatingListIndex(IEnumerable<int> permutation)
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
        }
        //ncrunch: no coverage end

        //ncrunch: no coverage start
        [Obsolete("ListToGridByTripleLoop is a more efficient Algorithm")]
        private int[,] ListToGridByLinqAndConvert(IEnumerable<int> permutation)
        {
            var jaggedArray = permutation.Batch(N).Select(Enumerable.ToArray).ToArray();
            return JaggedToMultiDim(N, jaggedArray);
        }
        //ncrunch: no coverage end

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

        private int[][] MultiDimToJagged(int n, int[,] multiDimInput)
        {
            var jaggedOutput = new int[n][];
            for (int i = 0; i < n; i++)
            {
                jaggedOutput[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    jaggedOutput[i][j] = multiDimInput[i, j];
                }
            }

            return jaggedOutput;
        }

    }
}