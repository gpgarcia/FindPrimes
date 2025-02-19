namespace FindPrimes
{
    public interface IPrime
    {
        /// <summary>
        /// Will find primes less than this number
        /// </summary>
        int N { get; init; }

        /// <summary>
        /// Run one time initialization
        /// </summary>
        void Initialize();

        /// <summary>
        /// Determine if the number n is prime. N must be less than N.
        /// Prime number have exactly 2 factors 1 and itself.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        bool IsPrime(int n);

        /// <summary> Get all prime numbers less than N </summary>
        IEnumerable<int> GetPrimes()
        {
            return PrimeHelper.GetPrimes(this);
        }

    }

    public static class PrimeHelper
    {
        public static IEnumerable<int> GetPrimes(this IPrime uut)
        {
            for (int i = 1; i < uut.N; ++i)
            {
                if (uut.IsPrime(i))
                {
                    yield return i;
                }
            }
        }
        public static int CountPrimes(this IPrime uut)
        {
            return uut.GetPrimes().Count();
        }
    }
}