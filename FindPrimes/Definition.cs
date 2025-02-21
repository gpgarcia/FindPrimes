namespace FindPrimes;

public class Definition : IPrime
{
    public Definition()
        : this(10)
    { }

    public Definition(long n)
    {
        N = n;
    }

    public long N { get; init; }

    public void Initialize()
    { }

    public bool IsPrime(long n)
    {
        if (n == 0) return false;
        if (n == 1) return false;
        for (long i = 2; i <= n / 2; ++i)
        {
            if (n % i == 0)
            {
                return false;
            }
        }
        return true;
    }
}
