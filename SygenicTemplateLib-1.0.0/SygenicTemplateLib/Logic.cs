namespace SygenicTemplateLib;

internal sealed class Logic : IHostedService
{
  #region ctor DI
  private readonly ILogger<Logic> logger;
  private readonly ICyberConsoleHelper cyberConsoleHelper;
  private readonly IHelpFile helpFile;
  private readonly ISerializer serializer;
  private readonly LastingSettings lastingSettings;
  private readonly IHostApplicationLifetime hostApplicationLifetime;
  private readonly EXAMPLE_SYGENIC_COMMON_USAGE exampleToRemove;
  private readonly GeneralSettings generalSettings;

  public Logic(ILogger<Logic> logger, ICyberConsoleHelper cyberConsoleHelper, IHelpFile helpFile,
    ISerializer serializer, LastingSettings lastingSettings, IHostApplicationLifetime hostApplicationLifetime,
    IOptions<GeneralSettings> optionsForGeneralSettings, EXAMPLE_SYGENIC_COMMON_USAGE exampleToRemove)
  {
    this.logger = logger;
    this.cyberConsoleHelper = cyberConsoleHelper;
    this.helpFile = helpFile;
    this.serializer = serializer;
    this.lastingSettings = lastingSettings;
    this.hostApplicationLifetime = hostApplicationLifetime;
    this.exampleToRemove = exampleToRemove;
    this.generalSettings = optionsForGeneralSettings.Value;
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
    services.TryAddSingleton(new LastingSettings());

    services.Configure<AppSettings>(config);
    services.Configure<GeneralSettings>(config.GetSection("General"));
    services.Configure<HelpFileSettings>(config.GetSection("Help"));
    services.Configure<CyberConsoleSettings>(config.GetSection("CyberConsole"));

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
    if (! string.IsNullOrWhiteSpace(generalSettings.LastingSettingsFileName) && File.Exists(generalSettings.LastingSettingsFileName))
    {
      serializer.PopulateObject(lastingSettings, File.ReadAllText(generalSettings.LastingSettingsFileName));
    }

    cyberConsoleHelper.MaybeDisplayAllEnabledLogLevels();
    helpFile.MaybeDisplayHelpFile();

    if (generalSettings.ShowAppNameOnStart)
    {
      logger.LogInformation("{appname} {appVersion}, SygenicCommonLib {commonLibVersion}, SygenicTemplateLib {templateLibVersion}",
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