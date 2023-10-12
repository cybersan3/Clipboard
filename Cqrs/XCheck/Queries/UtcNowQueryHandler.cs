namespace Queries;

internal sealed class UtcNowQueryHandler(IClock clock) : IQueryHandler<UtcNowQuery, DateTime>
{
	public ValueTask<DateTime> HandleAsync(UtcNowQuery query, CancellationToken cancellationToken) 
		=> ValueTask.FromResult(clock.UtcNow);
}
