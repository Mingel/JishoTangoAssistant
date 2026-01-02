namespace JishoTangoAssistant.Domain.Core.Models;

public record UpdateSelectionResult(bool IsSuccessful, string? Title = null, string? Message = null);