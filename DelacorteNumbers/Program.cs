using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DelacorteNumbers.Calculations;

namespace DelacorteNumbers
{
    class Program
    {
        static void Main()
        {
            AssessConceptualBoundsOfLowScore();
        }


        private static void Solve4x4Max()
        {
            var template = new DelacorteGrid(4, 4, new[]
            {
                8, 0, 0, 12,
                0, 0, 0, 0,
                0, 0, 0, 0,
                4, 0, 0, 16
            });

            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(template, 4, int.MinValue, int.MinValue).RunWithDuplicates();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }

        private static void Solve4x4Min()
        {
            var template = new DelacorteGrid(4, 4, new[]
            {
                16, 6, 0, 0,
                8, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            });

            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(template, 4, int.MaxValue, int.MaxValue).RunWithDuplicates();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }

        private static void OtherScript()
        {
            var template = new DelacorteGrid(4, 4, new[]
            {
                8, 0, 0, 12,
                0, 0, 0, 14,
                7, 0, 0, 0,
                6, 4, 0, 16
            });

            var collection = new GridGenerator(4)
                .GenerateAllGridsGivenPartialGrid(template)
                .Select(grid => new DelacorteGridEvaluator(grid).Evaluate());

            Console.WriteLine(collection.Max());
            Console.WriteLine(collection.Min());
            var histogram = collection.GroupBy(i => i).OrderBy(i => i.Key).ToDictionary(group => group.Key, group => group.Count());
            foreach (var entry in histogram)
            {
                Console.WriteLine(entry.Key + "|" + entry.Value);
            }
        }

        private static void AssessConceptualBoundsOfLowScore()
        {
            var grid = new DelacorteGrid(4,4, Enumerable.Repeat(1,16));
            new DelacorteGridEvaluator(grid).BreakDown();
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

        static void FullSearchTimeBenchmark()
        {
            var x = new Stopwatch();
            x.Start();
            for (int i = 0; i < 50; i++)
            {
                new ExhaustiveSearch(3).RunWithoutDuplicates();
            }
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }

        static void PermuteTimeBenchmark()
        {
            for (int i = 0; i < 10; i++)
            {
                var x = new Stopwatch();
                x.Start();
                List<int> y = null;
                foreach (var force in NumberPermuter.GeneratePermutationsLists(10))
                {
                    y = force.ToList();
                }
                Console.WriteLine(y);
                x.Stop();
                Console.WriteLine(x.ElapsedMilliseconds);
            }
        }

        static void GridFillTimeBenchmark()
        {
            var x = new Stopwatch();
            x.Start();
            for (int i = 0; i < 50; i++)
            {
                DelacorteGrid y = null;
                foreach (var force in new GridGenerator(3).GenerateAllGridsFromScratch())
                {
                    y = force;
                }
                Console.WriteLine(y);
            }
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
        }
    }
}
