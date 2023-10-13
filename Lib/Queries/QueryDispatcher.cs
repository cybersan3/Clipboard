namespace Lib.Queries;

internal sealed class QueryDispatcher(IQueryHandlerProvider queryHandlerProvider, IServiceProvider serviceProvider) : IQueryDispatcher
{
	public async ValueTask<TResponse> ExecuteQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
	{
		var handlerType = queryHandlerProvider.GetHandlerForQuery(query);
		using var scope = serviceProvider.CreateScope();
		var handlerCaller = scope.ServiceProvider.GetRequiredService(handlerType) as IQueryHandlerCaller<TResponse> 
			?? throw new ShouldNotBeHereException($"Service casted to {nameof(IQueryHandlerCaller<TResponse>)} is null");

		return await handlerCaller.CallQueryHandlerAsyc(query, cancellationToken);
	}
}
