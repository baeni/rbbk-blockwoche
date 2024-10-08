﻿using Bücherei.Lib.EntitiesDocument;
using Microsoft.EntityFrameworkCore;

namespace Bücherei.Lib.Contexts;

public class DocumentContext : DbContext
{
    private const string CONNECTION_STRING = "Host=localhost;Port=54322;Database=postgres-buechereien-doc;Username=postgres;Password=password1234";
    
    public DbSet<BuechereiDoc> Buechereien => Set<BuechereiDoc>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BuechereiDoc>()
            .ToTable("buechereien")
            .OwnsMany(b => b.Autoren, c =>
            {
                c.ToJson();
                c.OwnsMany(d => d.Buecher);
            });
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(CONNECTION_STRING);
    }
}