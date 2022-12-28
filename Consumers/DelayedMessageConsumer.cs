using MassTransit;
using Messages;
using Microsoft.Extensions.Logging;

namespace Consumers;

public class DelayedMessageConsumer : IConsumer<DelayedMessage>
{
	private readonly ILogger logger;

	public DelayedMessageConsumer(ILogger<DelayedMessageConsumer> logger)
	{
		this.logger = logger;
	}

	public async Task Consume(ConsumeContext<DelayedMessage> context)
	{
		logger.LogInformation("Delayed Message started: {Id}", context.Message.Id);
		await Task.Delay(context.Message.Seconds);
		logger.LogInformation("Delayed Message finished: {Id}", context.Message.Id);
	}
}