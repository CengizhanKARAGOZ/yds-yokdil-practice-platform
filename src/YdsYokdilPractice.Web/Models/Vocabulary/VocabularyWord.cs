namespace YdsYokdilPractice.Web.Models.Vocabulary;

public class VocabularyWord
{
    public int Id { get; set; }

    public string Word { get; set; } = string.Empty;

    public string Meaning { get; set; } = string.Empty;

    public string PartOfSpeech { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public List<string> Synonyms { get; set; } = new();

    public string Example { get; set; } = string.Empty;

    public string Translation { get; set; } = string.Empty;
}