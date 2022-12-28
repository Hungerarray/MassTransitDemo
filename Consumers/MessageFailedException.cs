namespace Consumers.Exceptions;

public class MessageFailedException : Exception
{
	public MessageFailedException() : base()
	{
	}

	public MessageFailedException(string? message) : base(message)
	{
	}

	public MessageFailedException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}