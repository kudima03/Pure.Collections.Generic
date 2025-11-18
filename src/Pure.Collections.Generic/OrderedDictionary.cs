using System.Collections;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;
using Pure.HashCodes.Abstractions;

namespace Pure.Collections.Generic;

public sealed record OrderedDictionary<TSource, TKey, TValue>
    : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull
{
    private readonly Lazy<IReadOnlyDictionary<TKey, TValue>> _lazyDictionary;

    private readonly Lazy<IEnumerable<KeyValuePair<TKey, TValue>>> _lazyCollection;

    public OrderedDictionary(
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

    private OrderedDictionary(
        IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TValue> valueSelector,
        IEqualityComparer<TKey> comparer
    )
    {
        source = [.. source];
        _lazyDictionary = new Lazy<IReadOnlyDictionary<TKey, TValue>>(() =>
            source.ToFrozenDictionary(keySelector, valueSelector, comparer)
        );

        _lazyCollection = new Lazy<IEnumerable<KeyValuePair<TKey, TValue>>>(() =>
            source.Select(x => new KeyValuePair<TKey, TValue>(
                keySelector(x),
                valueSelector(x)
            ))
        );
    }

    private IReadOnlyDictionary<TKey, TValue> InnerDict => _lazyDictionary.Value;

    private IEnumerable<KeyValuePair<TKey, TValue>> InnerCollection =>
        _lazyCollection.Value;

    public int Count => InnerDict.Count;

    public IEnumerable<TKey> Keys => InnerCollection.Select(x => x.Key);

    public IEnumerable<TValue> Values => InnerCollection.Select(x => x.Value);

    public bool ContainsKey(TKey key)
    {
        return InnerDict.ContainsKey(key);
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return InnerDict.TryGetValue(key, out value);
    }

    public TValue this[TKey key] => InnerDict[key];

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return InnerCollection.GetEnumerator();
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
