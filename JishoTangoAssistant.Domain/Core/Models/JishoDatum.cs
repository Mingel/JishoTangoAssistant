namespace JishoTangoAssistant.Domain.Core.Models;

public record JishoDatum
{
    public string Slug { get; set; } = string.Empty;

    public IEnumerable<JishoJapaneseItem> Japanese { get; set; } = [];

    public IEnumerable<JishoSense> Senses { get; set; } = [];

    public JishoAttribution Attribution { get; set; } = new();
}

public record JishoAttribution
{
    public bool JmDict { get; init; }
    
    public bool JmNedict { get; init; }
    
    public string DbPedia { get; init; } = string.Empty;
}

public record JishoJapaneseItem
{
    public string Word { get; init; } = string.Empty;
    
    public string Reading { get; init; } = string.Empty;
}

public record JishoMessage
{
    public JishoMeta Meta { get; set; } = new();

    public IEnumerable<JishoDatum> Data { get; set; } = [];
}

public record JishoMeta
{
    public int Status { get; init; }
}

public record JishoSense
{
    public IEnumerable<string> EnglishDefinitions { get; set; } = [];

    public IEnumerable<string> PartsOfSpeech { get; set; } = [];

    public IEnumerable<string> Tags { get; set; } = [];
}