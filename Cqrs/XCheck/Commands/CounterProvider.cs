namespace Commands;

public interface ICounterProvider
{
	int Counter { get; set; }
}

internal sealed class CounterProvider : ICounterProvider
{
	public int Counter { get; set; }
}
