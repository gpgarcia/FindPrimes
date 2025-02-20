using BenchmarkDotNet.Running;
using System.Diagnostics;

namespace FindPrimes
{
    internal class Program
    {
        static void Main(string[] _)
        {
            bool test = true;
            bool bench = !test;
            if (test)
            {
                RunSearch<Definition>(100);
                RunCount<Definition>(1_000);

                RunSearch<Eratosthenes>(100);
                RunCount<Eratosthenes>(1_000);

                RunSearch<EulerList>(100);
                RunCount<EulerList>(1_000);
                RunCount<EulerList>(10_000);

                RunSearch<EratosOdd>(100);
                RunCount<EratosOdd>(1_000);
                RunCount<EratosOdd>(10_000);
                RunCount<EratosOdd>(10_000_000);
                RunCount<EratosOdd>(2_147_483_591);
                RunCount<EratosOdd>(int.MaxValue);

                RunSearch<SegmentedSieve>(100);
                RunCount<SegmentedSieve>(1_000);
                RunCount<SegmentedSieve>(10_000);
                RunCount<SegmentedSieve>(10_000_000);

            }
            if (bench)
            {
                var __ = BenchmarkRunner.Run<Benchmark>(Benchmark.CreateConfig());
            }
        }

        private static void RunSearch<TSearch>(int n) where TSearch : IPrime, new()
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

        private static void RunCount<TSearch>(int n) where TSearch : IPrime, new()
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
