using IdentityService.Consumers;
using IdentityService.Interfaces;
using IdentityService.ServiceComponents;
using IdentityService.Utils;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAuthorisationProvider, DefaultAuthorisationService>();
builder.Services.AddSingleton<IRegistrationProvider, DefaultRegistrateService>();
builder.Services.AddSingleton<IJwtTokenGenerator, DefaultJwtTokenGenerator>();
builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<AuthConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("localhost", "/", h =>
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
