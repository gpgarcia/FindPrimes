using System.Collections;

namespace FindPrimes;

/// <summary>
/// Eratosthenes algorithm for finding prime numbers
/// </summary>
/// <remarks>
/// Most naive implementation
/// </remarks>
class Eratosthenes1 : IPrime
{
    private bool[] _sieve = null!;

    public Eratosthenes1()
        : this(10)
    { }
    public Eratosthenes1(long n)
    {
        N = n;
    }
    public long N { get; init; }

    public void Initialize()
    {
        _sieve = new bool[N];
        Array.Fill(_sieve, true, 2, (int)(N-2));

        int limit = (int)(N/2);
        for (long i = 2; i <= limit; i += 1)
        {
            if (_sieve[i])
            {
                for (long j = i * i; j < N; j += i)
                {
                    _sieve[j] = false;
                }
            }
        }
    }

    public bool IsPrime(long n)
    {
        return _sieve[n];
    }
}
