namespace Messages;

public record DelayedMessage
{
	public Guid Id { get; init; } = Guid.NewGuid();
	public TimeSpan Seconds { get; init; }
}