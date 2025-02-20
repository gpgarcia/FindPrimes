namespace FindPrimes;


class SegmentedSieve : IPrime
{
    const int CacheSize = 12 * 1024 * 1024;
    private List<int> _primes = [2];

    public SegmentedSieve()
        : this(10)
    { }

    public SegmentedSieve(int n)
    {
        N = n;
    }

    public int N { get; init; }

    public void Initialize()
    {

        if (N < CacheSize)
        {
            SimpleSieve(0, 3, N);
            return;
        }
        // Big N enought to justify segmented sieve.
        int limit = (int)Math.Floor(Math.Sqrt(N));
        //make limit even
        if ((limit & 1) == 1)   // if limit is odd
        {
            limit += 1;           // make it even
        }
        SimpleSieve(0, 3, limit);

        // Divide the range [0..N) into different segments of size limit 
        int low = limit;
        int high = 2 * limit;

        // process one segment at a time
        while (low < N)
        {
            var sieve = new Sieve(limit, true);
            high = Math.Min(high, N);
            // Use the previously found primes
            for (int i = 1; i < _primes.Count; ++i)  // skip 2
            {
                var prime = _primes[i];
                int loLim = AdjustLowLimit(low, prime);
                // mark all composites that are multiple of prime
                for (int j = loLim; j < high; j += 2 * prime)
                {
                    sieve[j - low] = false;
                }
            }
            StorePrimes(low, low + 1, high, sieve);
            // Update low and high for next segment
            low += limit;
            high += limit;
        }
    }


    /// <summary>
    /// finds all primes in range [low..high)
    /// </summary>
    /// <param name="low">lowest value of range inclusive</param>
    /// <param name="high">hightest value of range exclusive</param>
    private void SimpleSieve(int low, int start, int high)
    {
        var sieve = new Sieve(high - low, true);
        var limit = (int)Math.Floor(Math.Sqrt(high)) + 1;
        for (int i = start; i < limit; ++i)
        {
            if (sieve[i - low]) // if i is prime
            {
                for (int j = i * i; j < high; j += 2 * i)
                {
                    sieve[j - low] = false;
                }
            }
        }
        StorePrimes(low, start, high, sieve);
    }



    /// <summary>
    /// Store primes in the sieve from range low to high
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <param name="sieve">the sieve that marks primes. low is the zeroth element of sieve</param>
    private void StorePrimes(int low, int start, int high, Sieve sieve)
    {
        //assume low < start < high
        //assume low is even, start is odd, high is even
        for (int p = start; p < high; p += 2)
        {
            if (sieve[p - low] == true)
            {
                _primes.Add(p);
            }
        }
    }

    /// <summary>
    /// given an interval starting from low find value that this is a multiple
    /// of prime
    /// </summary>
    /// <param name="low">lowest value of interval</param>
    /// <param name="prime">a prime number </param>
    /// <returns> low/prime * prime that is >= low </returns>
    /// <example> AdjustLowLimit(31,3) returns 33 </example>    
    private static int AdjustLowLimit(int low, int prime)
    {
        int loLim = low - (low % prime);
        if (loLim < low)
        {
            loLim += prime;
        }
        if ((loLim & 1) == 0) // if loLim is even
        {
            loLim += prime;  // make it odd 
        }
        return loLim;
    }

    public bool IsPrime(int n)
    {
        return _primes.Contains(n);
    }

    public IEnumerable<int> GetPrimes()
    {
        return _primes;
    }

}


