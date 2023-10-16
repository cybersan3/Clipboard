namespace Lib.Queries;

public interface IQueryDispatcher
{
	ValueTask<TResponse> ExecuteQueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
}
