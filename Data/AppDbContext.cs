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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Category>().ToTable("tCategory");
            modelBuilder.Entity<Product>().ToTable("tProduct");
            modelBuilder.Entity<Image>().ToTable("tImage");
            modelBuilder.Entity<User>().ToTable("tUser");


            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(c => c.Name)
                      .HasMaxLength(100);

                entity.Property(c => c.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                // 1 - n: 1 Category -> n Product
                entity.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa loại => xóa sản phẩm
            });


            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(p => p.Name)
                      .HasMaxLength(100);

                entity.Property(p => p.CategoryId)
                      .HasMaxLength(50);

                entity.Property(p => p.Price)
                      .HasColumnType("BIGINT");

                entity.Property(p => p.Avatar)
                      .HasMaxLength(200);

                entity.Property(p => p.Description)
                      .HasColumnType("NVARCHAR(MAX)");

                entity.Property(p => p.Information)
                      .HasColumnType("NVARCHAR(MAX)");

                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                // 1 - n: 1 Product -> n Image
                entity.HasMany(p => p.Images)
                      .WithOne(i => i.SanPham)
                      .HasForeignKey(i => i.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // Image
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(i => i.ProductId)
                      .HasMaxLength(50);

                entity.Property(i => i.ImageFileName)
                      .HasMaxLength(100);

                entity.Property(i => i.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");
            });
            

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(u => u.Username)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.Password)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(u => u.Role)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(u => u.CreatedAt)
                      .HasDefaultValueSql("GETDATE()"); 
            });
        }
    }
}
