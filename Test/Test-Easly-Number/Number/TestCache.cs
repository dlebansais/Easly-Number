namespace TestNumber;

using EaslyNumber;
using NUnit.Framework;
using System.Threading;

[TestFixture]
public partial class TestCache
{
    [Test]
    [Category("Coverage")]
    public static void TestCacheThread()
    {
        Thread ThreadObject = new Thread(new ThreadStart(ExecuteThreadTest));
        ThreadObject.Start();
        ThreadObject.Join();
    }

    private static void ExecuteThreadTest()
    {
        bool Success = ThreadTestInternal();
        Assert.IsTrue(Success);
    }

    private static bool ThreadTestInternal()
    {
        bool Result = false;

        Number TestNumber = new Number("0");

        if (TestNumber.IsZero)
        {
            mpfr_t SomeObject = new();

            using (Cache TestCache = SomeObject.LibraryCache)
            {
                Result = true;
            }
        }

        return Result;
    }
}
