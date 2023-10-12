namespace Lib;

public interface ICommandHandler<in COMMAND> where COMMAND : ICommand
{
	ValueTask HandleAsync(COMMAND command, CancellationToken cancellationToken);
}