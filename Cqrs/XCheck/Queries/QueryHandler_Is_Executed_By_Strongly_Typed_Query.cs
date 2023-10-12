namespace Queries;

public class QueryHandler_Is_Executed_By_Strongly_Typed_Query
{
	[Fact]
	public async Task _()
	{
		var queryDispatcher = X.Services.Get<IDispatcher>();

		var input = "Hello world I am Jan B!";
		var query = new EchoQuery(input);

		var response = await queryDispatcher.ExecuteQueryAsync<EchoQuery, string>(query, CancellationToken.None);
		Assert.Equal(expected: $"{input} {input}", actual: response);
	}
}