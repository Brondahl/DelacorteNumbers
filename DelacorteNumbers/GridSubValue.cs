using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelacorteNumbers
{
    public class GridSubValue
    {
        public GridPoint PointA;
        public GridPoint PointB;

        public GridSubValue(GridPoint a, GridPoint b)
        {
            PointA = a;
            PointB = b;
        }

        public int GCDValue
        {
            get { return GCD.Of(PointA.Number, PointB.Number); }
        }

        public int DistanceValue
        {
            get { return SquareDistance.FromPoints(PointA, PointB); }
        }

        public int ProductValue
        {
            get { return GCDValue*DistanceValue; }
        }

        public override string ToString()
        {
            return "| " + String.Format("{0,3:###}", PointA.Number) + " , " + String.Format("{0,3:###}", PointB.Number)
                + " | " + String.Format("{0,3:###}", GCDValue)
                + " | " + String.Format("{0,4:####}", DistanceValue)
                + " | " + String.Format("{0,7:#####}", ProductValue) + " |";
        }

        public static string TableHeaderString()
        {
            return "|   A ,   B | GCD | Dist | Product |";
        }
    }
}
