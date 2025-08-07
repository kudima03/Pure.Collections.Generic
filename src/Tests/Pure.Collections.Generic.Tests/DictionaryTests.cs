using Pure.Collections.Generic.Tests.Fakes;
using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;

namespace Pure.Collections.Generic.Tests;

public sealed record DictionaryTests
{
    [Fact]
    public void InitializeCorrectlyOnDifferentValues()
    {
        IReadOnlyCollection<INumber<int>> distinctNumbers =
        [
            new Int(10),
            new Int(11),
            new Int(12),
            new Int(13),
        ];
        Assert.Equal(
            distinctNumbers,
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                distinctNumbers,
                x => x,
                _ => new MaxInt(),
                x => new DeterminedHash(x)
            ).Keys
        );
    }

#pragma warning disable xUnit1004
    [Fact(
        Skip = "ToString() override with exception blocks other exception message creation. Should be fixed."
    )]
    public void ThrowsExceptionOnAllDifferentOneSame()
    {
        IReadOnlyCollection<INumber<int>> numbers =
        [
            new Int(10),
            new Int(11),
            new Int(12),
            new Int(13),
            new Int(13),
        ];
        _ = Assert.Throws<ArgumentException>(() =>
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                numbers,
                x => x,
                _ => new MaxInt(),
                x => new DeterminedHash(x)
            ).Keys
        );
    }

    [Fact(
        Skip = "ToString() override with exception blocks other exception message creation. Should be fixed."
    )]
    public void ThrowsExceptionOnAllSame()
    {
        IReadOnlyCollection<INumber<int>> numbers =
        [
            new Int(10),
            new Int(10),
            new Int(10),
            new Int(10),
            new Int(10),
        ];
        _ = Assert.Throws<ArgumentException>(() =>
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                numbers,
                x => x,
                _ => new MaxInt(),
                x => new DeterminedHash(x)
            ).Keys
        );
    }

    [Fact]
    public void NotEnumerateSourceBeforeCall()
    {
        EnumerableWithEnumerationMarker<INumber<int>> source = new(
            new RandomIntCollection(new UShort(10))
        );

        _ = new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
            source,
            x => x,
            _ => new MaxInt(),
            x => new DeterminedHash(x)
        );

        Assert.False(source.Enumerated);
    }

    [Fact]
    public void EnumerateSourceAfterCall()
    {
        EnumerableWithEnumerationMarker<INumber<int>> source = new(
            new RandomIntCollection(new UShort(10))
        );

        IReadOnlyDictionary<INumber<int>, INumber<int>> dictionary = new Dictionary<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, _ => new MaxInt(), x => new DeterminedHash(x));

        foreach (INumber<int> _ in dictionary.Keys)
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
        ];

        Assert.Contains(
            new Int(10),
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                numbers,
                x => x,
                x => x,
                x => new DeterminedHash(x)
            ).Keys
        );
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                [],
                x => x,
                x => x,
                x => new DeterminedHash(x)
            ).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                [],
                x => x,
                x => x,
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
