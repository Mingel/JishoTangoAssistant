using System.ComponentModel.DataAnnotations;

namespace JishoTangoAssistant.Infrastructure.Entities;

public record CurrentSessionPropertyEntity
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(255)]
    public string Value { get; set; } = string.Empty;
}