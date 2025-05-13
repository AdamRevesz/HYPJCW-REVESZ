using Data;
using Data.ClassRepo;
using Data.Repo;
using Logic;
using Logic.Helper;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using System.Text;

namespace Backend_Feleves
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register the DbContext
            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BackendFeleves;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                options.UseLazyLoadingProxies();
            });

            // Register the Repository with the correct generic type
            builder.Services.AddTransient<IRepository<Video>, VideoRepo>();
            builder.Services.AddTransient<IRepository<Picture>, PictureRepo>();
            builder.Services.AddTransient<IRepository<Course>, CourseRepo>();
            builder.Services.AddTransient<IRepository<SalesItem>, SalesItemRepo>();
            builder.Services.AddTransient<IRepository<User>, UserRepo>();
            builder.Services.AddTransient<IRepository<Comments>, CommentRepo>();
            builder.Services.AddTransient<IRepository<Content>, ContentRepo>();

            builder.Services.AddTransient(typeof(RateLogic<>));

            builder.Services.AddTransient<DtoProvider>();
            builder.Services.AddTransient<CommentLogic>();
            builder.Services.AddTransient<UserLogic>();
            builder.Services.AddTransient<PictureLogic>();
            builder.Services.AddTransient<SalesItemLogic>();
            builder.Services.AddTransient<CourseLogic>();
            builder.Services.AddTransient<ContentLogic>();
            builder.Services.AddTransient<VideoLogic>();
            builder.Services.AddTransient<DimensionsLogic>();

            builder.Services.AddIdentity<User, IdentityRole>(
                option =>
                {
                    option.Password.RequireDigit = false;
                    option.Password.RequiredLength = 6;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireLowercase = false;
                }
            )
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<MainDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "artlounge.com",
                    ValidIssuer = "artlounge.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MegszencségeleníthetetlenségeskedéseitekértMegszencségeleníthetetlenségeskedéseitekértMegszencségeleníthetetlenségeskedéseitekért"))
                };
            });

            // Add CORS service
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend_Feleves_Artista", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
