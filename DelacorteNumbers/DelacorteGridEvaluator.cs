using System;
using System.Collections.Generic;
using System.Linq;
using DelacorteNumbers.Calculations;

namespace DelacorteNumbers
{
    public class DelacorteGridEvaluator
    {
        private readonly int xMax;
        private readonly int yMax;
        private readonly int[,] array;

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
            foreach (var gridPoint in simpleGridIterator)
            {
                foreach (var secondGridPoint in GridIteratorStrictlyAfter(gridPoint))
                {
                    total +=
                        GCD.Of(gridPoint.Number, secondGridPoint.Number) *
                        SquareDistance.FromPoints(gridPoint, secondGridPoint);
                }
            }

            return total;
        }


        public List<GridSubValue> BreakDown()
        {
            var values = new List<GridSubValue>();
            foreach (var gridPoint in simpleGridIterator)
            {
                foreach (var secondGridPoint in GridIteratorStrictlyAfter(gridPoint))
                {
                    var subValue = new GridSubValue(gridPoint, secondGridPoint);
                    values.Add(subValue);
                }
            }

            Console.WriteLine(GridSubValue.TableHeaderString());
            foreach (var val in values.OrderByDescending(val => val.ProductValue))
            {
                Console.WriteLine(val);
            }

            return values;
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

        private IEnumerable<GridPoint> GridIteratorStrictlyAfter(GridPoint start)
        {
            //Complete that Column, skipping the first value (if Y+1 = yMax, loop doesn't execute)
            for (int j = start.Y+1; j < yMax; j++)
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
