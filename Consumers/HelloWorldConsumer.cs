using MassTransit;
using Messages;
using Microsoft.Extensions.Logging;

namespace Consumers;

public class HelloWorldConsumer : IConsumer<HelloWorld>
{
	private readonly ILogger logger;

	public HelloWorldConsumer(ILogger<HelloWorldConsumer> logger)
	{
		this.logger = logger;
	}

	public Task Consume(ConsumeContext<HelloWorld> context)
	{
		logger.LogInformation("Received message with value: {Value}", context.Message.Value);

		return Task.CompletedTask;
	}
}
