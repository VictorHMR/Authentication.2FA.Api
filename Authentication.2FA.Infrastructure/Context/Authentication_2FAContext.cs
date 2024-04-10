using System;
using System.Collections.Generic;
using Authentication._2FA.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication._2FA.Infrastructure.Context;

public partial class Authentication_2FAContext : DbContext
{
    public Authentication_2FAContext()
    {
    }

    public Authentication_2FAContext(DbContextOptions<Authentication_2FAContext> options)
        : base(options)
    {
    }

    public virtual DbSet<USER> USERs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<USER>(entity =>
        {
            entity.HasKey(e => e.ID_USER).HasName("PRIMARY");

            entity.ToTable("USER");

            entity.Property(e => e.CREATED_AT).HasColumnType("datetime");
            entity.Property(e => e.DELETED_AT).HasColumnType("datetime");
            entity.Property(e => e.DS_EMAIL).HasMaxLength(50);
            entity.Property(e => e.DS_NAME).HasMaxLength(100);
            entity.Property(e => e.DS_PASSWORD).HasColumnType("text");
            entity.Property(e => e.LAST_VALIDATION).HasColumnType("datetime");
            entity.Property(e => e.UPDATED_AT).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
