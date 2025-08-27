using System.Collections;
using Pure.Collections.Generic.Tests.Fakes;
using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Materialized.Number;
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

        IEnumerable dictionary = new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
            source,
            x => x,
            x => x,
            x => new DeterminedHash(x)
        );

        ICollection<KeyValuePair<INumber<int>, INumber<int>>> list = [];

        foreach (object? item in dictionary)
        {
            list.Add((KeyValuePair<INumber<int>, INumber<int>>)item);
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

        IReadOnlyDictionary<INumber<int>, INumber<int>> dictionary = new Dictionary<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, x => x, x => new DeterminedHash(x));

        Assert.Equal(source.Count, dictionary.Count);
    }

    [Fact]
    public void TryGetValueReturnsCorrectValue()
    {
        IReadOnlyCollection<INumber<int>> source =
        [
            new Int(1),
            new Int(2),
            new Int(3),
            new Int(4),
            new Int(5),
        ];

        IReadOnlyDictionary<INumber<int>, INumber<int>> dictionary = new Dictionary<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, x => x, x => new DeterminedHash(x));

        bool result = dictionary.TryGetValue(source.First(), out INumber<int>? value);

        Assert.True(result);
        Assert.Equal(source.First(), value);
    }

    [Fact]
    public void IndexOperatorReturnCorrectValue()
    {
        IReadOnlyCollection<INumber<int>> source =
        [
            new Int(1),
            new Int(2),
            new Int(3),
            new Int(4),
            new Int(5),
        ];

        IReadOnlyDictionary<INumber<int>, INumber<int>> dictionary = new Dictionary<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, x => x, x => new DeterminedHash(x));

        Assert.Equal(source.Last(), dictionary[source.Last()]);
    }

    [Fact]
    public void ContainsKeyReturnCorrectValue()
    {
        IReadOnlyCollection<INumber<int>> source =
        [
            new Int(1),
            new Int(2),
            new Int(3),
            new Int(4),
            new Int(5),
        ];

        IReadOnlyDictionary<INumber<int>, INumber<int>> dictionary = new Dictionary<
            INumber<int>,
            INumber<int>,
            INumber<int>
        >(source, x => x, x => x, x => new DeterminedHash(x));

        Assert.True(dictionary.ContainsKey(source.Last()));
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
            10,
            new Dictionary<INumber<int>, INumber<int>, INumber<int>>(
                numbers,
                x => x,
                x => x,
                x => new DeterminedHash(x)
            ).Keys.Select(x => new MaterializedNumber<int>(x).Value)
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
