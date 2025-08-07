using Pure.Collections.Generic.Tests.Fakes;

namespace Pure.Collections.Generic.Tests;

public sealed record ArrayTests
{
    [Fact]
    public void InitializeCorrectly()
    {
        Random random = new Random();
        IReadOnlyCollection<int> randomNumbers =
        [
            .. Enumerable.Range(1, 100).Select(_ => random.Next()),
        ];
        Assert.Equal(randomNumbers, new Array<int>(randomNumbers));
    }

    [Fact]
    public void UnderlyingTypeIsArray()
    {
        using IEnumerator<int> enumerator = new Array<int>([]).GetEnumerator();

        Assert.Equal("SZGenericArrayEnumerator`1", enumerator.GetType().Name);
    }

    [Fact]
    public void NotEnumerateSourceBeforeCall()
    {
        EnumerableWithEnumerationMarker<int> source = new([1, 2, 3, 4, 5]);

        _ = new Array<int>(source);
        Assert.False(source.Enumerated);
    }

    [Fact]
    public void EnumerateSourceAfterCall()
    {
        EnumerableWithEnumerationMarker<int> source = new([1, 2, 3, 4, 5]);
        foreach (int i in new Array<int>(source)) { }
        Assert.True(source.Enumerated);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() => new Array<int>([]).GetHashCode());
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() => new Array<int>([]).ToString());
    }
}
