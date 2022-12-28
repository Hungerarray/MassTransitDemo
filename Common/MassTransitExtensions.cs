using Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class MassTransitExtensions
{
	public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
	{
		services.AddOptions<MassTransitHostOptions>()
			.Configure(options => options.WaitUntilStarted = true);

		services.AddMassTransit(config => {
			config.AddConsumersFromNamespaceContaining(typeof(HelloWorldConsumer));
			config.SetKebabCaseEndpointNameFormatter();

			config.UsingRabbitMq((ctx, cfg) => {
				cfg.ConfigureEndpoints(ctx);
				cfg.Host("localhost");
			});
		});

		return services;
	}

	public static IServiceCollection ConfigureMassTransitCore(this IServiceCollection services)
	{
		services.AddMassTransit(config => config.UsingRabbitMq());
		return services;
	}
}