using System.Reflection;
using Consumers;
using MassTransit;

namespace WebApi;

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
}