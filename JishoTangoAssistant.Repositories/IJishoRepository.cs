using JishoTangoAssistant.Domain.Core.Models;

namespace JishoTangoAssistant.Repositories;

public interface IJishoRepository
{
    Task<IEnumerable<JishoDatum>?> GetResultAsync(string keyword);
}