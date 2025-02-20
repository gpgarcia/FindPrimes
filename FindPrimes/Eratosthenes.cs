using System.Collections;

namespace FindPrimes;

/// <summary>
/// Eratosthenes algorithm for finding prime numbers
/// </summary>
/// <remarks> https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes#Pseudocode </remarks>
class Eratosthenes : IPrime
{
    private BitArray _sieve = null!;

    public Eratosthenes()
        : this(10)
    { }
    public Eratosthenes(int n)
    {
        N = n;
    }
    public int N { get; init; }

    public void Initialize()
    {
        _sieve = new BitArray(N, true);
        _sieve[0] = false;
        _sieve[1] = false;
        var limit = (int)Math.Sqrt(N) + 1;
        for (int i = 2; i <= limit; i += 1)
        {
            if (_sieve[i])
            {
                for (int j = i * i; j < N; j += i)
                {
                    _sieve[j] = false;
                }
            }
        }
    }
    public bool IsPrime(int n)
    {
        return _sieve[n];
    }
}
