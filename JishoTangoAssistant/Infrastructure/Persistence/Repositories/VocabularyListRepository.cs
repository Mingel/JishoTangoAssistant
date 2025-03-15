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
        return dbContext.VocabularyList.OrderBy(i => i.Order).ToList();
    }

    public async Task ReplaceVocabularyListAsync(IEnumerable<VocabularyItem> vocabularyItems, bool resetAutoIncrementId = true)
    {
        // TODO For now, delete data in table and then re-insert, change later
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