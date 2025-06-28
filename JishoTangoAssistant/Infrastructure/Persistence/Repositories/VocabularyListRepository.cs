using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;
using Microsoft.EntityFrameworkCore;

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
        // TODO currently, in this implementation, vocabularyItems equals all items in memory, only effectively updating all items
        //      preferably, we want to split up the "writing to db" part to updating, adding and removing items to/in db
        var newItems = vocabularyItems.ToList();
        var existingAnkiGuids = dbContext.VocabularyList
            .Where(item => item.AnkiGuid != null)
            .Select(item => item.AnkiGuid)
            .OfType<string>()
            .AsNoTracking();
        var existingAnkiGuidsSet = new HashSet<string>(existingAnkiGuids);
        var newItemsAnkiGuids = newItems
            .Where(item => item.AnkiGuid != null)
            .Select(item => item.AnkiGuid)
            .OfType<string>();
        var newAnkiGuidsSet = new HashSet<string>(newItemsAnkiGuids);
        
        // update existing items
        foreach (var newItem in newItems)
        {
            var existingItem = await dbContext.VocabularyList.FirstOrDefaultAsync(i => i.AnkiGuid == newItem.AnkiGuid);
            if (existingItem == null) 
                continue;

            // TODO use AutoMapper
            existingItem.AnkiGuid = newItem.AnkiGuid;
            existingItem.AdditionalCommentsJapanese = newItem.AdditionalCommentsJapanese;
            existingItem.Meanings = newItem.Meanings;
            existingItem.Order = newItem.Order;
            existingItem.Reading = newItem.Reading;
            existingItem.ShowReading = newItem.ShowReading;
            existingItem.Word = newItem.Word;
        }
        
        // add new items
        // TODO async possible?
        var itemsToAdd = newItems
            .Where(newItem => newItem.AnkiGuid != null && !existingAnkiGuidsSet.Contains(newItem.AnkiGuid));
        dbContext.AddRange(itemsToAdd.Select(item => item.MapToEntity()));
        
        // remove missing items
        // TODO async possible?
        var itemsToRemove = dbContext.VocabularyList
            .Where(existingItem => existingItem.AnkiGuid != null && !newAnkiGuidsSet.Contains(existingItem.AnkiGuid));
        dbContext.RemoveRange(itemsToRemove);
        
        await dbContext.SaveChangesAsync();
    }
}