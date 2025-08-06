using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Pure.HashCodes;

namespace Pure.Collections.Generic;

public sealed record Dictionary<TSource, TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    where TKey : notnull
{
    private readonly IEnumerable<TSource> _source;

    private readonly Func<TSource, TKey> _keySelector;

    private readonly Func<TSource, TValue> _valueSelector;

    private readonly IEqualityComparer<TKey> _comparer;

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
        _source = source;
        _keySelector = keySelector;
        _valueSelector = valueSelector;
        _comparer = comparer;
    }

    public int Count =>
        _source.ToDictionary(_keySelector, _valueSelector, _comparer).Count;

    public IEnumerable<TKey> Keys =>
        _source.ToDictionary(_keySelector, _valueSelector, _comparer).Keys;

    public IEnumerable<TValue> Values =>
        _source.ToDictionary(_keySelector, _valueSelector, _comparer).Values;

    public bool ContainsKey(TKey key)
    {
        return _source
            .ToDictionary(_keySelector, _valueSelector, _comparer)
            .ContainsKey(key);
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return _source
            .ToDictionary(_keySelector, _valueSelector, _comparer)
            .TryGetValue(key, out value);
    }

    public TValue this[TKey key] =>
        _source.ToDictionary(_keySelector, _valueSelector, _comparer)[key];

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _source
            .ToDictionary(_keySelector, _valueSelector, _comparer)
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
