using JishoTangoAssistant.Models;
using Microsoft.EntityFrameworkCore;

namespace JishoTangoAssistant.Data;

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
}