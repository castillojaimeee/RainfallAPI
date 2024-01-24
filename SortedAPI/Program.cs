using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Sorted.Application.Interface;
using Sorted.Application.Service;
using Sorted.Infrastructure.Data;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRainfallReadingRepository, RainfallReadingRepository>();
builder.Services.AddScoped<IRainfallReadingService, RainFallReadingService>();

builder.Services.AddMvcCore().ConfigureApiBehaviorOptions(options => {
    options.InvalidModelStateResponseFactory = (errorContext) =>
    {
        var errors = errorContext.ModelState.Values.SelectMany(e => e.Errors.Select(m => new
        {
            ErrorMessage = m.ErrorMessage
        })).ToList();
        var result = "Invalid Request";
        return new BadRequestObjectResult(result);
    };
});

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
