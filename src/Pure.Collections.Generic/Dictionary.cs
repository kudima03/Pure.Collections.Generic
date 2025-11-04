using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Pure.HashCodes.Abstractions;

namespace Pure.Collections.Generic;

public sealed record Dictionary<TSource, TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull
{
    private readonly Lazy<IReadOnlyDictionary<TKey, TValue>> _lazyDictionary;

    public Dictionary(
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

    private Dictionary(
        IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector,
        IEqualityComparer<TKey> comparer
    )
    {
        _lazyDictionary = new Lazy<IReadOnlyDictionary<TKey, TValue>>(() =>
            source.ToFrozenDictionary(keySelector, valueSelector, comparer)
        );
    }

    private IReadOnlyDictionary<TKey, TValue> Inner => _lazyDictionary.Value;

    public int Count => Inner.Count;

    public IEnumerable<TKey> Keys => Inner.Keys;

    public IEnumerable<TValue> Values => Inner.Values;

    public bool ContainsKey(TKey key)
    {
        return Inner.ContainsKey(key);
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return Inner.TryGetValue(key, out value);
    }

    public TValue this[TKey key] => Inner[key];

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Inner.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
