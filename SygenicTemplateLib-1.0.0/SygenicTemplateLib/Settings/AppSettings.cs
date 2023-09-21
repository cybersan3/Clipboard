namespace SygenicTemplateLib;

internal sealed class AppSettings
{
  public GeneralSettings General { get; set; } = new();
}

internal sealed class GeneralSettings
{
  public bool CtrlCToExit { get; set; } = false;

  /// <summary>
  /// If empty or white space - lasting settings will not be loaded or saved
  /// </summary>
  public string LastingSettingsFileName { get; set; } = "";

  public bool ShowAppNameOnStart { get; set; } = true;
}