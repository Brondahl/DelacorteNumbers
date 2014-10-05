using System.Text;

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

    }
}
