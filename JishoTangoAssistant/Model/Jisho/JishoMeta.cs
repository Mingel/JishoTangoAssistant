using Newtonsoft.Json;

namespace JishoTangoAssistant.Services.Jisho;

public record JishoMeta([property: JsonProperty("status")] int Status = default);