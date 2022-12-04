using Autofac;
using Autofac.Extensions.DependencyInjection;
using NetModule2_2.BAL;
using NetModule2_2.DAL;
using NetModule2_2.EAL;
using NetModule2_2.NAL;

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
builder.Services.AddSwaggerGen();

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
