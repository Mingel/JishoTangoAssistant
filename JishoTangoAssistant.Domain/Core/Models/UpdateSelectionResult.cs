namespace JishoTangoAssistant.Domain.Models.Core.Models;

public record UpdateSelectionResult(bool IsSuccessful, string? Title = null, string? Message = null);