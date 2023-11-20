using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace _;

internal class Program
{
    public const int HOW_MANY_CALLS = 100_000;
    public readonly static ServiceImplementation service = new();

    private static void Main() => BenchmarkRunner.Run<CallsWithInstanceCreation>();
}

public abstract class Base
{
    public int counter;
}
public sealed class Implementation1 : Base;
public sealed class Implementation2 : Base;
public sealed class ServiceImplementation
{
    public void Service(Implementation1 impl) => impl.counter++;
    public void Service(Implementation2 impl) => impl.counter++;
    public void ServiceByType(Base baseImplementation)
    {
        var type = baseImplementation.GetType();
        if (type == typeof(Implementation1)) Service((Implementation1)baseImplementation);
        else if (type == typeof(Implementation2)) Service((Implementation2)baseImplementation);
    }
    public void ServiceByIs(Base baseImplementation)
    {
        if (baseImplementation is Implementation1 implementation1) Service(implementation1);
        else if (baseImplementation is Implementation2 implementation2) Service(implementation2);
    }
}

public class CallsWithInstanceCreation
{
    [Benchmark(Baseline = true)]
    public void CallByType()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Implementation1 implementation1 = new Implementation1();
            Program.service.Service(implementation1);
            Implementation2 implementation2 = new Implementation2();
            Program.service.Service(implementation2);
        }
    }

    [Benchmark]
    public void CallByGetType()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Base baseImplementation1 = new Implementation1();
            Program.service.ServiceByType(baseImplementation1);
            Base baseImplementation2 = new Implementation2();
            Program.service.ServiceByType(baseImplementation2);
        }
    }

    [Benchmark]
    public void CallByDynamic()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Base baseImplementation1 = new Implementation1();
            Program.service.Service((dynamic)baseImplementation1);
            Base baseImplementation2 = new Implementation2();
            Program.service.Service((dynamic)baseImplementation2);
        }
    }

    [Benchmark]
    public void CallByBaseIs()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Base baseImplementation1 = new Implementation1();
            Program.service.ServiceByIs(baseImplementation1);
            Base baseImplementation2 = new Implementation2();
            Program.service.ServiceByIs(baseImplementation2);
        }
    }
}

public class CallsWithoutInstanceCreation
{
    private readonly Implementation1 implementation1 = new();
    private readonly Implementation2 implementation2 = new();
    private readonly Base baseImplementation1 = new Implementation1();
    private readonly Base baseImplementation2 = new Implementation2();

    [Benchmark(Baseline = true)]
    public void CallByType()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Program.service.Service(implementation1);
            Program.service.Service(implementation2);
        }
    }

    [Benchmark]
    public void CallByGetType()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Program.service.ServiceByType(baseImplementation1);
            Program.service.ServiceByType(baseImplementation2);
        }
    }

    [Benchmark]
    public void CallByDynamic()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Program.service.Service((dynamic)baseImplementation1);
            Program.service.Service((dynamic)baseImplementation2);
        }
    }

    [Benchmark]
    public void CallByBaseIs()
    {
        for (int index = 0; index < Program.HOW_MANY_CALLS; index++)
        {
            Program.service.ServiceByIs(baseImplementation1);
            Program.service.ServiceByIs(baseImplementation2);
        }
    }
}