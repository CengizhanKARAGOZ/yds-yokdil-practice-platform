using System.Text.Json;
using YdsYokdilPractice.Web.Services.Abstractions;

namespace YdsYokdilPractice.Web.Services;

public class JsonDataService : IJsonDataService
{
    private readonly IWebHostEnvironment _environment;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonDataService(IWebHostEnvironment environment)
    {
        _environment = environment;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<T?> ReadJsonAsync<T>(string relativePath)
    {
        var fullPath = Path.Combine(_environment.ContentRootPath, relativePath);

        if (!File.Exists(fullPath))
            return default;

        var json = await File.ReadAllTextAsync(fullPath);

        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    public async Task<List<T>> ReadJsonFilesFromFolderAsync<T>(string relativeFolderPath)
    {
        var folderPath = Path.Combine(_environment.ContentRootPath, relativeFolderPath);

        if (!Directory.Exists(folderPath))
            return new List<T>();

        var files = Directory.GetFiles(folderPath, "*.json", SearchOption.AllDirectories);

        var items = new List<T>();

        foreach (var file in files)
        {
            var json = await File.ReadAllTextAsync(file);
            var item = JsonSerializer.Deserialize<T>(json, _jsonOptions);

            if (item is not null)
                items.Add(item);
        }

        return items;
    }
}