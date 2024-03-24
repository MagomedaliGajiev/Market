using GraphQl.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<StorageEntity> Storages { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; } // Добавленный DbSet для ProductStock

        private string _connectionString;

        public AppDbContext() { }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Конфигурация для ProductEntity
            modelBuilder.Entity<ProductEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Cost).IsRequired();
                entity.HasOne(x => x.Category).WithMany(x => x.Products);
                entity.HasOne(x => x.Storage).WithMany(x => x.Products);
            });

            // Конфигурация для CategoryEntity
            modelBuilder.Entity<CategoryEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(255).IsRequired();
            });

            // Конфигурация для StorageEntity
            modelBuilder.Entity<StorageEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(255).IsRequired();
            });

            // Конфигурация для ProductStock
            modelBuilder.Entity<ProductStock>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
                entity.HasOne(x => x.Storage).WithMany().HasForeignKey(x => x.StorageId);
                entity.Property(x => x.Quantity).IsRequired();
            });
        }
    }
}