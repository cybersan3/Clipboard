namespace Commands;

public class Commands_work
{
	[Fact]
	public async Task _()
	{
		(var cmdDispatcher, var counterProvider) = X.Services.Get<IDispatcher, ICounterProvider>();

		var addCmd = new AddCommand(3);
		await cmdDispatcher.ExecuteCommandAsync(addCmd, CancellationToken.None);
		await cmdDispatcher.ExecuteCommandAsync(addCmd, CancellationToken.None);

		var subCmd = new SubstractCommand(1);
		await cmdDispatcher.ExecuteCommandAsync(subCmd, CancellationToken.None);

		Assert.Equal(expected: 5, actual: counterProvider.Counter);
	}
}
