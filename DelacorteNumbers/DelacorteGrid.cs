using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DelacorteNumbers
{
    public class DelacorteGrid
    {
        public readonly int X;
        public readonly int Y;
        public readonly int[,] Array;

        public DelacorteGrid(int[,] inputArray)
        {
            X = inputArray.GetLength(0);
            Y = inputArray.GetLength(1);
            Array = inputArray;
        }

        public override string ToString()
        {
            var line = new StringBuilder();

            for (int i = 0; i < X; i++)
            {
                line.Append("(");
                for (int j = 0; j < Y; j++)
                {
                    line.Append(Array[i, j] + ",");
                }
                line.Append("),");
            }
            line.Append(";");

            line.Replace(",)", ")");
            line.Replace("),;", ");");

            return line.ToString();
        }


        public List<int> IdentifyUnusedValues()
        {
            var allUnusedNumbers = Enumerable.Range(1, X*Y).ToList();
            foreach (var i in Array)
            {
                allUnusedNumbers.Remove(i);
            }
            return allUnusedNumbers;
        }

        public DelacorteGrid FillToCreateNewGrid(IEnumerable<int> permutation)
        {
            var output = new DelacorteGrid((int[,]) Array.Clone());
            output.FillWith(permutation);
            return output;
        }

        private void FillWith(IEnumerable<int> permutation)
        {
            var permEnum = permutation.GetEnumerator();

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (Array[i, j] == 0)
                    {
                        permEnum.MoveNext();
                        Array[i, j] = permEnum.Current;
                    }
                }
            }
        }
    }
}
