namespace Queries;

internal sealed record EchoQuery(string Input) : IQuery<string>;