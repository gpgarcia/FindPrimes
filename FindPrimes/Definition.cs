namespace FindPrimes;

public class Definition : IPrime
{
    public Definition()
        : this(10)
    { }

    public Definition(int n)
    {
        N = n;
    }

    public int N { get; init; }

    public void Initialize()
    { }

    public bool IsPrime(int n)
    {
        if (n == 0) return false;
        if (n == 1) return false;
        for (int i = 2; i <= n / 2; ++i)
        {
            if (n % i == 0)
            {
                return false;
            }
        }
        return true;
    }
}
