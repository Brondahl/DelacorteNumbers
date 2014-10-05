using System.Collections.Generic;
using System.Text;

namespace DelacorteNumbers
{
    public class DelacorteGridEvaluator
    {
        private readonly int xMax;
        private readonly int yMax;
        private readonly int[,] array;
        private int[,] arrayInput;

        public DelacorteGridEvaluator(DelacorteGrid grid)
        {
            xMax = grid.X;
            yMax = grid.Y;
            array = grid.Array;
        }

        public DelacorteGridEvaluator(int[,] arrayInput)
        {
            xMax = arrayInput.GetLength(0);
            yMax = arrayInput.GetLength(1);
            array = arrayInput;
        }

        public int Evaluate()
        {
            int total = 0;
            var values = new List<int>();
            foreach (var gridPoint in simpleGridIterator)
            {
                foreach (var secondGridPoint in GridIteratorAfterExcluding(gridPoint))
                {
                    var value =
                        GCD.Of(gridPoint.Number, secondGridPoint.Number) *
                        SquareDistance.FromPoints(gridPoint, secondGridPoint);

                    values.Add(value);
                    total += value;
                }
            }

            return total;
        }

        private IEnumerable<GridPoint> simpleGridIterator
        {
            get
            {
                for (int i = 0; i < xMax; i++)
                {
                    for (int j = 0; j < yMax; j++)
                    {
                        yield return new GridPoint(i, j, array[i,j]);
                    }
                }
            }
        }

        private IEnumerable<GridPoint> GridIteratorAfterExcluding(GridPoint start)
        {
            var first = true;
            foreach (var point in GridIteratorAfterIncluding(start))
            {
                if (first)
                {
                    first = false;
                    continue;
                }

                yield return point;
            }
        }

        private IEnumerable<GridPoint> GridIteratorAfterIncluding(GridPoint start)
        {
            //Complete that Column
            for (int j = start.Y; j < yMax; j++)
            {
                yield return new GridPoint(start.X, j, array[start.X, j]);
            }

            //Then do the rest of the grid
            for (int i = start.X+1; i < xMax; i++)
            {
                for (int j = 0; j < yMax; j++)
                {
                    yield return new GridPoint(i, j, array[i, j]);
                }
            }
        }
    }
}
