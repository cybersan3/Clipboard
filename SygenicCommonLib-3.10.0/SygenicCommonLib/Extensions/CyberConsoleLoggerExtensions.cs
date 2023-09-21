namespace SygenicCommonLib;

[Tested]
public static class CyberConsoleLoggerExtensions
{
	public static ILoggingBuilder AddCyberConsole(this ILoggingBuilder builder) =>
			builder
					.AddConsole(options => options.FormatterName = nameof(CyberConsole))
					.AddConsoleFormatter<CyberConsole, CyberConsoleSettings>();

	public static ILoggingBuilder AddCyberConsole(this ILoggingBuilder builder, Action<CyberConsoleSettings> configure) =>
			builder
					.AddConsole(options => options.FormatterName = nameof(CyberConsole))
					.AddConsoleFormatter<CyberConsole, CyberConsoleSettings>(configure);
}