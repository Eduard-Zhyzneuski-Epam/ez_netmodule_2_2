// See https://aka.ms/new-console-template for more information
using Autofac;
using NetModule2_2.BAL;
using NetModule2_2.DAL;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var builder = new ContainerBuilder();
builder.RegisterModule<BalModule>();
builder.RegisterModule<DalModule>();
var container = builder.Build();

var itemService = container.Resolve<IItemService>();
var items = itemService.List();
Console.WriteLine(JsonSerializer.Serialize(items));

var categoryService = container.Resolve<ICategoryService>();
var categories = categoryService.List();
Console.WriteLine(JsonSerializer.Serialize(categories));