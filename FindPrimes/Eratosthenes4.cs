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
/// The next major issue is that one is testing all numbers when we know that
/// even number will never be prime. So adjust the loops to skip even numbers.
/// 
/// The next issue is that the array size is larger than the maximum array size.
/// Since we are skipping even numbers, just store the odd numbers. Adjust the
/// access to the array to divide by 2.
/// 
/// https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes#Pseudocode 
/// 
/// TODO: https://stackoverflow.com/questions/1569393/c-how-to-make-sieve-of-atkin-incremental/20559687#20559687 
/// </remarks>
class Eratosthenes4 : IPrime
{
    private bool[] _sieve = null!;

    public Eratosthenes4()
        : this(10)
    { }
    public Eratosthenes4(long n)
    {
        N = n;
    }
    public long N { get; init; }

    public void Initialize()
    {
        _sieve = new bool[N / 2];
        Array.Fill(_sieve, true);

        long limit = (long)Math.Sqrt(N);
        for (long i = 3; i <= limit; i += 2)  //odd numbers only
        {
            if (_sieve[i / 2])
            {
                for (long j = i * i; 0 < j && j < N; j += 2 * i)
                {
                    _sieve[j / 2] = false;
                }
            }
        }
    }

    public bool IsPrime(long n)
    {
        if (n >= N)
        {
            throw new ArgumentOutOfRangeException(nameof(n), $"n={n} has to be less than N={N}");
        }
        if (n == 1)
        {
            return false;
        }
        if (n == 2)
        {
            return true;
        }
        if ((n & 1) == 0)
        {
            return false;
        }
        return _sieve[n / 2];
    }
}
