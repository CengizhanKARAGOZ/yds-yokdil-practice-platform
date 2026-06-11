namespace YdsYokdilPractice.Web.Models.Exam;

public class Exam
{
    public string Id { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string ExamType { get; set; } = string.Empty;

    public string Field { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public int QuestionCount { get; set; }

    public string Description { get; set; } = string.Empty;

    public List<Question> Questions { get; set; } = new();
}