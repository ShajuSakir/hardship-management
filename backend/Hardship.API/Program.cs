using FluentValidation;
using Hardship.Application.Abstractions;
using Hardship.Application.Common;
using Hardship.Application.Common.Exceptions;
using Hardship.Application.Hardships.Commands;
using Hardship.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core with SQLite.
builder.Services.AddDbContext<HardshipDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(
    provider => provider.GetRequiredService<HardshipDbContext>());

// Register MediatR handlers from Application layer.
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateHardshipCommand).Assembly));

// Automatically register FluentValidation validators.
builder.Services.AddValidatorsFromAssemblyContaining<CreateHardshipValidator>();

// Add validation pipeline so commands are validated before reaching handlers.
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
    typeof(ValidationBehaviour<,>));

// Add validation pipeline so commands are validated before reaching handlers.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new
            {
                title = "Validation failed",
                status = 400,
                errors = errors
            };

            return new BadRequestObjectResult(response);
        };
    });

// Load allowed CORS origins from configuration.
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Centralized exception handling using ProblemDetails.
// Maps domain/application exceptions to proper HTTP responses.
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        var exception = context.HttpContext.Features
            .Get<IExceptionHandlerFeature>()?.Error;

        if (exception == null)
            return;

        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<Program>>();

        logger.LogError(exception, "Unhandled exception");

        switch (exception)
        {
            case NotFoundException notFound:
                context.ProblemDetails.Status = StatusCodes.Status404NotFound;
                context.ProblemDetails.Title = "Resource not found";
                context.ProblemDetails.Detail = notFound.Message;
                break;

            case FluentValidation.ValidationException validation:
                context.ProblemDetails.Status = StatusCodes.Status400BadRequest;
                context.ProblemDetails.Title = "Validation failed";
                context.ProblemDetails.Extensions["errors"] =
                    validation.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();
                break;

            default:
                context.ProblemDetails.Status = StatusCodes.Status500InternalServerError;
                context.ProblemDetails.Title = "An unexpected error occurred";
                context.ProblemDetails.Detail = "Something went wrong. Please try again later.";
                break;
        }
    };
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
public partial class Program { }