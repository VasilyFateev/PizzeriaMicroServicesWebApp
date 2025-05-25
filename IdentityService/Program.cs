using IdentityService.Consumers;
using IdentityService.Interfaces;
using IdentityService.ServiceComponents;
using IdentityService.Utils;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthorisationProvider, DefaultAuthorisationService>();
builder.Services.AddScoped<IRegistrationProvider, DefaultRegistrateService>();
builder.Services.AddScoped<IJwtTokenGenerator, DefaultJwtTokenGenerator>();

builder.Services.AddScoped<AuthConsumer>();
builder.Services.AddScoped<RegConsumer>();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<AuthConsumer>();
	x.AddConsumer<RegConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("rabbitmq", "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		cfg.ReceiveEndpoint("auth-queue", e =>
		{
			e.ConfigureConsumer<AuthConsumer>(context);
		});

		cfg.ReceiveEndpoint("reg-queue", e =>
		{
			e.ConfigureConsumer<RegConsumer>(context);
		});
	});
});

 var app = builder.Build();
app.Run();
