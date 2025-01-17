using Data;
using Logic;
using Logic.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Repository.ClassRepos;
namespace Backend_Feleves
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<IRepository<User>, UserRepo>();
            builder.Services.AddTransient<IRepository<Comments>, CommentRepo>();
            builder.Services.AddTransient<IRepository<Content>, ContentRepo>();
            builder.Services.AddTransient<IRepository<Course>, CourseRepo>();
            builder.Services.AddTransient<IRepository<Picture>, PictureRepo>();
            builder.Services.AddTransient<IRepository<SalesItem>, SalesItemRepo>();
            builder.Services.AddTransient<IRepository<Video>, VideoRepo>();
            builder.Services.AddScoped<ICommentLogic, CommentLogic>();
            builder.Services.AddScoped<IUserLogic, UserLogic>();
            builder.Services.AddScoped<IPictureLogic, PictureLogic>();
            builder.Services.AddScoped<IContentLogic, ContentLogic>();
            builder.Services.AddScoped<IVideoLogic, VideoLogic>();

            // Add services to the container.
            builder.Services.AddDbContext<MainDbContext>(options =>
            {
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BackendFeleves;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                options.UseLazyLoadingProxies();
            });


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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
