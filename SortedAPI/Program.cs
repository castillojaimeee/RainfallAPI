using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using Sorted.Application.Interface;
using Sorted.Application.Service;
using Sorted.Core.Entities;
using Sorted.Infrastructure.Data;
using SortedAPI.Middleware;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
    {
        Description = "Basic auth added to authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "basic",
        Type = SecuritySchemeType.Http
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
       {
         new OpenApiSecurityScheme
         {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Basic" }
         }, new List<string>()
       }
    });
});
builder.Services.AddScoped<IRainfallReadingRepository, RainfallReadingRepository>();
builder.Services.AddScoped<IRainfallReadingService, RainFallReadingService>();
builder.Services.AddControllers(options => options.OutputFormatters.RemoveType<StringOutputFormatter>());

builder.Services.AddMvcCore().ConfigureApiBehaviorOptions(options => {
    options.InvalidModelStateResponseFactory = (errorContext) =>
    {
        var errors = errorContext.ModelState.Values.SelectMany(e => e.Errors.Select(m => new
        {
            ErrorMessage = m.ErrorMessage
        })).ToList();
        var result = new ErrorResponse { Message = "Invalid Request" };
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

app.UseMiddleware<BasicAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
