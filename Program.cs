using Microsoft.OpenApi.Models;

namespace TicketSystemAPI.Controllers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adiciona serviços ao contêiner.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ticket System API", Version = "v1" });
            });

            var app = builder.Build();

            // Configura o pipeline HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticket System API v1"));
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}