using Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class MassTransitExtensions
{
	private const string RabbitMqConnection = "localhost";
	public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
	{
		services.AddOptions<MassTransitHostOptions>()
			.Configure(options => options.WaitUntilStarted = true);

		services.AddMassTransit(config => {
			config.AddConsumersFromNamespaceContaining(typeof(HelloWorldConsumer));
			config.SetKebabCaseEndpointNameFormatter();

			config.UsingRabbitMq((ctx, cfg) => {
				// cfg.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2)));
				// cfg.UseMessageRetry(r => r.Immediate(2));
				// cfg.UseInMemoryOutbox();
				cfg.Host(RabbitMqConnection);

				cfg.UsePrometheusMetrics(serviceName: "MassTransit");
				cfg.ConfigureEndpoints(ctx);
			});
			// config.UsingInMemory();
		});

		return services;
	}

	public static IServiceCollection ConfigureMassTransitCore(this IServiceCollection services)
	{
		services.AddMassTransit(config => config.UsingRabbitMq((_, cfg) => cfg.Host(RabbitMqConnection)));
		return services;
	}
}