using Microsoft.EntityFrameworkCore;
using QLDC.Models;

namespace QLDC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllersWithViews();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapAreaControllerRoute(
                name: "MyAreaProducts",
                areaName: "VPKhoa",
                pattern: "VPKhoa/{controller=DeCuong}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "MyAreaServices",
                areaName: "VPKhoa",
                pattern: "VPKhoa/{controller=GiangVien}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "VPKhoa",
                areaName: "VPKhoa",
                pattern: "VPKhoa/{controller=MonHoc}/{action=Index}/{id?}");

            app.MapAreaControllerRoute(
                name: "MyAreaServices",
                areaName: "VPKhoa",
                pattern: "VPKhoa/{controller=VanPhongKhoa}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "VPKhoa",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


            app.MapAreaControllerRoute(
                name: "MyAreaServices",
                areaName: "GVien",
                pattern: "GVien/{controller=DeCuong}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "GVien",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");


            app.Run();
        }
    }
}