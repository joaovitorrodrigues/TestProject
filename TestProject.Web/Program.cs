using Microsoft.EntityFrameworkCore;
using TestProject.Infrastructure.Context;
using TestProject.Domain.Entities;
using System.Reflection;
using MongoDB.Bson;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR;

namespace TestProject.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();


            var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
            builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

            builder.Services.AddDbContext<TestProjectDbContext>(options => options.UseMongoDB(mongoDBSettings.AtlasURI ?? "", mongoDBSettings.DatabaseName ?? ""));
            builder.Services.AddRepositories();

            builder.Services.AddIdentity<User, Role>().AddMongoDbStores<User, Role, Guid>(
                mongoDBSettings.AtlasURI, mongoDBSettings.DatabaseName);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IBus>(provider =>
            {
                return RabbitHutch.CreateBus("host=localhost");
            });

            var serviceProvider = builder.Services.BuildServiceProvider();
            var bus = serviceProvider.GetService<IBus>();
            bus.PubSub.SubscribeAsync<TextMessage>("test",async message =>
            {
                var hubContext = serviceProvider.GetService<IHubContext<NotificationHub>>();
                Console.WriteLine(message.Text);                
                await hubContext.Clients.All.SendAsync("ReceiveMessage", message.Text);
                
                
            });
            builder.Services.AddRepositories();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationHub");
            });







            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
            app.MapRazorPages();
        }
    }
}