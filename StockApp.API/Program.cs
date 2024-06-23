using StockApp.Application.Services;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Repositories;
using StockApp.Infra.IoC;
using Microsoft.Extensions.Caching.StackExchangeRedis; // Adicione esta linha
using Microsoft.Extensions.Caching.Distributed; // Esta linha também pode ser necessária

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IRecommendationService, RecommendationService>();
        builder.Services.AddInfrastructureAPI(builder.Configuration);
        builder.Services.AddInfrastructureJWT(builder.Configuration);
        builder.Services.AddInfrastructureSwagger();

        // Configura o cache Redis
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
