namespace FindPrimes
{
    class UltimatePrimesSoEAdapter : IPrime
    {
        public long N { get; init; }

        public void Initialize()
        {
            //do noting
        }

        public bool IsPrime(long n)
        {
            return GetPrimes().Contains(n);
        }

        public IEnumerable<long> GetPrimes()
        {
            long current = 0;
            long value;
            do
            {
                value = (long)UltimatePrimesSoE.ElementAt(current++);
                yield return value;
            } while (value < N);
        }
    }
}
