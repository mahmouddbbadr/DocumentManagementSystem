using Application.IRepository;
using Application.IServices;
using Application.Services;
using DocumentManagementSystem.Domain.IRepository;
using Domain.Models;
using Infrasturcture.Data;
using Infrasturcture.Repositories;
using Infrasturcture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Presentaion
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DataContext>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDirectoryService, DirectoryService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();

            builder.Services.AddScoped<IGenericRepository<WorkSpace>, WorkSpaceRepository>();
            builder.Services.AddScoped<IGenericRepository<Domain.Models.Directory>, DirectoryRepository>();
            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,

                };

            });
            builder.Services.AddAuthorization();


            builder.Services.AddSwaggerGen(Options =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Auth Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                Options.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirment = new OpenApiSecurityRequirement { { securityScheme, new[] { "bearer" } } };
                Options.AddSecurityRequirement(securityRequirment);


            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
 
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowOrigins");
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();


            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "User"};
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            app.Run();
        }
    }
}