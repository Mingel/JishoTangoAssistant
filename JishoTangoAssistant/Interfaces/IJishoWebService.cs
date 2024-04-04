using System.Collections.Generic;
using System.Threading.Tasks;
using JishoTangoAssistant.Models.Jisho;

namespace JishoTangoAssistant.Interfaces;

public interface IJishoWebService
{
    Task<IList<JishoDatum>?> GetResultJsonAsync(string keyword);
}