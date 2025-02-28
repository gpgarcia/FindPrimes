namespace FindPrimes
{
    /// <summary>
    /// Pritchard's wheel sieve algorithm for finding prime numbers
    /// </summary>
    /// <remarks> 
    /// https://rosettacode.org/wiki/Sieve_of_Pritchard#C#
    /// </remarks>
    class Pritchard2 : IPrime
    {
        private List<long> primes = [];

        public long N { get; init; }

        public Pritchard2()
        : this(10)
        { }

        public Pritchard2(long n)
        {
            N = n;
        }

        public void Initialize()
        {
            var wheel = new SortedSet<long> { 1 };
            long stp = 1;
            long prime = 2;
            long rootLimit = 1 + (int)Math.Sqrt(N);

            while (prime < rootLimit)
            {
                long length = ExpandWheel(wheel, stp, prime);
                stp = length; // update wheel size to wheel limit
                long nextPrime = DeleteComposites(wheel, prime, length);

                primes.Add(prime);
                prime = prime == 2 ? 3 : nextPrime;
            }
            wheel.Remove(1); 
            primes.AddRange(wheel);
        }

        private static long DeleteComposites(SortedSet<long> wheel, long prime, long length)
        {
            List<long> tl = [];
            long nextPrimeCandidate = 5; 
            foreach (var w in wheel)
            {
                if (nextPrimeCandidate == 5 && w > prime)
                {
                    nextPrimeCandidate = w;
                }
                var n = prime * w;
                if (n > length)
                {
                    break;
                }
                else
                {
                    tl.Add(n);
                }
            }
            foreach (var itm in tl)
            {
                wheel.Remove(itm);
            }
            return nextPrimeCandidate;
        }

        private long ExpandWheel(SortedSet<long> wheel, long stp, long prime)
        {
            List<long> tl = [];
            var length = Math.Min(prime * stp, N);
            if (stp < N)
            {
                foreach (var w in wheel)
                {
                    for (var n = w + stp; n <= length; n += stp)
                    {
                        tl.Add(n);
                    }
                }
                wheel.UnionWith(tl);
            }
            return length;
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
