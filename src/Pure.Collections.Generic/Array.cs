using System.Collections;

namespace Pure.Collections.Generic;

public sealed record Array<T> : IEnumerable<T>
{
    private readonly IEnumerable<T> _source;

    public Array(IEnumerable<T> source)
    {
        _source = source;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _source.ToArray().AsEnumerable().GetEnumerator();
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
