using MassTransit;
using Messages;

namespace WebApi.BackgroundWorker;

public class HelloPublishWorker : BackgroundService
{
	private readonly IBus _bus;

	public HelloPublishWorker(IBus bus)
	{
		_bus = bus;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			// var endpoint = await _bus.GetSendEndpoint(new Uri("queue:hello-world"));
			// await endpoint.Send(new HelloWorld
			// 	{
			// 		Value = new Random().Next(),
			// 	}, stoppingToken
			// );
            await _bus.Publish(new HelloWorld{
                Value = new Random().Next()
            }, stoppingToken);

			await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
		}
	}
}