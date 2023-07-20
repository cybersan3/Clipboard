using SygenicCommonLib;

namespace NAMESPACE;

internal sealed class Logic : IHostedService
{
	#region ctor DI
	private readonly ILogger<Logic> logger;
	private readonly ICyberConsoleHelper cyberConsoleHelper;
	private readonly IHelpFile helpFile;
  private readonly ISerializer serializer;
  private readonly LastingSettings lastingSettings;
  private readonly EXAMPLE_SYGENIC_COMMON_USAGE exampleToRemove;
	private readonly AppSettings settings;

	public Logic(ILogger<Logic> logger, IOptions<AppSettings> options, ICyberConsoleHelper cyberConsoleHelper, IHelpFile helpFile, 
		ISerializer serializer, LastingSettings lastingSettings, EXAMPLE_SYGENIC_COMMON_USAGE exampleToRemove)
	{
		this.logger = logger;
		this.cyberConsoleHelper = cyberConsoleHelper;
		this.helpFile = helpFile;
    this.serializer = serializer;
    this.lastingSettings = lastingSettings;
    this.exampleToRemove = exampleToRemove;
		this.settings = options.Value;
	}
	#endregion

	/// <summary>
	/// Configure services here
	/// </summary>
	/// <param name="context"></param>
	/// <param name="services"></param>
	public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
	{
		var config = context.Configuration;

		services.TryAddSygenicCommonLib();

		services.Configure<AppSettings>(config);
		services.Configure<CyberConsoleSettings>(config.GetSection("CyberConsole"));
		services.Configure<HelpFileSettings>(config.GetSection("Help"));

		services.AddHostedService<Logic>();

		services.AddTransient<EXAMPLE_SYGENIC_COMMON_USAGE>();
	}

	public async Task StartAsync(CancellationToken cancellationToken)
  {
    InfrastructureBegin();

    await exampleToRemove.LambdaCmdsAsync(cancellationToken);
    await exampleToRemove.RunKeyGenerationAsync(cancellationToken);
    exampleToRemove.GetAssembliesAndImplementations();
    exampleToRemove.DisplayGuidExtensions();
    exampleToRemove.ShowIEnv();

    InfrastructureEnd();
  }

  private void InfrastructureBegin()
  {
    if (File.Exists(Const.LastingSettingsFileName))
    {
      serializer.PopulateObject(lastingSettings, File.ReadAllText(Const.LastingSettingsFileName));
    }

    cyberConsoleHelper.MaybeDisplayAllEnabledLogLevels();
    helpFile.MaybeDisplayHelpFile();

    logger.LogInformation("App {appVersion}, template {vsTemplateVersion}, SygenicCommonLib {sygenicCommonLibVersion}",
      Const.AppVersion, Const.ConsoleTemplateVersion, CommonLibConst.Version);
  }

	private void InfrastructureEnd()
	{
    File.WriteAllText(Const.LastingSettingsFileName, serializer.ToJson(lastingSettings));

    logger.LogCritical("Logic is done");
	}

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}