namespace YdsYokdilPractice.Web.Services.Abstractions;

public interface IJsonDataService
{
    Task<T?> ReadJsonAsync<T>(string relativePath);

    Task<List<T>> ReadJsonFilesFromFolderAsync<T>(string relativeFolderPath);
}