using Serilog;
using Neredekal.Application.Logging;
using Neredekal.UserAPI.Service.Concretes;
using Neredekal.UserAPI.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

new ConfigureLogging().Set();
builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUserService, UserService>();

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