using API.ClientWebAppIdentityService;
using ClientAppUI.RequestSenders;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
var rabbitMqHost = builder.Configuration["RabbitMq:Host"];

builder.Logging.AddSimpleConsole(options =>
{
	options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
	options.SingleLine = true;
	options.UseUtcTimestamp = true;
}).SetMinimumLevel(LogLevel.Debug);

builder.Services.AddControllersWithViews(); 
builder.Services.AddScoped<PingRequestSenderService>();
builder.Services.AddScoped<AuthRequestSenderService>();
builder.Services.AddScoped<RegRequestSenderService>();

builder.Services.AddMassTransit(x =>
{
	x.AddRequestClient<AuthRequest>(new Uri("queue:auth-queue"));
	x.AddRequestClient<RegRequest>(new Uri("queue:reg-queue"));

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(rabbitMqHost, "/", h =>
		{
			h.Username("guest");
			h.Password("guest");
		});

		cfg.ConfigureEndpoints(context);
	});
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidIssuer = "Vessel",
			ValidateAudience = true,
			ValidAudience = "VesselPizzeria",
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qH3sOV3f0ZNfXD5eMb5JL+if2LXX4/IAhVxqMZ6Z5K3Q1+KnrXEMUmxOREMRb1lj"))
		};
	});

builder.Services.AddAntiforgery(options =>
{
	options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
		? CookieSecurePolicy.SameAsRequest
		: CookieSecurePolicy.Always;      
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();


app.Run();
