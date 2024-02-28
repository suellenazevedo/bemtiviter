using bemtiviter.Data;
using bemtiviter.Validator;
using bemtiviter.Service;
using bemtiviter.Service.Implements;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using bemtiviter.DTOs;

namespace bemtiviter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Controller Class
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                }
            );

            // DB Connection 
            if (builder.Configuration["Environment:Start"] == "PROD")
            {
                // PostgresSQL connection - Cloud

                builder.Configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("secrets.json");

                var connectionString = builder.Configuration
               .GetConnectionString("ProdConnection");

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString)
                );
            }
            else
            {
                // SQL Server connection - Localhost
                var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection");

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString)
                );
            }

            // Entities Validation
            builder.Services.AddTransient<IValidator<PostagemDTO>, PostagemValidator>();
            builder.Services.AddTransient<IValidator<TemaDTO>, TemaValidator>();
            builder.Services.AddTransient<IValidator<UsuarioDTO>, UsuarioValidator>();

            // Class and Interfaces Service Registration
            builder.Services.AddScoped<IPostagemService, PostagemService>();
            builder.Services.AddScoped<ITemaService, TemaService>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //Registrar o Swagger
            builder.Services.AddSwaggerGen(options =>
            {

                //Personalizar a Págna inicial do Swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bemtiviter",
                    Description = "Bemtiviter, a sua rede social para você bemtivitar seus pensamentos",
                    Contact = new OpenApiContact
                    {
                        Name = "Suellen Azevedo",
                        Email = "suellen.azevedo13@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/suellen-azevedo/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Github",
                        Url = new Uri("https://github.com/suellenazevedo")
                    }
                });
            });

                //  CORS configuration
                builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

                var app = builder.Build();

                // Creating Database and Tables
                using (var scope = app.Services.CreateAsyncScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    dbContext.Database.EnsureCreated();
                }

                app.UseDeveloperExceptionPage();

            // Swagger Como Página Inicial - Nuvem

            if (app.Environment.IsProduction())
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bemtiviter - v1");
                    options.RoutePrefix = string.Empty;
                });
            }


            app.UseCors("MyPolicy");

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
        }
    }


}