using Common;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args);
host.UseSerilog((_, cfg) => {
	cfg.WriteTo
		.Console();
});
host.ConfigureServices(services => services.ConfigureMassTransit());

await host.Build().RunAsync();
