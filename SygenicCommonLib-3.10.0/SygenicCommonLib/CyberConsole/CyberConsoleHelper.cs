namespace SygenicCommonLib;

[Tested]
internal sealed class CyberConsoleHelper : ICyberConsoleHelper
{
	#region ctor DI
	private readonly ILogger<CyberConsoleHelper> logger;
	private readonly CyberConsoleSettings settings;

	public CyberConsoleHelper(ILogger<CyberConsoleHelper> logger, IOptions<CyberConsoleSettings> options)
	{
		this.logger = logger;
		settings = options.Value;
	} 
	#endregion

	public void MaybeDisplayAllEnabledLogLevels()
	{
		if (settings.DisplayAllEnabledLogLevelsOnStart)
		{
			logger.LogTrace("TRACE log level is on");
			logger.LogDebug("DEBUG log level is on");
			logger.LogInformation("INFO log level is on");
			logger.LogWarning("WARN log level is on");
			logger.LogError("ERROR log level is on");
			logger.LogCritical("CRITICAL log level is on");
		}
	}
}
