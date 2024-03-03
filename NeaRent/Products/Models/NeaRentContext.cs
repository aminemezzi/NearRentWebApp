using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Products.Models;

public partial class NeaRentContext : DbContext
{
    public NeaRentContext()
    {
    }

    public NeaRentContext(DbContextOptions<NeaRentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryShippingMap> CountryShippingMaps { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationType> LocationTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategoryMap> ProductCategoryMaps { get; set; }

    public virtual DbSet<ProductLocationMap> ProductLocationMaps { get; set; }

    public virtual DbSet<ProductRating> ProductRatings { get; set; }

    public virtual DbSet<ProductRentalMap> ProductRentalMaps { get; set; }

    public virtual DbSet<ProductReservation> ProductReservations { get; set; }

    public virtual DbSet<ProductShippingMap> ProductShippingMaps { get; set; }

    public virtual DbSet<RentalType> RentalTypes { get; set; }

    public virtual DbSet<ReservationType> ReservationTypes { get; set; }

    public virtual DbSet<ShippingType> ShippingTypes { get; set; }

    public virtual DbSet<UserStub> UserStubs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ImageName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Text).HasMaxLength(50);

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Category_Category");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<CountryShippingMap>(entity =>
        {
            entity.HasKey(e => new { e.CountryId, e.ShippingTypeId });

            entity.ToTable("Country_Shipping_Map");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.ShippingTypeId).HasColumnName("ShippingTypeID");
            entity.Property(e => e.Cost).HasColumnType("money");

            entity.HasOne(d => d.Country).WithMany(p => p.CountryShippingMaps)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_Shipping_Map_Country");

            entity.HasOne(d => d.ShippingType).WithMany(p => p.CountryShippingMaps)
                .HasForeignKey(d => d.ShippingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Country_Shipping_Map_ShippingType");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.HasOne(d => d.LocationTypeNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.LocationType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Location_Location_Type");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Location_Location");
        });

        modelBuilder.Entity<LocationType>(entity =>
        {
            entity.ToTable("Location_Type");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CancelDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(2048);
            entity.Property(e => e.ImageName).HasMaxLength(50);
            entity.Property(e => e.ListDate).HasColumnType("datetime");
            entity.Property(e => e.ListFromDate).HasColumnType("datetime");
            entity.Property(e => e.ListToDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ReservationTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ReservationType)
                .HasConstraintName("FK_Product_ReservationType");

            entity.HasOne(d => d.User).WithMany(p => p.Products)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_User");
        });

        modelBuilder.Entity<ProductCategoryMap>(entity =>
        {
            entity.HasKey(e => new { e.CategoryId, e.ProductId });

            entity.ToTable("Product_Category_Map");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategoryMaps)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category_Map_Category");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategoryMaps)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category_Map_Product");
        });

        modelBuilder.Entity<ProductLocationMap>(entity =>
        {
            entity.HasKey(e => new { e.LocationId, e.ProductId });

            entity.ToTable("Product_Location_Map");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.Location).WithMany(p => p.ProductLocationMaps)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Location_Map_Location");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductLocationMaps)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Location_Map_Product");
        });

        modelBuilder.Entity<ProductRating>(entity =>
        {
            entity.ToTable("Product_Rating");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Review).HasMaxLength(1000);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductRatings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Rating_Product");

            entity.HasOne(d => d.User).WithMany(p => p.ProductRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Rating_User_Stub");
        });

        modelBuilder.Entity<ProductRentalMap>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.RentalTypeId });

            entity.ToTable("Product_Rental_Map");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.RentalTypeId).HasColumnName("RentalTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductRentalMaps)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Rental_Map_Product");

            entity.HasOne(d => d.RentalType).WithMany(p => p.ProductRentalMaps)
                .HasForeignKey(d => d.RentalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Rental_Map_RentalType");
        });

        modelBuilder.Entity<ProductReservation>(entity =>
        {
            entity.ToTable("Product_Reservations");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductReservations)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Reservations_Product");

            entity.HasOne(d => d.User).WithMany(p => p.ProductReservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Reservations_User");
        });

        modelBuilder.Entity<ProductShippingMap>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.ShippingTypeId });

            entity.ToTable("Product_Shipping_Map");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ShippingTypeId).HasColumnName("ShippingTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductShippingMaps)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Shipping_Map_Product");

            entity.HasOne(d => d.ShippingType).WithMany(p => p.ProductShippingMaps)
                .HasForeignKey(d => d.ShippingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Shipping_Map_ShippingType");
        });

        modelBuilder.Entity<RentalType>(entity =>
        {
            entity.ToTable("RentalType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ReservationType>(entity =>
        {
            entity.ToTable("ReservationType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ShippingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Shipping_Type");

            entity.ToTable("ShippingType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserStub>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.ToTable("User_Stub");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
