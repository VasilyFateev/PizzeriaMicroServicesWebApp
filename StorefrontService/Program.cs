using DatabasesAccess.AssortmentDb;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using StorefrontService.Consumers;
using StorefrontService.Core;
using StorefrontService.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AssortmentContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("AssortmentDb");
	options.UseNpgsql(connectionString, b => b.MigrationsAssembly("DatabasesAccess"));
});

builder.Services.AddScoped<IAssortmentListBuilder, AssortmentListBuilderV1>();
builder.Services.AddScoped<IProductItemConfiguratorBuilder, ProductItemConfiguratorBuilderV1>();

builder.Services.AddScoped<AssortmentListConsumer>();
builder.Services.AddScoped<ProductInfoConsumer>();

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<AssortmentListConsumer>();
	x.AddConsumer<ProductInfoConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host("rabbitmq", "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		cfg.ReceiveEndpoint("assortment-list-queue", e =>
		{
			e.ConfigureConsumer<AssortmentListConsumer>(context);
		});

		cfg.ReceiveEndpoint("product-item-configuration-queue", e =>
		{
			e.ConfigureConsumer<ProductInfoConsumer>(context);
		});
	});
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AssortmentContext>();
	db.Database.Migrate();
}

app.Run();