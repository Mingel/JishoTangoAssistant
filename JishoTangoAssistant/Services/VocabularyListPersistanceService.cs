using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JishoTangoAssistant.Model;
using JishoTangoAssistant.Persistence;

namespace JishoTangoAssistant.Services;

public class VocabularyListPersistanceService : IVocabularyListPersistanceService
{
    private readonly DbContext dbContext;

    public VocabularyListPersistanceService()
    {
        dbContext = new DbContext();
        dbContext.Database.EnsureCreated();
    }

    public IEnumerable<VocabularyItem> GetVocabularyItems()
    {
        return dbContext.VocabularyList.OrderBy(i => i.Order).ToList();
    }

    public async Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true)
    {
        // For now, delete data in table and then re-insert, change later
        if (resetAutoIncrementId)
            ResetAutoIncrementId();
        var vocabularyList = vocabularyItems.ToList();
        foreach (var vocabularyItem in vocabularyList)
        {
            vocabularyItem.Id = 0; // reset id for re-adding, change later as well
        }
        await dbContext.VocabularyList.AddRangeAsync(vocabularyList);
        await dbContext.SaveChangesAsync();
    }

    private void ResetAutoIncrementId()
    {
        dbContext.ResetAutoIncrementId();
    }
}