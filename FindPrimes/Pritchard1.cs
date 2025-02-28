namespace FindPrimes;

/// <summary>
/// Pritchard's wheel sieve algorithm for finding prime numbers
/// </summary>
/// <remarks>
/// https://en.wikipedia.org/wiki/Sieve_of_Pritchard#Pseudocode
/// </remarks>
class Pritchard1 : IPrime
{
    private List<long> primes = null!;

    public long N { get; init; }

    public Pritchard1()
    : this(10)
    { }
    public Pritchard1(long n)
    {
        N = n;
    }
    public void Initialize()
    {
        List<long> wheel = [1];
        long length = 2;
        long p = 3;
        primes = [2L];

        while (p * p <= N)
        {
            if (length < N)
            {
                length = Extend(wheel, length, Math.Min(p * length, N));
            }
            DeleteMultiple(wheel, p, length);
            primes.Add(p);
            p = Next(wheel, 1);
            if (length < N)
            {
                length = Extend(wheel, length, N);
            }
        }
        wheel.Remove(1);
        primes.AddRange(wheel);
    }

    private static void DeleteMultiple(List<long> W, long p, long length)
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

    private static long Extend(List<long> W, long length, long n)
    {
        long w = 1;
        long x = length + 1;
        while (x <= n)
        {
            W.Add(x);
            w = Next(W, w);
            x = length + w;
        }
        return n;
    }

    private static long Next(List<long> w, long y)
    {
        return w[w.IndexOf(y) + 1];
    }

    private static long Prev(List<long> w, long y)
    {
        return w[w.IndexOf(y) - 1];
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
