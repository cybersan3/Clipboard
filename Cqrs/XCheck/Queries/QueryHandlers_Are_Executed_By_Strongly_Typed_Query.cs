namespace Queries;

public class QueryHandlers_Are_Executed_By_Strongly_Typed_Query
{
	[Fact]
	public async Task _()
	{
		var queryDispatcher = X.Services.Get<IQueryDispatcher>();

		var input = "Hello world I am Jan B!";
		var echoQuery = new EchoQuery(input);

		var echoResponse = await queryDispatcher.ExecuteQueryAsync(echoQuery, CancellationToken.None);
		Assert.Equal(expected: $"{input} {input}", actual: echoResponse);

		var dateTimeMaxQuery = new DateTimeMaxQuery();
		var utcResponse = await queryDispatcher.ExecuteQueryAsync(dateTimeMaxQuery, CancellationToken.None);
		Assert.Equal(expected: DateTime.MaxValue, actual: utcResponse);
	}
}