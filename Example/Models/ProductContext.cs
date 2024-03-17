using Microsoft.EntityFrameworkCore;

namespace Example.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductsStorage> ProductsStorage { get; set;}
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=WebStore;Username=postgres;Password=example");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(x => x.Id).ValueGeneratedOnAdd(); // Указание, что Id будет генерироваться автоматически
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(e => e.Name)
                      .HasColumnName("ProductName")
                      .HasMaxLength(255)
                      .IsRequired();
                entity.Property(e => e.Description)
                    .HasColumnName("Deccription")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(e => e.Cost)
                    .HasColumnName("Cost")
                    .IsRequired();
                entity.HasOne(x => x.ProductCategory)
                    .WithMany(c => c.Products)
                    .HasForeignKey(x => x.CategoryId) // Изменение внешнего ключа на CategoryId
                    .HasConstraintName("CategoryToProduct");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");
                entity.Property(x => x.Id).ValueGeneratedOnAdd(); // Указание, что Id будет генерироваться автоматически
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Name).IsUnique();
                entity.Property(e => e.Name)
                    .HasColumnName("CategoryName")
                    .HasMaxLength(255)
                    .IsRequired();
            });

            modelBuilder.Entity<ProductsStorage>(entity =>
            {
                entity.ToTable("Sorage");
                entity.Property(x => x.Id).ValueGeneratedOnAdd(); // Указание, что Id будет генерироваться автоматически
                entity.HasKey(x => x.Id).HasName("ProductsStorageId");
                entity.Property(entity => entity.Name)
                    .HasColumnName("ProductsStorageName");
                entity.Property(e => e.Amount)
                    .HasColumnName("ProductAmount");
                entity.HasMany(x => x.Products)
                    .WithMany(m => m.ProductsStorage)
                    .UsingEntity(j => j.ToTable("ProductsStorage"));
            });
        }
    }
}
