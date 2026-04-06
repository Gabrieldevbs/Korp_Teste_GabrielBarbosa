using Microsoft.AspNetCore.Connections;
using Estoque;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Estoque.Interfaces;
using Estoque.Repositórios;
using Estoque.Serviços;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext <Estoque.ConnectionContext>(options =>
    options.UseNpgsql(ConnectionString)
);

builder.Services.AddScoped<IProdutos, RepositóriosProdutos>();
builder.Services.AddScoped<IProdutosServiços, ProdutosServiços>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod();
    }
    );
}
);

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
