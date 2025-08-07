using Pure.HashCodes;
using Pure.Primitives.Abstractions.Number;
using Pure.Primitives.Number;

namespace Pure.Collections.Generic.Tests;

public sealed record EqualityComparerByDeterminedHashTests
{
    [Fact]
    public void ProduceCorrectEqualityOnSameValues()
    {
        Assert.Equal(
            new Int(100),
            new Int(100),
            new EqualityComparerByDeterminedHash<Int>(x => new DeterminedHash(x))
        );
    }

    [Fact]
    public void ProduceCorrectEqualityOnDifferentValues()
    {
        Assert.NotEqual(
            new Int(100),
            new Int(200),
            new EqualityComparerByDeterminedHash<Int>(x => new DeterminedHash(x))
        );
    }

    [Fact]
    public void ProduceDifferentHashCodes()
    {
        const int elementCount = 100;
        Random random = new Random();
        IEnumerable<int> hashCodes = Enumerable
            .Range(0, elementCount)
            .Select(_ => random.Next())
            .Distinct()
            .Select(x => new Int(x))
            .Select(x =>
                new EqualityComparerByDeterminedHash<INumber<int>>(
                    c => new DeterminedHash(c)
                ).GetHashCode(x)
            );

        Assert.Equal(elementCount, hashCodes.Distinct().Count());
    }

    [Fact]
    public void ThrowsExceptionOnGetHashCode()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new EqualityComparerByDeterminedHash<INumber<int>>(x => new DeterminedHash(
                x
            )).GetHashCode()
        );
    }

    [Fact]
    public void ThrowsExceptionOnToString()
    {
        _ = Assert.Throws<NotSupportedException>(() =>
            new EqualityComparerByDeterminedHash<INumber<int>>(x => new DeterminedHash(
                x
            )).ToString()
        );
    }
}
