using DatabasesAccess.AccountDb;
using IdentityService.Consumers;
using IdentityService.Interfaces;
using IdentityService.ServiceComponents;
using IdentityService.Utils;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddDbContext<AccountContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("AccountDb");
	options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DatabasesAccess"));
});

builder.Services.AddScoped<IAuthorisationProvider, DefaultAuthorisationService>();
builder.Services.AddScoped<IRegistrationProvider, DefaultRegistrateService>();
builder.Services.AddScoped<IJwtTokenGenerator, DefaultJwtTokenGenerator>();

builder.Services.AddScoped<PingConsumer>();
builder.Services.AddScoped<AuthConsumer>();
builder.Services.AddScoped<RegConsumer>();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<PingConsumer>();
	x.AddConsumer<AuthConsumer>();
	x.AddConsumer<RegConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("rabbitmq", "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		cfg.ReceiveEndpoint("ping-queue", e =>
		{
			e.ConfigureConsumer<PingConsumer>(context);
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

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AccountContext>();
	db.Database.Migrate();
}

app.Run();
