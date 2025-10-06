using System.Collections;
using Pure.HashCodes;

namespace Pure.Collections.Generic;

public sealed record Lookup<TSource, TKey, TValue> : ILookup<TKey, TValue>
{
    private readonly Lazy<ILookup<TKey, TValue>> _lazyLookup;

    public Lookup(
        IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector,
        Func<TKey, IDeterminedHash> determinedHashFactory
    )
        : this(
            source,
            keySelector,
            valueSelector,
            new EqualityComparerByDeterminedHash<TKey>(determinedHashFactory)
        )
    { }

    private Lookup(
        IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector,
        IEqualityComparer<TKey> comparer
    )
    {
        _lazyLookup = new Lazy<ILookup<TKey, TValue>>(() =>
            source.ToLookup(keySelector, valueSelector, comparer)
        );
    }

    private ILookup<TKey, TValue> Inner => _lazyLookup.Value;

    public int Count => Inner.Count;

    public IEnumerable<TValue> this[TKey key] => Inner[key];

    public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
    {
        return Inner.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(TKey key)
    {
        return Inner.Contains(key);
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
