using System.Collections.Frozen;
using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;

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

        Assert.Contains(new Int(10), numbers);
    }

    [Fact]
    public void UnderlyingTypeIsFrozenSet()
    {
        using IEnumerator<INumber<int>> enumerator = new Set<INumber<int>>(
            [],
            x => new DeterminedHash(x)
        ).GetEnumerator();

        _ = Assert.IsType<FrozenSet<INumber<int>>.Enumerator>(enumerator);
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
