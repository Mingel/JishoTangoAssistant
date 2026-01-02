namespace JishoTangoAssistant.Domain.Common.Data;

public record FileInfo<T>(T Content, string FilePath);