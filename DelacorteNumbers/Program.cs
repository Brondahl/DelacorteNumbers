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
            FillAllPermutesTest();
        }

        private static void FillTest()
        {
            var arrayInput = new int[3, 3] { { 2, 3, 4 }, { 9, 0, 5 }, { 8, 7, 0 } };
            var x = new DelacorteGrid(arrayInput);
            var y = x.FillToCreateNewGrid(new[] {6,1});
            Console.WriteLine(y.ToString());
        }

        private static void FillAllPermutesTest()
        {
            var arrayInput = new int[3, 3] { { 2, 3, 4 }, { 9, 0, 5 }, { 0, 7, 0 } };
            var x = new DelacorteGrid(arrayInput);
            var y = x.FillToCreateNewGrid(new[] { 6, 1, 8 });
            foreach (var grid in new GridGenerator(3).GenerateAllGridsGivenPartialGrid(x))
            {
                Console.WriteLine(grid.ToString());
            }
        }

        private static void BreakdownOfWinning3Grid()
        {
            var arrayInput = new int[3, 3] { { 2, 3, 4 }, { 9, 1, 5 }, { 8, 7, 6 } };
            new DelacorteGrid(arrayInput);
            new DelacorteGridEvaluator(arrayInput).BreakDown();
        }

        static void ExhSearch3Limited()
        {
            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(3, 180, 125).RunWithDuplicates();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }

        static void ExhSearchSimple(int n)
        {
            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(n).RunWithoutDuplicates();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }

        static void TimeBenchmark()
        {
            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(3).RunWithoutDuplicates();
            new ExhaustiveSearch(3).RunWithoutDuplicates();
            new ExhaustiveSearch(3).RunWithoutDuplicates();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }

        static void OtherTimeBenchmark()
        {
            var x = new Stopwatch();
            x.Start();
            NumberPermuter.GeneratePermutationsLists(10).ToList();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }
    }
}
