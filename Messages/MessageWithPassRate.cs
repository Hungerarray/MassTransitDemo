namespace Messages;

public record MessageWithPassRate
{
	public Guid Id { get; } = Guid.NewGuid();
	public double Value { get; init; }
}