
using AuthTwoFactorApi.Domain.Handlers;
using AuthTwoFactorApi.Domain.Interfaces;
using AuthTwoFactorApi.Domain.Services;
using AuthTwoFactorApi.Repository;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMailSenderHandler, MailSenderHandler>();
builder.Services.AddScoped<IAuthenticatorService, AuthenticatorService>();
builder.Services.AddScoped<IAuthenticatorRepository, AuthenticatorRepository>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMongoClient>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var mongoSettings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDB"));
    return new MongoClient(mongoSettings);
});

// Set the ServerApi field of the settings object to Stable API version 1
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
