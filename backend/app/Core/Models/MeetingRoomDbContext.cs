using System;
using System.Collections.Generic;
using Core.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbContext;

namespace Core.Models;

public partial class MeetingRoomDbContext : IdentityDbContext<User, Role, Guid>
{
    public MeetingRoomDbContext()
    {
    }

    public MeetingRoomDbContext(DbContextOptions<MeetingRoomDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Meeting> Meetings { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER01;Database=MeetingRoomDB;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971CAC6F205894");

            entity.ToTable("Company");

            entity.HasIndex(e => e.EmailAddress, "UQ__Company__49A14740A09D6B33").IsUnique();

            entity.Property(e => e.CompanyId).ValueGeneratedNever();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1118A78EE2");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Password, "UQ__Employee__87909B15407FA82B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534538B16B5").IsUnique();

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(7)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Employee__Compan__5070F446");
        });

        modelBuilder.Entity<Meeting>(entity =>
        {
            entity.HasKey(e => e.MeetingId).HasName("PK__Meeting__E9F9E94C16795868");

            entity.ToTable("Meeting");

            entity.Property(e => e.MeetingId).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.Meetings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Meeting__RoomId__5629CD9C");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__3286393981302F9E");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId).ValueGeneratedNever();
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Company).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Room__CompanyId__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    protected void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
