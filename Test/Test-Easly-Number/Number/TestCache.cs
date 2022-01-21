namespace TestNumber;

using EaslyNumber;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;

[TestFixture]
public partial class TestCache
{
    [Test]
    [Category("Coverage")]
    public static void TestCacheThread()
    {
        Thread ThreadObject1 = new Thread(new ThreadStart(ExecuteThreadWithCache));
        Thread ThreadObject2 = new Thread(new ThreadStart(ExecuteThreadWithoutCache));

        ThreadObject1.Start();
        ThreadObject2.Start();

        ThreadObject1.Join();
        ThreadObject2.Join();
    }

    private static void ExecuteThreadWithCache()
    {
        _ = new Number(1.0);

        Cache LibraryCache = mpfr_t.LibraryCache;
        LibraryCache.Dispose();
    }

    private static void ExecuteThreadWithoutCache()
    {
        _ = new Number("0");

        Cache LibraryCache = mpfr_t.LibraryCache;
        LibraryCache.Dispose();
    }
}
