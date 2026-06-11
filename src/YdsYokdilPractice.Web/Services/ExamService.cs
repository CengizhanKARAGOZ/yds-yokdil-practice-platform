using YdsYokdilPractice.Web.Models.Exam;
using YdsYokdilPractice.Web.Services.Abstractions;

namespace YdsYokdilPractice.Web.Services;

public class ExamService : IExamService
{
    private readonly IJsonDataService _jsonDataService;

    public ExamService(IJsonDataService jsonDataService)
    {
        _jsonDataService = jsonDataService;
    }

    public async Task<List<Exam>> GetAllExamsAsync()
    {
        var exams = await _jsonDataService.ReadJsonFilesFromFolderAsync<Exam>("Data/exams");

        return exams
            .OrderBy(x => x.ExamType)
            .ThenBy(x => x.Title)
            .ToList();
    }

    public async Task<Exam?> GetExamByIdAsync(string id)
    {
        var exams = await GetAllExamsAsync();

        return exams.FirstOrDefault(x => x.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
    }
}