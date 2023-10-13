namespace Lib.Queries;

internal sealed class QueryHandlerProvider(IImplementationProvider implementationProvider) : IQueryHandlerProvider
{
	private readonly Lazy<MappingTypeToType> Mapping = new(() => CreateMapping(implementationProvider));

	private static readonly string iQueryHandlerName = typeof(IQueryHandler<,>).Name;

	private static MappingTypeToType CreateMapping(IImplementationProvider implementationProvider)
	{
		var mapping = new MappingTypeToType();
		var handlerTypes = implementationProvider.GetTypesImplementingOrExtending(typeof(IQueryHandler<,>));
		foreach (var handlerType in handlerTypes)
		{
			var interfce = handlerType.GetInterface(iQueryHandlerName) 
				?? throw new ShouldNotBeHereException($"{iQueryHandlerName} not implemented");

			var genericArguments = interfce.GetGenericArguments();
			ShouldNotBeHereException.ThrowIf(genericArguments.Length != 2);
			mapping[genericArguments[0]] = handlerType;
		}
		return mapping;
	}

	public Type GetHandlerForQuery<RESPONSE>(IQuery<RESPONSE> query)
	{
		var queryType = query.GetType();
		if (Mapping.Value.TryGetValue(queryType, out var handlerType)) return handlerType;

		throw new NotImplementedException($"Handler for query {queryType} not found/registered/implemented");
	}
}
