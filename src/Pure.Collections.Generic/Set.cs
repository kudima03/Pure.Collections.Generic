using System.Collections;
using System.Collections.Frozen;
using Pure.HashCodes;

namespace Pure.Collections.Generic;

public sealed record Set<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _source;

    private readonly IEqualityComparer<T> _equalityComparer;

    public Set(IEnumerable<T> source, Func<T, IDeterminedHash> determinedHashFactory)
        : this(source, new EqualityComparerByDeterminedHash<T>(determinedHashFactory)) { }

    private Set(IEnumerable<T> source, IEqualityComparer<T> equalityComparer)
    {
        _source = source;
        _equalityComparer = equalityComparer;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _source.ToFrozenSet(_equalityComparer).GetEnumerator();
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
