namespace SygenicCommonLib;

public interface IProcessExecutor
{
	Task<ProcessOutcome> StartAsync(CancellationToken cancellationToken, string? workingDir, string executable, params object[] args);
}