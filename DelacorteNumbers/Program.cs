using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelacorteNumbers
{
    class Program
    {
        static void Main()
        {
            New();
        }

        private static void New()
        {
            var arrayInput = new int[3,3] {{2, 3, 4}, {9,1,5}, {8,7,6}};
            new DelacorteGrid(arrayInput);
            new DelacorteGridEvaluator(arrayInput).BreakDown();
        }

        static void ExhSearch3Limited()
        {
            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(3, 180, 125).Run();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }
        static void ExhSearchSimple(int n)
        {
            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(n).Run();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }
    }
}
