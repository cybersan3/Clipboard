namespace Lib.Queries;

public interface IQueryHandlerCaller<TResponse>
{
	ValueTask<TResponse> CallQueryHandlerAsyc(IQuery<TResponse> query, CancellationToken cancellationToken);
}