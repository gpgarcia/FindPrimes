using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindPrimes
{
    class Pritchard : IPrime
    {
        private List<long> primes = null!;

        public long N { get; init; }

        public void Initialize()
        {
            long k = 0;
            LinkedList<long> wheel = new();
            wheel.AddLast(1);
            long length = 2;
            long p = 3;
            primes = [2L];

            while (p * p <= N)
            {
                if (length < N)
                {
                    (wheel,length) = Extend(wheel, length, Math.Min(p * length, N));
                }
                DeleteMultiple(wheel, p, length);
                primes.Add(p);
                k += 1;
                p = Next(wheel, 1);
                if (length < N)
                {
                    (wheel, length) = Extend(wheel,length, N);
                }
            }
            wheel.Remove(1);
            primes.AddRange(wheel);
        }

        private static void DeleteMultiple(LinkedList<long> W, long p, long length)
        {
            long w = p;
            while (p * w <= length)
            {
                w = Next(W, w);
            }
            while (w > 1)
            {
                w = Prev(W, w);
                W.Remove(p * w);
            }
        }


        private static (LinkedList<long>, long len) Extend(LinkedList<long> W, long length, long n)
        {
            long w = 1;
            long x = length + 1;
            while(x <= n)
            {
                W.AddLast(x);
                w = Next(W,w);
                x = length+ w;
            }
            return (W, n);
        }

        private static long Next(LinkedList<long> w, long y)
        {
            return w.Find(y)?.Next?.Value ?? default;
        }

        private static long Prev(LinkedList<long> w, long y)
        {
            return w.Find(y)?.Previous?.Value ?? default;
        }


        public bool IsPrime(long n)
        {
            return primes.Contains(n);
        }

        public IEnumerable<long> GetPrimes()
        {
            return primes;
        }
    }
}
