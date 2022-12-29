using Consumers.Exceptions;
using MassTransit;
using Messages;
using Microsoft.Extensions.Logging;

namespace Consumers;

public class MessageWithPassRateConsumer : IConsumer<MessageWithPassRate>
{
	private readonly ILogger logger;

	public MessageWithPassRateConsumer(ILogger<MessageWithPassRateConsumer> logger)
	{
		this.logger = logger;
	}

	public Task Consume(ConsumeContext<MessageWithPassRate> context)
	{
		logger.LogInformation("Got message with Id: {Id}", context.Message.Id);
		double roll = new Random().NextDouble();
		if (roll > context.Message.Value)
		{
			logger.LogError("Message Failed the die roll: {Id}", context.Message.Id);
			throw new MessageFailedException();
		}

		return Task.CompletedTask;
	}
}

public class MessageWithPassRateConsumerDefinition :
	ConsumerDefinition<MessageWithPassRateConsumer>
{
	protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<MessageWithPassRateConsumer> consumerConfigurator)
	{
		endpointConfigurator.UseMessageRetry(
			cfg => cfg.Immediate(2)
		);
		endpointConfigurator.UseDelayedRedelivery(
			cfg => cfg.Intervals(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2))
		);
		endpointConfigurator.UseInMemoryOutbox();
	}
}