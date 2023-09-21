namespace SygenicCommonLib;

[NotTested]
[Serializable]
internal sealed class TooManyMembersFoundException : BaseIfException<TooManyMembersFoundException>
{
	public TooManyMembersFoundException()
	{
	}

	public TooManyMembersFoundException(string? message) : base(message)
	{
	}
}