using BenchmarkDotNet.Running;
using System.Diagnostics;

namespace FindPrimes
{
    internal class Program
    {
        static void Main(string[] _)
        {
            bool test = false;
            bool bench = !test;
            if (test)
            {
                RunCount<Definition>(1);
                RunSearch<Definition>(100);
                RunCount<Definition>(1_000);

            }
            if (bench)
            {
                var __ = BenchmarkRunner.Run<Benchmark>(Benchmark.CreateConfig());
            }
        }

        private static void RunSearch<TSearch>(long n) where TSearch : IPrime, new()
        {
            var sw = Stopwatch.StartNew();
            var uut = new TSearch { N = n };
            Console.WriteLine($"class {uut.GetType().Name}");
            uut.Initialize();
            var result = uut.GetPrimes();
            foreach (var item in result)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds} mSec");
        }

        private static void RunCount<TSearch>(long n) where TSearch : IPrime, new()
        {
            var sw = Stopwatch.StartNew();
            var uut = new TSearch { N = n };
            Console.WriteLine($"class {uut.GetType().Name}");
            uut.Initialize();
            var result = uut.CountPrimes();
            Console.WriteLine($"Count of primes < {n:N0} : {result:N0} Elapsed:{sw.Elapsed.TotalMilliseconds} mSec");
        }
    }
}
