namespace Lib;

public interface ICommandDispatcher
{
	ValueTask ExecuteCommandAsync<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand;
}