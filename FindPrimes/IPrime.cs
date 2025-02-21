namespace FindPrimes
{
    public interface IPrime
    {
        /// <summary>
        /// Will find primes less than this number
        /// </summary>
        long N { get; init; }

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
        bool IsPrime(long n);

        /// <summary> Get all prime numbers less than N </summary>
        IEnumerable<long> GetPrimes()
        {
            return PrimeHelper.GetPrimes(this);
        }

    }

    public static class PrimeHelper
    {
        public static IEnumerable<long> GetPrimes(this IPrime uut)
        {
            for (long i = 1; i < uut.N; ++i)
            {
                if (uut.IsPrime(i))
                {
                    yield return i;
                }
            }
        }
        public static long CountPrimes(this IPrime uut)
        {
            return uut.GetPrimes().Count();
        }
    }
}