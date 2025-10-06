using System.Collections;
using Pure.Collections.Generic.Tests.Fakes;
using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;
using Pure.Primitives.Random.Number;

namespace Pure.Collections.Generic.Tests;

public sealed record LookupTests
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
            new Lookup<INumber<int>, INumber<int>, INumber<int>>(
                distinctNumbers,
                x => x,
                _ => new MaxInt(),
                x => new DeterminedHash(x)
            ).Select(x => x.Key)
        );
    }

    [Fact]
    public void GroupAllDifferentOneSame()
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
            2,
            new Lookup<INumber<int>, INumber<int>, INumber<int>>(
                numbers,
                x => x,
                _ => new MaxInt(),
                x => new DeterminedHash(x)
            )[new Int(13)]
                .Count()
        );
    }

    [Fact]
    public void GroupAllSame()
    {
        IReadOnlyCollection<INumber<int>> numbers =
        [
            new Int(10),
            new Int(10),
            new Int(10),
            new Int(10),
            new Int(10),
        ];

        Assert.Equal(
            5,
            new Lookup<INumber<int>, INumber<int>, INumber<int>>(
                numbers,
                x => x,
                _ => new MaxInt(),
                x => new DeterminedHash(x)
            )[new Int(10)]
                .Count()
        );
    }

    [Fact]
    public void EnumeratesAsUntyped()
    {
        IReadOnlyCollection<INumber<int>> source =
        [
            new Int(1),
            new Int(2),
            new Int(3),
            new Int(4),
            new Int(5),
        ];

        IEnumerable lookup = new Lookup<INumber<int>, INumber<int>, INumber<int>>(
            source,
            x => x,
            x => x,
            x => new DeterminedHash(x)
        );

        ICollection<IGrouping<INumber<int>, INumber<int>>> list = [];

        foreach (object? item in lookup)
        {
            list.Add((IGrouping<INumber<int>, INumber<int>>)item);
        }

        Assert.Equal(source.Count, list.Count);
    }

    [Fact]
    public void EnumeratesAsTyped()
    {
        IReadOnlyCollection<INumber<int>> source =
        [
            new Int(1),
            new Int(2),
            new Int(3),
            new Int(4),
            new Int(5),
        ];

        ICollection<IGrouping<INumber<int>, INumber<int>>> lookup = new Lookup<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, x => x, x => new DeterminedHash(x)).ToArray();

        Assert.Equal(source.Count, lookup.Count);
    }

    [Fact]
    public void ContainsReturnCorrectValue()
    {
        IReadOnlyCollection<INumber<int>> source =
        [
            new Int(1),
            new Int(2),
            new Int(3),
            new Int(4),
            new Int(5),
        ];

        ILookup<INumber<int>, INumber<int>> dictionary = new Lookup<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, x => x, x => new DeterminedHash(x));

        Assert.True(dictionary.Contains(source.Last()));
    }

    [Fact]
    public void NotEnumerateSourceBeforeCall()
    {
        EnumerableWithEnumerationMarker<INumber<int>> source = new(
            new RandomIntCollection(new UShort(10))
        );

        _ = new Lookup<INumber<int>, INumber<int>, INumber<int>>(
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

        ILookup<INumber<int>, INumber<int>> lookup = new Lookup<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, _ => new MaxInt(), x => new DeterminedHash(x));

        foreach (INumber<int> _ in lookup.Select(x => x.Key))
        { }

        Assert.True(source.Enumerated);
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new Lookup<INumber<int>, INumber<int>, INumber<int>>(
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
            new Lookup<INumber<int>, INumber<int>, INumber<int>>(
                [],
                x => x,
                x => x,
                x => new DeterminedHash(x)
            ).ToString()
        );
    }
}
