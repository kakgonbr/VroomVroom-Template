
using Microsoft.EntityFrameworkCore;
using VroomVroom.Server.Data;

namespace VroomVroom.Server
{
    public class Program
    {
        internal static bool doSomething()
        {
            return true;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Services.AddDbContext<RentalContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECTION")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
