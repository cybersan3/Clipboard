namespace NAMESPACE;

internal class EXAMPLE_SYGENIC_COMMON_USAGE
{
	#region ctor DI
	private readonly ICmdRegistry cmdRegistry;
	private readonly ILogger<EXAMPLE_SYGENIC_COMMON_USAGE> logger;
	private readonly IImplementationProvider implementationsProvider;
	private readonly IEnv env;

	public EXAMPLE_SYGENIC_COMMON_USAGE(ICmdRegistry cmdRegistry, 
		ILogger<EXAMPLE_SYGENIC_COMMON_USAGE> logger,
    IImplementationProvider implementationsProvider,
		IEnv env)
	{
		this.cmdRegistry = cmdRegistry;
		this.logger = logger;
		this.implementationsProvider = implementationsProvider;
		this.env = env;
	}
	#endregion

	public void ShowIEnv()
  {
    logger.LogInformation("env.CommandLine {env.CommandLine}", env.CommandLine);
    logger.LogInformation("env.CurrentDirectory {env.CurrentDirectory}", env.CurrentDirectory);
    logger.LogInformation("env.CurrentManagedThreadId {env.CurrentManagedThreadId}", env.CurrentManagedThreadId);
    logger.LogInformation("env.ExitCode {env.ExitCode} + method Exit(exitCode) exists :-)", env.ExitCode);
    logger.LogInformation("env.GetCommandLineArgs() {env.GetCommandLineArgs()}", string.Join(';', env.GetCommandLineArgs()));
    LogGetEnvironmentVariables();
    LogSpecialFolders();
  }

  private void LogSpecialFolders()
  {
    var specialFolderValues = Enum.GetValues<Environment.SpecialFolder>();
    var specialFolderNames = Enum.GetNames<Environment.SpecialFolder>();
    ShouldNotBeHereException.ThrowIf(specialFolderNames.Length != specialFolderValues.Length);
    for (int index = 0; index < specialFolderValues.Length; index++)
    {
			logger.LogInformation(
				$"env.GetFolderPath(Environmnet.SpecialFolder.{specialFolderNames[index]})={{value}}", 
				env.GetFolderPath(specialFolderValues[index]));
    }
  }

  private void LogGetEnvironmentVariables()
  {
    var dict = env.GetEnvironmentVariables();
    foreach (var key in dict.Keys)
    {
      logger.LogInformation($"env.GetEnvironmentVariables()[{key}]={{value}}", dict[key]);
    }
  }

  public async Task LambdaCmdsAsync(CancellationToken cancellationToken)
	{
		int counter = 0;
		cmdRegistry.RegisterLambdaCmd("command name", () => true, () => logger.LogWarning($"lambda cmd {counter++}"));

		var cmds = cmdRegistry.GetCmds("command name, command name, command name");
		foreach (var cmd in cmds)
		{
			if (await cmd.CanExecuteAsync(cancellationToken))
			{
				await cmd.ExecuteAsync(cancellationToken);
			}
		}
	}

	public async Task RunKeyGenerationAsync(CancellationToken cancellationToken)
	{
		var cmd = cmdRegistry.GetCmd(nameof(GenerateRandomKeyCmd)) as ICmd<GenerateRandomKeyContext> ?? throw new Exception();

		var context = new GenerateRandomKeyContext("input");
		if (await cmd.CanExecuteAsync(context, cancellationToken))
		{
			await cmd.ExecuteAsync(context, cancellationToken);
			logger.LogError("RandomKeyGenerator + Cmd + Context: input: {input} output: {output}", context.Input, context.Output);
		}
	}

	public record GenerateRandomKeyContext(string Input)
	{
		public string Output { get; set; } = "";
	}

	public class GenerateRandomKeyCmd : ICmd<GenerateRandomKeyContext>
	{
		#region ctor DI
		private readonly ILogger<GenerateRandomKeyCmd> logger;
		private readonly ISerializer serializer;

		public GenerateRandomKeyCmd(ILogger<GenerateRandomKeyCmd> logger, ISerializer serializer)
		{
			this.logger = logger;
			this.serializer = serializer;
		} 
		#endregion

		public static string Name { get; } = nameof(GenerateRandomKeyCmd);

		public Task<bool> CanExecuteAsync(GenerateRandomKeyContext context, CancellationToken cancellationToken) => Task.FromResult(true);
		public Task<bool> CanExecuteAsync(CancellationToken cancellationToken) => Task.FromResult(true);
		public Task ExecuteAsync(GenerateRandomKeyContext context, CancellationToken cancellationToken)
		{
			context.Output = KeyGenerator.CreateId();
			logger.LogInformation("Serialized context: {context}", serializer.ToJson(context));
			return Task.CompletedTask;
		}
		public Task ExecuteAsync(CancellationToken cancellationToken) => throw new NotImplementedException();
	}

	public void GetAssembliesAndImplementations()
	{
		foreach (var assembly in implementationsProvider.KnownAssembliesAsEnumerable())
		{
			logger.LogCritical("Assembly: {assembly}", assembly.FullName);
		}

		var types = implementationsProvider.GetConcreteImplementationsOf(typeof(ICmd));
		foreach (var type in types)
		{
			logger.LogWarning("Implementation: {type}", type.FullName);
		}
	}

	public void DisplayGuidExtensions()
	{
		var guid = Guid.NewGuid();
		logger.LogError("Guid: {guid} Encoded: {encoded}", guid, guid.ToBase64EncodedString());
	}
}
