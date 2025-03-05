using BenchmarkDotNet.Running;
using Microsoft.Diagnostics.Runtime.Utilities;
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
                Console.WriteLine($"Environment.ProcessorCount={Environment.ProcessorCount}");
                Console.WriteLine($"int.MaxValue=   {int.MaxValue}");
                Console.WriteLine($"Array.MaxLength={Array.MaxLength}");
                Console.WriteLine();

                //RunCount<Definition>(1);
                //RunSearch<Definition>(100);
                //RunCount<Definition>(1_000);


                //RunSearch<Eratosthenes1>(100);
                //RunCount<Eratosthenes1>(1_000);

                //RunSearch<Eratosthenes2>(100);
                //RunCount<Eratosthenes2>(1_000);
                //RunCount<Eratosthenes2>(10_000);

                //RunSearch<Eratosthenes3>(100);
                //RunCount<Eratosthenes3>(1_000);
                //RunCount<Eratosthenes3>(10_000);
                //try
                //{
                //    RunCount<Eratosthenes3>(2_147_483_591 + 1);
                //}
                //catch (OutOfMemoryException ex)
                //{
                //    Console.WriteLine($"N={2_147_483_591 + 1}, {ex.Message}");
                //}

                RunCount<Eratosthenes4>(1);
                RunSearch<Eratosthenes4>(100);
                RunCount<Eratosthenes4>(1_000);
                RunCount<Eratosthenes4>(10_000);
                RunCount<Eratosthenes4>(100_000);
                RunCount<Eratosthenes4>(1_000_000);
                RunCount<Eratosthenes4>(10_000_000);
                RunCount<Eratosthenes4>(100_000_000);
                ////RunCount<Eratosthenes4>(2_147_483_591 + 1);
                ////RunCount<Eratosthenes4>(2L * (long)Array.MaxLength + 1L);
                try
                {
                    RunCount<Eratosthenes4>(2L * (long)Array.MaxLength + 2L);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine($"N={2L * (long)Array.MaxLength + 2L}, {ex.Message}");
                }

                RunCount<Eratosthenes5>(1);
                RunSearch<Eratosthenes5>(100);
                RunCount<Eratosthenes5>(10_000);
                RunCount<Eratosthenes5>(1_000_000);
                RunCount<Eratosthenes5>(100_000_000);


                RunCountUltimatePrimesSoE(1);
                RunSearchUltimatePrimesSoE(100);
                RunCountUltimatePrimesSoE(1_000);
                RunCountUltimatePrimesSoE(10_000);
                RunCountUltimatePrimesSoE(100_000);
                RunCountUltimatePrimesSoE(10_000_000);
                RunCountUltimatePrimesSoE(2_147_483_591 + 1);
                RunCountUltimatePrimesSoE(2L * (long)Array.MaxLength + 1L);

                RunCount<UltimatePrimesSoEAdapter>(1);
                RunCount<UltimatePrimesSoEAdapter>(100);
                RunCount<UltimatePrimesSoEAdapter>(1_000);
                //RunCount<UltimatePrimesSoEAdapter>(100_000);


                ////RunCount<Pritchard1>(1);
                //RunSearch<Pritchard1>(100);
                //RunCount<Pritchard1>(1_000);
                //RunCount<Pritchard1>(10_000);
                //RunCount<Pritchard1>(100_000);

                //RunCount<Pritchard2>(1);
                //RunSearch<Pritchard2>(100);
                //RunCount<Pritchard2>(1_000);
                //RunCount<Pritchard2>(10_000);
                //RunCount<Pritchard2>(100_000);
                //RunCount<Pritchard2>(10_000_000);
                ////RunCount<Pritchard2>(2L * (long)Array.MaxLength + 1L); /// > 4 hours :-(

                //RunSearch<EulerList>(100);
                //RunCount<EulerList>(1_000);
                //RunCount<EulerList>(10_000);

                //RunSearch<EratosOdd>(100);
                //RunCount<EratosOdd>(1_000);
                //RunCount<EratosOdd>(10_000);
                //RunCount<EratosOdd>(10_000_000);
                ////RunCount<EratosOdd>(Array.MaxLength + 1);
                ////RunCount<EratosOdd>(2L * (long)Array.MaxLength + 1L);
                ////RunCount<EratosOdd>(2L * (long)int.MaxValue + 1L);
                //try
                //{
                //    RunCount<EratosOdd>(2L * (long)int.MaxValue + 2L);
                //}
                //catch (ArgumentOutOfRangeException ex)
                //{
                //    Console.WriteLine($"N={2L * (long)int.MaxValue + 2L}, {ex.Message}");
                //}

                RunCount<SegmentedSieve>(1);
                RunSearch<SegmentedSieve>(100);
                RunCount<SegmentedSieve>(10_000);
                RunCount<SegmentedSieve>(1_000_000);
                RunCount<SegmentedSieve>(10_000_000);
                //RunCount<SegmentedSieve>(2L * (long)int.MaxValue + 1L);

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


        private static void RunCountUltimatePrimesSoE(long n)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"class {nameof(UltimatePrimesSoE)}");
            var result = UltimatePrimesSoE.CountTo((ulong)n);
            Console.WriteLine($"Count of primes < {n:N0} : {result:N0} Elapsed:{sw.Elapsed.TotalMilliseconds} mSec");
        }

        private static void RunSearchUltimatePrimesSoE(long n)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine($"class {nameof(UltimatePrimesSoE)}");
            foreach (var i in Enumerable.Range(0, 25))
            {
                Console.Write($"{UltimatePrimesSoE.ElementAt(i)} ");
            }
            Console.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds} mSec");
        }
    }
}
