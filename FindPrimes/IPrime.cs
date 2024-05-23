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
    }

    public static class PrimeHelper
    {
        public static int[] GetPrimes(this IPrime uut)
        {
            List<int> result = [];
            for (int i = 1; i < uut.N; ++i)
            {
                if (uut.IsPrime(i))
                {
                    result.Add(i);
                }
            }
            return result.ToArray();
        }
        public static int CountPrimes(this IPrime uut)
        {
            return uut.GetPrimes().Length;
        }
    }
}