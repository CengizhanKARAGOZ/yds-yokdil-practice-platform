using Microsoft.AspNetCore.Mvc;
using YdsYokdilPractice.Web.Services.Abstractions;

namespace YdsYokdilPractice.Web.Controllers;

public class VocabularyController : Controller
{
    private readonly IVocabularyService _vocabularyService;

    public VocabularyController(IVocabularyService vocabularyService)
    {
        _vocabularyService = vocabularyService;
    }

    public async Task<IActionResult> Index()
    {
        var words = await _vocabularyService.GetAllWordsAsync();

        return View(words);
    }

    public async Task<IActionResult> Quiz()
    {
        var words = await _vocabularyService.GetRandomWordsAsync(10);

        return View(words);
    }

    public async Task<IActionResult> Flashcards()
    {
        var words = await _vocabularyService.GetRandomWordsAsync(20);

        return View(words);
    }
}