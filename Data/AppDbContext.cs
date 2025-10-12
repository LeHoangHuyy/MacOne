using Macone.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Macone.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Loais { get; set; }
        public DbSet<Product> SanPhams { get; set; }
        public DbSet<Image> Anhs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().ToTable("tLoai");
            modelBuilder.Entity<Product>().ToTable("tSanPham");
            modelBuilder.Entity<Image>().ToTable("tAnh");
            modelBuilder.Entity<User>().ToTable("tUser");


            // Category (tLoai)
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.MaLoai);

                entity.Property(c => c.MaLoai)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(c => c.TenLoai)
                      .HasMaxLength(100)
                      .IsRequired();

                // 1 - n: 1 Category -> n Product
                entity.HasMany(c => c.SanPhams)
                      .WithOne(p => p.Loai)
                      .HasForeignKey(p => p.MaLoai)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa loại => xóa sản phẩm
            });


            // Product (tSanPham)
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.MaSp);

                entity.Property(p => p.MaSp)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(p => p.TenSp)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(p => p.MaLoai)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(p => p.Gia)
                      .HasColumnType("BIGINT")
                      .IsRequired();

                entity.Property(p => p.AnhDaiDien)
                      .HasMaxLength(200);

                entity.Property(p => p.MoTa)
                      .HasColumnType("NVARCHAR(MAX)");

                entity.Property(p => p.ThongTin)
                      .HasColumnType("NVARCHAR(MAX)");

                entity.Property(p => p.NgayTao)
                      .HasDefaultValueSql("GETDATE()");

                // 1 - n: 1 Product -> n Image
                entity.HasMany(p => p.Anhs)
                      .WithOne(i => i.SanPham)
                      .HasForeignKey(i => i.MaSp)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // Image (tAnh)
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(i => i.MaAnh);

                entity.Property(i => i.MaAnh)
                      .ValueGeneratedOnAdd();

                entity.Property(i => i.MaSp)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(i => i.TenFileAnh)
                      .HasMaxLength(100);
            });
            

            //  User (tUser)
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.MaUser);

                entity.Property(e => e.MaUser)
                      .ValueGeneratedOnAdd();

                entity.Property(u => u.TaiKhoan)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.MatKhau)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.ViTri)
                      .HasMaxLength(20)
                      .IsRequired();
            });
        }
    }
}
