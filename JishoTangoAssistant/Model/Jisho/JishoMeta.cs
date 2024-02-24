using Newtonsoft.Json;

namespace JishoTangoAssistant.Model.Jisho;

public record JishoMeta([property: JsonProperty("status")] int Status = default);