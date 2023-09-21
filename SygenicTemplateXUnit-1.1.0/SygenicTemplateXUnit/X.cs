namespace SygenicTemplateXUnit;

public static class X
{
  private static readonly Version Version = new(1, 1, 0);

  internal static readonly IServiceProvider Services;

  static X()
  {
    var programMainArgs = Array.Empty<string>();
    var environmentalPrefix = "";

    var host = SygenicProgram.TryBuildHost(programMainArgs, environmentalPrefix, (hostBuilderContext, services) =>
    {
      services.TryAddSygenicCommonLib();
      SygenicTemplateLib.Logic.ConfigureServices(hostBuilderContext, services);

      var appSettings = new AppSettings
      {
        General = new GeneralSettings
        {

        }
      };
      services.AddSingleton(Options.Create(appSettings));

    }) ?? throw new Exception("Host is null");

    Services = host.Services;
  }
}