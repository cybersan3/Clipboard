namespace Lib;

public interface IQueryHandlerProvider
{
	Type GetHandlerForQuery<TResponse>(IQuery<TResponse> query);
}