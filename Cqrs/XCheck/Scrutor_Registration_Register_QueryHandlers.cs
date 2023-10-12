namespace XCheck;

public class Scrutor_Registration_Register_QueryHandlers
{
	[Fact]
	public void _()
	{
		var echoQueryHandler = X.Services.Get<IQueryHandler<EchoQuery, string>>();
		Assert.IsType<EchoQueryHandler>(echoQueryHandler);
	}
}
