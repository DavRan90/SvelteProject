using EventsApi.src.Data;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using MinimalApi.src.Events;
using EventsApi.Events;
using Scalar.AspNetCore;
using MinimalApi.src.EventsAlt;


namespace MinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<NpgsqlConnection>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                return new NpgsqlConnection(
                    config.GetConnectionString("Postgres")
                );
            });

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
            );

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            app.UseCors();

            app.MapGet("/db-test", async (NpgsqlConnection conn) =>
            {
                await conn.OpenAsync();

                using var cmd = new NpgsqlCommand("SELECT 1", conn);
                var result = await cmd.ExecuteScalarAsync();

                return Results.Ok(new { result });
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapScalarApiReference();
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.MapGroup("/events").MapEventEndpoints();
            app.MapGroup("/events").MapEventEndpointsAlt();
            app.MapGroup("/categories").MapCategoryEndpoints();

            //app.MapGetEvents();
            //app.MapDeleteEvent();
            //app.MapUpdateEvent();

            app.Run();
        }
    }
}
