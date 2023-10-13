namespace Lib;

public static class CqrsExtensions
{
	public static IServiceCollection AddCommandsAndQueriesHandlers(this IServiceCollection services)
	{
		services.Scan(x => x
			.FromApplicationDependencies()
			.AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
			.AsSelfWithInterfaces()
			.WithTransientLifetime());

		services.Scan(x => x
			.FromApplicationDependencies()
			.AddClasses(x => x.AssignableTo(typeof(ICommandHandler<>)))
			.AsSelfWithInterfaces()
			.WithTransientLifetime());

		return services;
	}

	public static IServiceCollection TryAddCqrs(this IServiceCollection services)
	{
		services.TryAddSingleton<IQueryHandlerProvider, QueryHandlerProvider>();
		services.TryAddTransient<ICommandDispatcher, CommandDispatcher>();
		services.TryAddTransient<IQueryDispatcher, QueryDispatcher>();
		
		return services;
	}
}