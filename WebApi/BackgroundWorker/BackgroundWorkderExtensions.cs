namespace WebApi.BackgroundWorker;

public static class BackgroundWorkerExtensions
{
	public static IServiceCollection ConfigureBackgroundWorkers(this IServiceCollection services)
	{
		services.AddHostedService<HelloWorker>();

		return services;
	}
}