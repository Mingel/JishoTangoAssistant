namespace JishoTangoAssistant.Domain.Models.Common.Data;

public record FileInfo<T>(T Content, string FilePath);