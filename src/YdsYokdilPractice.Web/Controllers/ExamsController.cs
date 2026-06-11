using Microsoft.AspNetCore.Mvc;
using YdsYokdilPractice.Web.Services.Abstractions;
using YdsYokdilPractice.Web.ViewModels;

namespace YdsYokdilPractice.Web.Controllers;

public class ExamsController : Controller
{
    private readonly IExamService _examService;

    public ExamsController(IExamService examService)
    {
        _examService = examService;
    }

    public async Task<IActionResult> Index()
    {
        var exams = await _examService.GetAllExamsAsync();

        var viewModel = new ExamListViewModel
        {
            Exams = exams
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return NotFound();

        var exam = await _examService.GetExamByIdAsync(id);

        if (exam is null)
            return NotFound();

        return View(exam);
    }

    public async Task<IActionResult> Solve(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return NotFound();

        var exam = await _examService.GetExamByIdAsync(id);

        if (exam is null)
            return NotFound();

        return View(exam);
    }
}