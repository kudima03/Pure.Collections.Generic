using System.Collections;

namespace Pure.Collections.Generic;

public sealed record Array<T> : IEnumerable<T>
{
    private readonly Lazy<T[]> _source;

    public Array(IEnumerable<T> source)
    {
        _source = new Lazy<T[]>(source.ToArray);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _source.Value.AsEnumerable().GetEnumerator();
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
