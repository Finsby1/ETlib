﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ETlib.Models;

public partial class finsby_dk_db_viberContext : DbContext
{

    public finsby_dk_db_viberContext(DbContextOptions<finsby_dk_db_viberContext> options)
        : base(options)
    {

    }

    public virtual DbSet<PriceInterval> PriceInterval { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=mssql6.unoeuro.com;Initial Catalog=finsby_dk_db_viber;User ID=finsby_dk;Password=d2krycfGbR3HE9gADzFh");
        }

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceInterval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pk");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}