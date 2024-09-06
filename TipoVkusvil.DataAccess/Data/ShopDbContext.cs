using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TipoVkusvil.Core.Enums;
using TipoVkusvil.DataAccess.Configurations;
using TipoVkusvil.DataAccess.Entities;
using TipoVkusvil.Models;

namespace TipoVkusvil.DataAccess;

public class ShopDbContext : DbContext
{
    
    private readonly AuthorizationOptions _authorizationOptions;
    private readonly DbContextOptions<ShopDbContext> _options;

    public ShopDbContext(DbContextOptions<ShopDbContext> options, IOptions<AuthorizationOptions> authOptions) : base(options)
    {
        _authorizationOptions = authOptions.Value;
        _options = options;
    }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    
    public DbSet<RoleEntity> Roles { get; set; }    
    public DbSet<ShopCartEntity> ShopCarts { get; set; }
    public DbSet<CartItemEntity> CartItems { get; set; }
    public DbSet<CartItemFinishedEntity> CartItemsForOrders { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderCartEntity> OrderCarts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=VkusvilDb;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);

        modelBuilder.Entity<OrderCartEntity>(entity =>
        {
            entity.HasKey(oc => new { oc.OrderId, oc.CartItemFinishedId });

            entity.HasOne(oc => oc.Order)
                .WithMany(o => o.OrderCarts)
                .HasForeignKey(oc => oc.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oc => oc.CartItem)
                .WithMany(ci => ci.OrderCarts)
                .HasForeignKey(oc => oc.CartItemFinishedId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<CartItemFinishedEntity>(entity =>
        {
            entity.HasKey(ci => ci.CartItemId);

            entity.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict); 
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(o => o.OrderId);

            entity.Property(o => o.UserId);
            entity.Property(o => o.CreatedAt);
            entity.Property(o => o.Status);
            
            
        });
        
        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            
            entity.Property(e => e.CategoryName).IsRequired();
            entity.Property(e => e.UpperCategoryName).IsRequired();
            entity.Property(e => e.ImgURL).IsRequired();

            entity.HasMany(c => c.Products)
                .WithMany(p => p.Categories)
                .UsingEntity<ProductCategoryEntity>(
                    product => product
                        .HasOne(pc => pc.Product)
                        .WithMany(p => p.ProductCategories)
                        .HasForeignKey(pc => pc.ProductId),

                    category => category
                        .HasOne(pc => pc.Category)
                        .WithMany(c => c.ProductCategories)
                        .HasForeignKey(pc => pc.CategoryId)
                );
        });

        modelBuilder.Entity<CartItemEntity>(entity =>
        {
            entity.HasKey(ci => ci.CartItemId);

            entity.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId);
        });

        modelBuilder.Entity<ShopCartEntity>(entity =>
        {
            entity.HasKey(sc => sc.Id);
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(u => u.Id);
        });
        
        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.HasKey(e => e.ProductId);
        });
        
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authorizationOptions));
        
        base.OnModelCreating(modelBuilder);
    }
}
