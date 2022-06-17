using MqttExample.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureMqttClient(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Mqtt-Client App.");
    });
});

app.Run();
