using System.Collections;

namespace Pure.Collections.Generic.Tests.Fakes;

internal sealed class EnumerableWithEnumerationMarker<T>(IEnumerable<T> source)
    : IEnumerable<T>
{
    private readonly IEnumerable<T> _source = source;

    public bool Enumerated { get; private set; }

    public IEnumerator<T> GetEnumerator()
    {
        Enumerated = true;
        return _source.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
