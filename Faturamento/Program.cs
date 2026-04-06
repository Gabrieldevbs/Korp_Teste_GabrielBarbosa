using Faturamento.Application.Serviços;
using Faturamento.Domain.Interfaces.Repositórios;
using Faturamento.Domain.Interfaces.Serviços;
using Faturamento.Infra.Data;
using Faturamento.Infra.Data.Repositórios;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(ConnectionString)
);

builder.Services.AddScoped<INotaFiscalRepositório, NotaFiscalRepositório>();
builder.Services.AddScoped<INotaFiscalServiços, NotaFiscalServiços>();
builder.Services.AddScoped<IProdutosNotaFiscalRepositório, ProdutosNotaFiscalRepositório>();
builder.Services.AddScoped<IProdutoNotaFiscalServiços, ProdutoNotaFiscalServiços>();
builder.Services.AddScoped<PdfService>();

builder.Services.AddHttpClient<EstoqueClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7246"); // API de estoque
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ArgumentException:
                context.Response.StatusCode = 400;
                break;

            case UnauthorizedAccessException:
                context.Response.StatusCode = 401;
                break;

            case KeyNotFoundException:
                context.Response.StatusCode = 404;
                break;

            default:
                context.Response.StatusCode = 500;
                break;
        }

        await context.Response.WriteAsJsonAsync(new
        {
            error = exception?.Message
        });
    });
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
