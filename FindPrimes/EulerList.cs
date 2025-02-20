namespace FindPrimes;

/// <summary>
/// Euler algorithm for finding prime numbers. Composites are only removed once.
/// </summary>
/// <remarks>https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes#Euler's_sieve </remarks>
public class EulerList : IPrime
{
    private List<int> _primes = null!;

    public EulerList()
        : this(10)
    { }

    public EulerList(int n)
    {
        N = n;
    }

    public int N { get; init; }

    public void Initialize()
    {
        var sieve = Enumerable.Range(2, N - 1).ToList();
        var limit = (int)Math.Sqrt(N);
        _primes = [];
        while (sieve.Count > 0)
        {
            var composite = new List<int>();
            for (int i = 0; i < sieve.Count && sieve.First() <= limit; ++i)
            {
                var x = sieve.ElementAt(i) * sieve.First();
                composite.Add(x);
            }
            foreach (var item in composite)
            {
                sieve.Remove(item);
            }
            if (sieve.Count > 0)
            {
                var t = sieve.First();
                sieve.Remove(t);
                _primes.Add(t);
            }
        }
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
