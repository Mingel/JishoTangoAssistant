using System.Collections.Generic;
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

    public async Task ReplaceVocabularyListAsync(IList<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true)
    {
        if (resetAutoIncrementId)
            ResetAutoIncrementId();
        dbContext.VocabularyList.UpdateRange(vocabularyItems);
        await dbContext.SaveChangesAsync();
    }

    private void ResetAutoIncrementId()
    {
        dbContext.ResetAutoIncrementId();
    }
}