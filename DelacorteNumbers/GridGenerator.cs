using System.Collections.Generic;
using System.Linq;
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
            foreach (var permutation in NumberPermuter.GeneratePermutationsLists(N*N))
            {
                yield return new DelacorteGrid(ListToGridByListIndex(permutation));
            }
        }

        private int[,] ListToGridByListIndex(IEnumerable<int> permutation)
        {
            var perm = permutation.ToList();
            var multiDimOutput = new int[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    multiDimOutput[i, j] = perm[i *N + j];
                }
            }

            return multiDimOutput;
        }

        private int[,] ListToGridByLinqAndConvert(IEnumerable<int> permutation)
        {
            var jaggedArray = permutation.Batch(N).Select(Enumerable.ToArray).ToArray();
            return JaggedToMultiDim(N, jaggedArray);
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