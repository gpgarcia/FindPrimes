using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;

namespace FindPrimes;

/// <summary>
/// A special bitArray that only holds odd values
/// </summary>
/// <remarks>used for prime number sieves</remarks>
class Sieve : IEnumerable<bool>
{
    private BitArray _sieve;

    public int Count => 2 * _sieve.Count;


    public Sieve(long length)
        : this(length, false)
    { }

    public Sieve(long length, bool defaultValue)
    {
        if((length & 1L) == 1L)
        {
            length -= 1L; // make it even
        }
        var len = (int)(length / 2L);
        _sieve = new BitArray(len, defaultValue);
    }

    public bool Get(long i)
    {
        bool result = false;
        if ((i & 1) == 1)  //isOdd
        {
            result = _sieve[(int)((i - 1) / 2)];
        }
        return result;
    }
    public void Set(long i, bool value)
    {
        _sieve[(int)((i - 1) / 2)] = value;
    }

    public IEnumerator<bool> GetEnumerator()
    {
        return new SieveEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool this[long index]
    {
        get => Get(index);
        set => Set(index, value);
    }


}

class SieveEnumerator : IEnumerator<bool>
{
    private Sieve _sieve;
    private long _index = -1;
    public SieveEnumerator(Sieve sieve)
    {
        _sieve = sieve;
    }
    public bool Current => _sieve[_index];
    object IEnumerator.Current => Current;
    public void Dispose()
    {
        _sieve = null!;
    }
    public bool MoveNext()
    {
        _index++;
        return (_index >= 0) && (_index < _sieve.Count);
    }
    public void Reset()
    {
        _index = -1;
    }
}

