using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Core.Models;

namespace JishoTangoAssistant.Infrastructure.ApiClients;

public interface IJishoWebService
{
    Task<IEnumerable<JishoDatum>?> GetResultAsync(string keyword);
}