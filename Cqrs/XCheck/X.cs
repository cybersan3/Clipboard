namespace XCheck;

public static class X
{
	private static readonly Version Version = new(1, 1, 1);

	internal static readonly IServiceProvider Services;

	static X()
	{
		var programMainArgs = Array.Empty<string>();
		var environmentalPrefix = "";

		var host = SygenicProgram.TryBuildHost(programMainArgs, environmentalPrefix, (hostBuilderContext, services) =>
		{
			services.TryAddSygenicCommonLib();
			Logic.ConfigureServices(hostBuilderContext, services);
			services.AddCommandsAndQueriesHandlers();
			services.TryAddCqrs();

			var appSettings = new AppSettings
			{
				General = new GeneralSettings
				{

				}
			};
			services.AddSingleton(Options.Create(appSettings));
			services.AddSingleton<ICounterProvider, CounterProvider>();
			//services.AddTransient<IQueryHandler<EchoQuery, string>, EchoQueryHandler>();

		}) ?? throw new Exception("Host is null");

		Services = host.Services;
	}
}