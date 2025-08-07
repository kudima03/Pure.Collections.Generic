using Pure.Collections.Generic.Tests.Fakes;
using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;

namespace Pure.Collections.Generic.Tests;

public sealed record SetTests
{
    [Fact]
    public void InitializeCorrectlyOnDifferentValues()
    {
        IReadOnlyCollection<INumber<int>> randomNumbers =
        [
            new Int(10),
            new Int(11),
            new Int(12),
            new Int(13),
        ];
        Assert.Equal(
            randomNumbers,
            new Set<INumber<int>>(randomNumbers, x => new DeterminedHash(x))
        );
    }

    [Fact]
    public void InitializeCorrectlyOnAllDifferentOneSame()
    {
        IReadOnlyCollection<INumber<int>> numbers =
        [
            new Int(10),
            new Int(11),
            new Int(12),
            new Int(13),
            new Int(13),
        ];
        Assert.Equal(
            4,
            new Set<INumber<int>>(numbers, x => new DeterminedHash(x)).Count()
        );
    }

    [Fact]
    public void InitializeCorrectlyOnAllSame()
    {
        IReadOnlyCollection<INumber<int>> numbers =
        [
            new Int(10),
            new Int(10),
            new Int(10),
            new Int(10),
            new Int(10),
        ];
        _ = Assert.Single(new Set<INumber<int>>(numbers, x => new DeterminedHash(x)));
    }

    [Fact]
    public void NotEnumerateSourceBeforeCall()
    {
        EnumerableWithEnumerationMarker<INumber<int>> source = new(
            new RandomIntCollection(new UShort(10))
        );
        _ = new Set<INumber<int>>(source, x => new DeterminedHash(x));
        Assert.False(source.Enumerated);
    }

    [Fact]
    public void EnumerateSourceAfterCall()
    {
        EnumerableWithEnumerationMarker<INumber<int>> source = new(
            new RandomIntCollection(new UShort(10))
        );
        IEnumerable<INumber<int>> set = new Set<INumber<int>>(
            source,
            x => new DeterminedHash(x)
        );
        foreach (INumber<int> _ in set)
        { }
        Assert.True(source.Enumerated);
    }

    [Fact]
    public void ContainsWorksCorrectly()
    {
        IReadOnlyCollection<INumber<int>> numbers =
        [
            new Int(10),
            new Int(11),
            new Int(12),
            new Int(13),
            new Int(13),
        ];

        Assert.Contains(
            new Int(10),
            new Set<INumber<int>>(numbers, x => new DeterminedHash(x))
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Set<INumber<int>>([], x => new DeterminedHash(x)).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Set<INumber<int>>([], x => new DeterminedHash(x)).ToString()
        );
    }
}
