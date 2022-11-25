using VetService.Domain.Managers;
using VetService.Domain.Repositories;
using VetService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VetService.Domain.RepositoriesEFCore;

namespace AppointmentService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<VetServiceDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IManagerDoctor, ManagerDoctor>();
            builder.Services.AddScoped<IManagerPrescription, ManagerPrescription>();
            builder.Services.AddScoped<IManagerSymptom, ManagerSymptom>();
            builder.Services.AddScoped<IManagerTest, ManagerTest>();
            builder.Services.AddScoped<IManagerVital, ManagerVital>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            builder.Services.AddScoped<ISymptomRepository, SymptomRepository>();
            builder.Services.AddScoped<ITestRepository, TestRepository>();
            builder.Services.AddScoped<IVitalRepository, VitalRepository>();
            builder.Services.AddScoped<VetServiceDbContext, VetServiceDbContext>();
            builder.Services.AddScoped<IMemoryCache, MemoryCache>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(p => p.AddPolicy("corsPolicy", build =>
            {
                build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("corsPolicy");


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}