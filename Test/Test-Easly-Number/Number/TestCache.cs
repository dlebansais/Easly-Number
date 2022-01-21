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
        Thread ThreadObject1 = new Thread(new ThreadStart(ExecuteThreadTest));
        Thread ThreadObject2 = new Thread(new ThreadStart(ExecuteThreadTest));
        Thread ThreadObject3 = new Thread(new ThreadStart(ExecuteThreadTest));

        ThreadObject1.Start();
        ThreadObject2.Start();
        ThreadObject3.Start();

        ThreadObject1.Join();
        ThreadObject2.Join();
        ThreadObject3.Join();
    }

    private static void ExecuteThreadTest()
    {
        _ = new Number(1.0);

        Cache LibraryCache = mpfr_t.LibraryCache;
        LibraryCache.Dispose();
        LibraryCache.Dispose();
    }
}
