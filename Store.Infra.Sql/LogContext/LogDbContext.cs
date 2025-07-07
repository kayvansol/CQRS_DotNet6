using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Store.Domain.Log;

namespace Store.Infra.Sql.LogContext;

public partial class LogDbContext : DbContext
{
    public LogDbContext()
    {
    }

    public LogDbContext(DbContextOptions<LogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<HandleLog> HandleLogs { get; set; }

    public virtual DbSet<OperationLog> OperationLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=192.168.1.4;Initial Catalog=LogDB;User ID=sa;Password=ABCabc123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("ErrorLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Exception).HasMaxLength(2000);
            entity.Property(e => e.Parameters).HasMaxLength(500);
        });

        modelBuilder.Entity<HandleLog>(entity =>
        {
            entity.ToTable("HandleLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Exception).HasMaxLength(500);
            entity.Property(e => e.Parameters).HasMaxLength(500);
        });

        modelBuilder.Entity<OperationLog>(entity =>
        {
            entity.ToTable("OperationLog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer).HasMaxLength(1000);
            entity.Property(e => e.CreateDateTime).HasColumnType("datetime");
            entity.Property(e => e.Parameters).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
