namespace TestEaslyNumber;

using EaslyNumber;
using NUnit.Framework;
using System;
using System.Threading;

[TestFixture]
public partial class TestCache
{
    [Test]
    [Category("Coverage")]
    public static void TestMultipleThreads()
    {
        Thread t1 = new Thread(new ParameterizedThreadStart(ExecuteThreadTest));
        Thread t2 = new Thread(new ParameterizedThreadStart(ExecuteThreadTest));
        Thread t3 = new Thread(new ParameterizedThreadStart(ExecuteThreadTest));

        t1.Start(TimeSpan.FromSeconds(1));
        t2.Start(TimeSpan.FromSeconds(1));
        t3.Start(TimeSpan.FromSeconds(10));

        t1.Join();
        t2.Join();

        GC.Collect();

        t3.Join();

        GC.Collect();
        Thread.Sleep(TimeSpan.FromSeconds(1));
    }

    private static void ExecuteThreadTest(object? parameter)
    {
        TimeSpan Duration = (TimeSpan)parameter!;

        Number TestNumber = new Number("1");
        TestNumber = TestNumber + 1;

        if (TestNumber > 0)
            Thread.Sleep(Duration);
    }
}
