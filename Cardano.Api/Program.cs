using Cardano.Repositories;
using Cardano.Logic;
using Serilog;
using Cardano.Domain.Dto;
using Cardano.Logic.Mappers;
using Cardano.Domain.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<HttpClient>(_ => new HttpClient() { BaseAddress = new Uri(builder.Configuration.GetValue<string>("GleifBaseAddress")) });
builder.Services.ServiceCollectionForRepositories();
builder.Services.ServiceCollectionForServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//logs
builder.Logging.AddSerilog(new LoggerConfiguration().WriteTo.File(".\\Logs\\log-.log", rollingInterval: RollingInterval.Day).CreateLogger());

//register mapping
CsvFileService.Mapping.Add(typeof(TransactionDto), new TransactionDtoMap());
CsvFileService.Mapping.Add(typeof(Transaction), new TransactionMap());

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
