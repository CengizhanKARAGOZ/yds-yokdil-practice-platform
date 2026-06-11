namespace YdsYokdilPractice.Web.Models.Exam;

public class Question
{
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Difficulty { get; set; } = string.Empty;

    public string QuestionText { get; set; } = string.Empty;

    public List<string> Options { get; set; } = new();

    public string CorrectAnswer { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = new();
}