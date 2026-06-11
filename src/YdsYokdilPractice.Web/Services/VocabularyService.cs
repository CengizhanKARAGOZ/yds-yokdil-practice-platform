using YdsYokdilPractice.Web.Models.Vocabulary;
using YdsYokdilPractice.Web.Services.Abstractions;

namespace YdsYokdilPractice.Web.Services;

public class VocabularyService : IVocabularyService
{
    private readonly IJsonDataService _jsonDataService;

    public VocabularyService(IJsonDataService jsonDataService)
    {
        _jsonDataService = jsonDataService;
    }

    public async Task<List<VocabularyWord>> GetAllWordsAsync()
    {
        var words = await _jsonDataService.ReadJsonAsync<List<VocabularyWord>>("Data/vocabulary/academic-words.json");

        return words?
            .OrderBy(x => x.Word)
            .ToList() ?? new List<VocabularyWord>();
    }

    public async Task<List<VocabularyWord>> GetRandomWordsAsync(int count)
    {
        var words = await GetAllWordsAsync();

        return words
            .OrderBy(_ => Guid.NewGuid())
            .Take(count)
            .ToList();
    }

    public async Task<VocabularyWord?> GetWordByIdAsync(int id)
    {
        var words = await GetAllWordsAsync();

        return words.FirstOrDefault(x => x.Id == id);
    }
}