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
        static void Main(string[] args)
        {
            var x = new Stopwatch();
            x.Start();
            new ExhaustiveSearch(3, 180, 125).Run();
            x.Stop();
            Console.WriteLine("Completed");
            Console.WriteLine(x.ElapsedMilliseconds);
            Thread.Sleep(10000);
        }
    }
}
