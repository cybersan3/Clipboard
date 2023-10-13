namespace Commands;

internal sealed class SubstractCommandHandler(ICounterProvider counterProvider) : ICommandHandler<SubstractCommand>
{
	public ValueTask HandleAsync(SubstractCommand command, CancellationToken cancellationToken)
	{
		counterProvider.Counter -= command.Counter;
		return ValueTask.CompletedTask;
	}
}
