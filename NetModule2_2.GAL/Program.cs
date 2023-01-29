using Autofac;
using Autofac.Extensions.DependencyInjection;
using NetModule2_2.BAL;
using NetModule2_2.DAL;
using NetModule2_2.EAL;
using NetModule2_2.GAL.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule<DalModule>();
    builder.RegisterModule<BalModule>();
    builder.RegisterModule<EalModule>();
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
var app = builder.Build();

app.MapGraphQL();

app.Run();
