using System.Collections.Generic;
using JishoTangoAssistant.Core.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Infrastructure.Persistence;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private const string DataSource = "local.db"; 
    public DbSet<VocabularyItem> VocabularyList { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
    {
        contextOptionsBuilder.UseSqlite($"Data Source={DataSource}");
    }

    public void ResetAutoIncrementId()
    {
        ChangeTracker.Clear();
        Database.ExecuteSql($"DELETE FROM VocabularyList");
        Database.ExecuteSql($"DELETE FROM sqlite_sequence WHERE name = 'VocabularyList'");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<VocabularyItem>()
            .Property(e => e.Meanings)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<IEnumerable<string>>>(v) ?? new List<IEnumerable<string>>())
            .HasMaxLength(2000);
    }
}