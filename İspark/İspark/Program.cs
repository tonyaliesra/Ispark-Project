using Ýspark.Extensions;
using Ýspark.Business;
using Ýspark.Datas;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentationServices(builder.Configuration);
builder.Services.AddBusinessServices();
builder.Services.AddDataServices(builder.Configuration);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.ConfigurePipeline(app.Environment);

app.Run();