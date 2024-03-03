using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserManager.Models;

public partial class NeaRentContext : DbContext
{
    public NeaRentContext()
    {
    }

    public NeaRentContext(DbContextOptions<NeaRentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressCountry> AddressCountries { get; set; }

    public virtual DbSet<ContactTypeAddress> ContactTypeAddresses { get; set; }

    public virtual DbSet<ContactTypeEmail> ContactTypeEmails { get; set; }

    public virtual DbSet<ContactTypePhone> ContactTypePhones { get; set; }

    public virtual DbSet<SysIdentityType> SysIdentityTypes { get; set; }

    public virtual DbSet<SysUserType> SysUserTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserCategoryPreference> UserCategoryPreferences { get; set; }

    public virtual DbSet<UserEmail> UserEmails { get; set; }

    public virtual DbSet<UserPhone> UserPhones { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<UserStatusHistory> UserStatusHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Address_Country_1");

            entity.ToTable("Address_Country");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CurrencyName).HasMaxLength(50);
            entity.Property(e => e.CurrencySymbol).HasMaxLength(10);
            entity.Property(e => e.InitialsLong)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.InitialsShort)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.PhoneCode).HasMaxLength(20);
            entity.Property(e => e.PostalCodeRegex).HasMaxLength(200);
        });

        modelBuilder.Entity<ContactTypeAddress>(entity =>
        {
            entity.ToTable("ContactType_Address");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ContactTypeEmail>(entity =>
        {
            entity.ToTable("ContactType_Email");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ContactTypePhone>(entity =>
        {
            entity.ToTable("ContactType_Phone");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SysIdentityType>(entity =>
        {
            entity.ToTable("Sys_IdentityType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.IdentityName).HasMaxLength(250);
        });

        modelBuilder.Entity<SysUserType>(entity =>
        {
            entity.ToTable("Sys_UserType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Activer).HasDefaultValue(true);
            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.AzureObjectId);

            entity.ToTable("User");

            entity.Property(e => e.AzureObjectId)
                .ValueGeneratedNever()
                .HasColumnName("AzureObjectID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Joined).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Surname).HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.ToTable("User_Address");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.AddressStreetId).HasColumnName("AddressStreetID");
            entity.Property(e => e.AddressTypeId).HasColumnName("AddressTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AddressType).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Address_ContactType_Address");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Address_User");
        });

        modelBuilder.Entity<UserCategoryPreference>(entity =>
        {
            entity.ToTable("User_CategoryPreferences");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.AzureObjectId).HasColumnName("AzureObjectID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.HasOne(d => d.AzureObject).WithMany(p => p.UserCategoryPreferences)
                .HasForeignKey(d => d.AzureObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_CategoryPreferences_User");
        });

        modelBuilder.Entity<UserEmail>(entity =>
        {
            entity.ToTable("User_Email");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.EmailTypeNavigation).WithMany(p => p.UserEmails)
                .HasForeignKey(d => d.EmailType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Email_ContactType_Email");

            entity.HasOne(d => d.User).WithMany(p => p.UserEmails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Email_User");
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.ToTable("User_Phone");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.PhoneTypeId).HasColumnName("PhoneTypeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PhoneType).WithMany(p => p.UserPhones)
                .HasForeignKey(d => d.PhoneTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Phone_ContactType_Phone");

            entity.HasOne(d => d.User).WithMany(p => p.UserPhones)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Phone_User");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.ToTable("User_Registration");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Address).HasMaxLength(1024);
            entity.Property(e => e.AddressLat).HasColumnType("decimal(18, 8)");
            entity.Property(e => e.AddressLng).HasColumnType("decimal(18, 8)");
            entity.Property(e => e.Area).HasMaxLength(250);
            entity.Property(e => e.AzureObjectId).HasColumnName("AzureObjectID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.Distance).HasDefaultValue(10);
            entity.Property(e => e.District).HasMaxLength(250);
            entity.Property(e => e.Flat).HasMaxLength(250);
            entity.Property(e => e.LastStep).HasDefaultValue(1);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.PhoneCountryId).HasColumnName("PhoneCountryID");
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            entity.Property(e => e.PostCode).HasMaxLength(250);
            entity.Property(e => e.Province).HasMaxLength(250);
            entity.Property(e => e.Street).HasMaxLength(250);
            entity.Property(e => e.Surname).HasMaxLength(250);
            entity.Property(e => e.VettingEnd).HasColumnType("datetime");
            entity.Property(e => e.VettingStart)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.ToTable("User_Status");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserStatusHistory>(entity =>
        {
            entity.ToTable("User_StatusHistory");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Note).HasMaxLength(200);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Status).WithMany(p => p.UserStatusHistories)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_StatusHistory_User_Status");

            entity.HasOne(d => d.User).WithMany(p => p.UserStatusHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_StatusHistory_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
