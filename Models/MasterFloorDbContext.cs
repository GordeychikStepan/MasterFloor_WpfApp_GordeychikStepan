using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MasterFloor_WpfApp.Models;

public partial class MasterFloorDbContext : DbContext
{
    public MasterFloorDbContext()
    {
    }

    public MasterFloorDbContext(DbContextOptions<MasterFloorDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<PartnerType> PartnerTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=STEPAN\\SQLEXPRESS;Database=MasterFloor_db;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.ToTable("MaterialType");

            entity.Property(e => e.MaterialTypeId).HasColumnName("MaterialTypeID");
            entity.Property(e => e.MaterialType1)
                .HasMaxLength(50)
                .HasColumnName("MaterialType");
            entity.Property(e => e.PercentDamage).HasColumnType("decimal(4, 2)");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.Property(e => e.Ceo)
                .HasMaxLength(100)
                .HasColumnName("CEO");
            entity.Property(e => e.Inn)
                .HasMaxLength(50)
                .HasColumnName("INN");
            entity.Property(e => e.PartnerAddress).HasMaxLength(100);
            entity.Property(e => e.PartnerEmail).HasMaxLength(50);
            entity.Property(e => e.PartnerName).HasMaxLength(50);
            entity.Property(e => e.PartnerPhone).HasMaxLength(50);

            entity.HasOne(d => d.PartnerType).WithMany(p => p.Partners)
                .HasForeignKey(d => d.PartnerTypeId)
                .HasConstraintName("FK_Partners_PartnerType");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.PartnterProductsId);

            entity.Property(e => e.PartnerId).HasColumnName("PartnerID");

            entity.HasOne(d => d.Partner).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.PartnerId)
                .HasConstraintName("FK_PartnerProducts_Partners");

            entity.HasOne(d => d.Product).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_PartnerProducts_Products");
        });

        modelBuilder.Entity<PartnerType>(entity =>
        {
            entity.ToTable("PartnerType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Articul).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(100);

            entity.HasOne(d => d.ProductMaterial).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductMaterialId)
                .HasConstraintName("FK_Products_MaterialType");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK_Products_ProductType");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("ProductType");

            entity.Property(e => e.Coefficient).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.ProductType1)
                .HasMaxLength(50)
                .HasColumnName("ProductType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
