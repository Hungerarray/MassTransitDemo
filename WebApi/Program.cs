using Common;
using Serilog;
using WebApi.BackgroundWorker;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, cfg) => {
    cfg.WriteTo
        .Console();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureMassTransit();
// builder.Services.ConfigureMassTransitCore();

// builder.Services.ConfigureBackgroundWorkers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
