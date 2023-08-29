
using StockExchange.Api.Extensions;
using StockExchange.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterFilters();
builder.Services.RegisterServices();
builder.Services.RegisterLogging();
builder.Services.RegisterDbContexts(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
