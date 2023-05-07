using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace QLDC.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<MonHoc> DanhSachMonHoc { get; set; }
        public DbSet<GiangVien> DanhSachGiangVien { get; set; }
        public DbSet<DeCuong> DanhSachDeCuong { get; set; }
        public DbSet<VanPhongKhoa> DanhSachVanPhongKhoa { get; set; }
        public DbSet<TaiKhoan> DanhSachTaiKhoan { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonHoc>().ToTable("MonHoc");
            modelBuilder.Entity<GiangVien>().ToTable("GiangVien");
            modelBuilder.Entity<DeCuong>().ToTable("DeCuong");
            modelBuilder.Entity<VanPhongKhoa>().ToTable("VanPhongKhoa");
            modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
        }
    }
}
