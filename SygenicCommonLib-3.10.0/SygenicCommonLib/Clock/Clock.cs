namespace SygenicCommonLib;

[Tested]
internal class Clock : IClock
{
	public DateTime UtcNow => DateTime.UtcNow;
}
