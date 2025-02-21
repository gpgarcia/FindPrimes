using System.Collections;

namespace FindPrimes;

/// <summary>
/// Eratosthenes algorithm for finding prime numbers
/// </summary>
/// <remarks>
/// Fix naive implementation most glaring issue.
/// The problem is that the search space is from [2..N/2). In reality we know
/// that N/2 is a potential multiple of 2 show it should have already been
/// struck out. one can try N/3 , N/4, N/5, etc.but the same logic applies.
/// Until one gets to x*x <=N. So the search space is [2..sqrt(N)].
/// 
/// The next major issue is that one is testing all numbers when we know that even number will never be prime.
/// 
/// https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes#Pseudocode 
/// 
/// </remarks>
class Eratosthenes3 : IPrime
{
    private bool[] _sieve = null!;

    public Eratosthenes3()
        : this(10)
    { }
    public Eratosthenes3(long n)
    {
        N = n;
    }
    public long N { get; init; }

    public void Initialize()
    {
        _sieve = new bool[N];
        Array.Fill(_sieve, true, 2, (int)(N-2));
        for (long i = 4; i < N; i += 2)
        {
            _sieve[i] = false;
        }

        int limit = (int)Math.Sqrt(N);
        for (long i = 3; i <= limit; i += 2)  //odd numbers only
        {
            if (_sieve[i])
            {
                for (long j = i * i; j < N; j += 2*i)
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
