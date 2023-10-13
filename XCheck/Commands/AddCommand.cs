namespace Commands;

internal sealed record AddCommand(int Counter) : ICommand;