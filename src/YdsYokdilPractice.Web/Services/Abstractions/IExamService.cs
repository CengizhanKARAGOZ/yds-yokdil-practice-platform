using YdsYokdilPractice.Web.Models.Exam;

namespace YdsYokdilPractice.Web.Services.Abstractions;

public interface IExamService
{
    Task<List<Exam>> GetAllExamsAsync();

    Task<Exam?> GetExamByIdAsync(string id);
}