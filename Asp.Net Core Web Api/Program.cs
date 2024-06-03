using Asp.Net_Core_Web_Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Asp.Net_Core_Web_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
            builder.Services.AddDbContext<ContactsAPIDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiConnectionString")));
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
        }
    }
}