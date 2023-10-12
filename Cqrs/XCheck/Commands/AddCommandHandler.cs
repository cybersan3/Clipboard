namespace Commands;

internal sealed class AddCommandHandler(ICounterProvider counterProvider) : ICommandHandler<AddCommand>
{
	public ValueTask HandleAsync(AddCommand command, CancellationToken cancellationToken)
	{
		counterProvider.Counter += command.Counter;
		return ValueTask.CompletedTask;
	}
}