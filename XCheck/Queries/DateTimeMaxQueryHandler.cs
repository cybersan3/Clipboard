namespace Queries;

internal sealed class DateTimeMaxQueryHandler() : IQueryHandler<DateTimeMaxQuery, DateTime>
{
	public ValueTask<DateTime> HandleAsync(DateTimeMaxQuery query, CancellationToken cancellationToken) 
		=> ValueTask.FromResult(DateTime.MaxValue);
}
