using MqttExample.Broker;
using MQTTnet.AspNetCore.Extensions;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseKestrel(options => 
        {
            options.ListenAnyIP(1883, listenOptions => listenOptions.UseMqtt());
        });
                    
        webBuilder.UseStartup<Startup>();
    });

var app = builder.Build();

app.Run();
