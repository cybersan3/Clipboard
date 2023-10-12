namespace Lib;

public static class CqrsExtensions
{
	public static IServiceCollection AddCommandsAndQueriesHandlers(this IServiceCollection services)
	{
		services.Scan(x => x
			.FromApplicationDependencies()
			.AddClasses(x => x.AssignableTo(typeof(IQueryHandler<,>)))
			.AsImplementedInterfaces()
			.WithTransientLifetime());

		services.Scan(x => x
			.FromApplicationDependencies()
			.AddClasses(x => x.AssignableTo(typeof(ICommandHandler<>)))
			.AsImplementedInterfaces()
			.WithTransientLifetime());

		return services;
	}

	public static IServiceCollection TryAddCqrs(this IServiceCollection services)
	{
		services.TryAddTransient<IDispatcher, Dispatcher>();
		
		return services;
	}
}