using PipBoy2000.Models;
using System.Text.Json;

namespace PipBoy2000.Services;


public class JsonDataService
{
    private readonly string _folderPath;

    public JsonDataService()
    {
        _folderPath = Path.Combine(FileSystem.AppDataDirectory, "CharacterSheets");
        Directory.CreateDirectory(_folderPath);
    }

    public async Task SaveAsync(CharacterSheet sheet)
    {
        if (sheet == null) return;

        string guid = sheet.FileGuid;

        if (string.IsNullOrWhiteSpace(guid))
        {
            guid = Guid.NewGuid().ToString();
            sheet.FileGuid = guid;
        }

        string fileName = $"{guid}.json";
        string fullPath = Path.Combine(_folderPath, fileName);

        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(sheet, options);
            await File.WriteAllTextAsync(fullPath, json);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Save Error", ex.Message, "OK");
        }
    }

    public async Task<CharacterSheet?> LoadAsync(string guid)
    {
        if (string.IsNullOrWhiteSpace(guid)) return null;

        string fileName = $"{guid}.json";
        string fullPath = Path.Combine(_folderPath, fileName);

        if (!File.Exists(fullPath)) return null;

        try
        {
            string json = await File.ReadAllTextAsync(fullPath);
            var sheet = JsonSerializer.Deserialize<CharacterSheet>(json);
            return sheet;
        }
        catch
        {
            return null;
        }
    }
}