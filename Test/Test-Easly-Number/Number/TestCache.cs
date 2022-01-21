namespace TestNumber;

using EaslyNumber;
using NUnit.Framework;

[TestFixture]
public partial class TestCache
{
    [Test]
    [Category("Coverage")]
    public static void TestCacheThread()
    {
        long Count1 = Cache.FreeCount;

        using (Cache LocalCache = new())
        {
        }

        long Count2 = Cache.FreeCount;
        Assert.IsTrue(Count2 == Count1);

        using (Cache LocalCache = new())
        {
            _ = LocalCache.Value;
        }

        long Count3 = Cache.FreeCount;
        Assert.IsTrue(Count3 > Count2);
    }
}
