namespace Lib;

public interface IQueryHandler<in QUERY, RESPONSE> where QUERY : IQuery<RESPONSE>
{
	ValueTask<RESPONSE> HandleAsync(QUERY query, CancellationToken cancellationToken);
}