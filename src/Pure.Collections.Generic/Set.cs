using System.Collections;
using System.Collections.Frozen;
using Pure.HashCodes.Abstractions;

namespace Pure.Collections.Generic;

public sealed record Set<T> : IEnumerable<T>
{
    private readonly Lazy<IReadOnlySet<T>> _set;

    public Set(IEnumerable<T> source, Func<T, IDeterminedHash> determinedHashFactory)
        : this(source, new EqualityComparerByDeterminedHash<T>(determinedHashFactory)) { }

    private Set(IEnumerable<T> source, IEqualityComparer<T> equalityComparer)
    {
        _set = new Lazy<IReadOnlySet<T>>(() => source.ToFrozenSet(equalityComparer));
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _set.Value.GetEnumerator();
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
