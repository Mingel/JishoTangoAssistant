using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Infrastructure.Persistence.Repositories;

public class VocabularyListRepository : IVocabularyListRepository
{
    private readonly DbContext dbContext;

    public VocabularyListRepository()
    {
        dbContext = new DbContext();
        dbContext.Database.EnsureCreated();
    }

    public IEnumerable<VocabularyItem> GetVocabularyItems()
    {
        return dbContext.VocabularyList
            .OrderBy(i => i.Order)
            .ToList().Select(item => item.MapToModel());
    }

    public async Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true)
    {
        // TODO For now, delete data in table and then re-insert, change later
        if (resetAutoIncrementId)
            dbContext.ResetAutoIncrementId();
        var vocabularyItemEntitiesList = vocabularyItems.Select(item => item.MapToEntity()).ToList();
        
        await dbContext.VocabularyList.AddRangeAsync(vocabularyItemEntitiesList);
        await dbContext.SaveChangesAsync();
    }
}