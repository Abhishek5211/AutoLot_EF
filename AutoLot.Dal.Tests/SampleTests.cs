namespace AutoLot.Dal.Tests;

public class SampleTests
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(5, 3 + 2);
    }

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-1, 1, 0)]

    public void AppendTest(int add1, int add2, int Result)
    {
        Assert.Equal(Result, add2 + add1);
    }
}
