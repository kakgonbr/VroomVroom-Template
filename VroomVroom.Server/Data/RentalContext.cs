using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VroomVroom.Server.Models;

namespace VroomVroom.Server.Data;

public partial class RentalContext : DbContext
{
    public RentalContext()
    {
    }

    public RentalContext(DbContextOptions<RentalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<DriverLicense> DriverLicenses { get; set; }

    public virtual DbSet<DriverLicenseType> DriverLicenseTypes { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleModel> VehicleModels { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECTION"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AED10BD61F1");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bookings_users");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bookings_vehicle");
        });

        modelBuilder.Entity<DriverLicense>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LicenseTypeId }).HasName("pk_drlc");

            entity.Property(e => e.HolderName).HasMaxLength(256);

            entity.HasOne(d => d.LicenseType).WithMany(p => p.DriverLicenses)
                .HasForeignKey(d => d.LicenseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drlc_drlct");

            entity.HasOne(d => d.User).WithMany(p => p.DriverLicenses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_drlc_users");
        });

        modelBuilder.Entity<DriverLicenseType>(entity =>
        {
            entity.HasKey(e => e.LicenseTypeId).HasName("PK__DriverLi__48F794F8616B73BE");

            entity.Property(e => e.LicenseTypeCode)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasMany(d => d.VehicleTypes).WithMany(p => p.LicenseTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "LicenseTypeToVehicleType",
                    r => r.HasOne<VehicleType>().WithMany()
                        .HasForeignKey("VehicleTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_lctvht_vht"),
                    l => l.HasOne<DriverLicenseType>().WithMany()
                        .HasForeignKey("LicenseTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_lctvht_lct"),
                    j =>
                    {
                        j.HasKey("LicenseTypeId", "VehicleTypeId").HasName("pk_lctvht");
                        j.ToTable("LicenseTypeToVehicleType");
                    });
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("PK__Manufact__357E5CC125DB731B");

            entity.Property(e => e.ManufacturerName).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A3895F5AD1A");

            entity.Property(e => e.PaymentDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pay_bookings");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pay_users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C2F4A6CFF");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E3866C3919F").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105342F027FD8").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(256);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54926A3AD926");

            entity.Property(e => e.Condition).HasMaxLength(100);

            entity.HasOne(d => d.Model).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vehicle_model");
        });

        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__VehicleM__E8D7A12C3924D637");

            entity.Property(e => e.ImageFile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModelName).HasMaxLength(100);

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.VehicleModels)
                .HasForeignKey(d => d.ManufacturerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vhm_manu");

            entity.HasOne(d => d.VehicleType).WithMany(p => p.VehicleModels)
                .HasForeignKey(d => d.VehicleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vhm_vht");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.VehicleTypeId).HasName("PK__VehicleT__9F449643083E562D");

            entity.Property(e => e.CylinderVolumeCm3).HasColumnName("CylinderVolume_cm3");
            entity.Property(e => e.VehicleTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
