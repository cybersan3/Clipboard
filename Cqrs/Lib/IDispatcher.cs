namespace Lib;

public interface IDispatcher
{
	ValueTask ExecuteCommandAsync<COMMAND>(COMMAND command, CancellationToken cancellationToken) 
		where COMMAND : ICommand;

	ValueTask<RESPONSE> ExecuteQueryAsync<QUERY, RESPONSE>(QUERY query, CancellationToken cancellationToken)
		where QUERY : IQuery<RESPONSE>;
}

internal sealed class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
	public async ValueTask ExecuteCommandAsync<COMMAND>(COMMAND command, CancellationToken cancellationToken) 
		where COMMAND : ICommand
	{
		using var scope = serviceProvider.CreateScope();
		var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<COMMAND>>();
		await handler.HandleAsync(command, cancellationToken);
	}


	public async ValueTask<RESPONSE> ExecuteQueryAsync<QUERY, RESPONSE>(QUERY query, CancellationToken cancellationToken)
		where QUERY : IQuery<RESPONSE>
	{
		using var scope = serviceProvider.CreateScope();
		var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<QUERY, RESPONSE>>();

		return await handler.HandleAsync(query, cancellationToken);
	}
}