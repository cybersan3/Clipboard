namespace SygenicTemplateConsole;

internal sealed class Program
{
  public static async Task<int> Main(string[] args)
  {
    try
    {
      var host = SygenicProgram.TryBuildHost(args, Const.ENVIRONMENTAL_VARIABLES_PREFIX, Logic.ConfigureServices)
        ?? throw new Exception("host is null");

      return await SygenicProgram.TryRunHostAsync(host, CancellationToken.None);
    }
    catch (Exception ex)
    {
      System.Console.Error.WriteLine($"General error during Main: {ex}");
      return 1;
    }
  }
}
