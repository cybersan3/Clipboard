namespace Lib;

internal sealed class Logic(
	ILogger<Logic> logger,
	IColorConsoleHelper cyberConsoleHelper,
	IHelpFile helpFile,
	ISerializer serializer,
	LastingSettings lastingSettings,
	IHostApplicationLifetime hostApplicationLifetime,
	IOptions<GeneralSettings> optionsForGeneralSettings) : IHostedService
{
	private readonly GeneralSettings generalSettings = optionsForGeneralSettings.Value;

	/// <summary>
	/// Configure services here
	/// </summary>
	/// <param name="context"></param>
	/// <param name="services"></param>
	public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
	{
		var config = context.Configuration;

		services.TryAddSygenicCommonLib();
		services.TryAddSingleton(new LastingSettings());

		services.Configure<AppSettings>(config);
		services.Configure<GeneralSettings>(config.GetSection("General"));
		services.Configure<HelpFileSettings>(config.GetSection("Help"));
		services.Configure<ColorConsoleSettings>(config.GetSection("CyberConsole"));

		services.AddHostedService<Logic>();

	}

	public async Task StartAsync(CancellationToken cancellationToken)
	{
		InfrastructureBegin();


		InfrastructureEnd();
	}

	private void InfrastructureBegin()
	{
		if (!string.IsNullOrWhiteSpace(generalSettings.LastingSettingsFileName) && File.Exists(generalSettings.LastingSettingsFileName))
		{
			serializer.PopulateObject(lastingSettings, File.ReadAllText(generalSettings.LastingSettingsFileName));
		}

		cyberConsoleHelper.MaybeDisplayAllEnabledLogLevels();
		helpFile.MaybeDisplayHelpFile();

		if (generalSettings.ShowAppNameOnStart)
		{
			logger.LogInformation("{appname} {appVersion}, SygenicCommonLib {commonLibVersion}, Sygenic.Cqrs.Lib {templateLibVersion}",
			  Const.AppName, Const.AppVersion, CommonLibConst.Version, Const.SygenicTemplateLibVersion);
		}
	}

	private void InfrastructureEnd()
	{
		if (!string.IsNullOrWhiteSpace(generalSettings.LastingSettingsFileName))
		{
			File.WriteAllText(generalSettings.LastingSettingsFileName, serializer.ToJson(lastingSettings));
		}

		if (generalSettings.CtrlCToExit)
		{
			logger.LogCritical("{appName} {appVersion} is done, press Ctrl-C to exit", Const.AppName, Const.AppVersion);
		}
		else
		{
			hostApplicationLifetime.StopApplication();
		}
	}

	public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}