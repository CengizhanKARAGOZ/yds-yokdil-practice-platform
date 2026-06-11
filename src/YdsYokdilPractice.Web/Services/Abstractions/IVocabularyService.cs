using YdsYokdilPractice.Web.Models.Vocabulary;

namespace YdsYokdilPractice.Web.Services.Abstractions;

public interface IVocabularyService
{
    Task<List<VocabularyWord>> GetAllWordsAsync();

    Task<List<VocabularyWord>> GetRandomWordsAsync(int count);

    Task<VocabularyWord?> GetWordByIdAsync(int id);
}