using Newtonsoft.Json;

namespace JishoTangoAssistant.Models.Jisho;

public record JishoMeta([property: JsonProperty("status")] int Status = default);