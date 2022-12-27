using Autofac;
using Autofac.Extensions.DependencyInjection;
using NetModule2_2.BAL;
using NetModule2_2.DAL;
using NetModule2_2.EAL;
using NetModule2_2.NAL;
using Okta.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule<DalModule>();
    builder.RegisterModule<BalModule>();
    builder.RegisterModule<EalModule>();
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorHandlingAttribute>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Okta", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header
    });
});
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
        options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
        options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
    })
    .AddOktaWebApi(new OktaWebApiOptions
    {
        OktaDomain = builder.Configuration["Okta:OktaDomain"],
        AuthorizationServerId = builder.Configuration["Okta:AuthorizationServerId"],
        Audience = builder.Configuration["Okta:Audience"]
    });
builder.Services.AddAuthorization(options =>
{
    var scopeClaim = "http://schemas.microsoft.com/identity/claims/scope";
    options.AddPolicy(nameof(ManagerAccessAttribute), policy => policy.RequireAuthenticatedUser().RequireClaim(scopeClaim, "catalog.manager"));
    options.AddPolicy(nameof(BuyerAccessAttribute), policy => policy.RequireAuthenticatedUser().RequireClaim(scopeClaim, "catalog.buyer"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
