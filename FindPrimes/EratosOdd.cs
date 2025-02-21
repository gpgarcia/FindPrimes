﻿using System.Drawing;

namespace FindPrimes;

/// <summary>
/// Eratosthenes algorithm for finding prime numbers
/// </summary>
/// <remarks> excluding all even numbers </remarks>
class EratosOdd : IPrime
{
    private Sieve _sieve = null!;

    public EratosOdd()
        : this(10)
    { }
    public EratosOdd(long n)
    {
        N = n;
    }
    public long N { get; init; }

    public void Initialize()
    {
        _sieve = new Sieve(N, true);
        _sieve[1] = false;
        var limit = (int)Math.Sqrt(N) + 1;
        for (int i = 3; i < limit; i += 2)
        {
            if (_sieve[i])
            {
                for (int j = i * i; 0 < j && j < N; j += 2 * i)
                {
                    _sieve[j] = false;
                }
            }
        }
    }
    public bool IsPrime(long n)
    {
        if (n == 2)
        {
            return true;
        }
        return _sieve[n];
    }
}

