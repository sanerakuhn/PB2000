using PipBoy2000.Models;
using PipBoy2000.Services;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Xml.Linq;

namespace PipBoy2000.Views;

public partial class CharacterSelectionPage : ContentPage
{
    private readonly JsonDataService _dataService = new();
    private readonly string _folderPath;
    private CharacterFileItem? _selectedItem;
    private Frame? _selectedFrame;

    public ObservableCollection<CharacterFileItem> CharacterFiles { get; } = new();


    public CharacterSelectionPage()
    {
        InitializeComponent();
        BindingContext = this;

        _folderPath = Path.Combine(FileSystem.AppDataDirectory, "CharacterSheets");
        Directory.CreateDirectory(_folderPath);

        LoadCharacterFiles();
    }

    private void LoadCharacterFiles()
    {
        CharacterFiles.Clear();

        if (!Directory.Exists(_folderPath)) return;

        foreach (var file in Directory.GetFiles(_folderPath, "*.json"))
        {
            try
            {
                string json = File.ReadAllText(file);
                var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("CharacterName", out _) &&
                    doc.RootElement.TryGetProperty("Origin", out _) &&
                    doc.RootElement.TryGetProperty("FileGuid", out var guidProp) && guidProp.ValueKind == JsonValueKind.String &&
                    doc.RootElement.TryGetProperty("Skills", out var skills) && skills.ValueKind == JsonValueKind.Array &&
                    doc.RootElement.TryGetProperty("BodyParts", out var bodyParts) && bodyParts.ValueKind == JsonValueKind.Array &&
                    doc.RootElement.TryGetProperty("Weapons", out var weapons) && weapons.ValueKind == JsonValueKind.Array &&
                    doc.RootElement.TryGetProperty("Inventory", out var inventory) && inventory.ValueKind == JsonValueKind.Array &&
                    doc.RootElement.TryGetProperty("Ammunition", out var ammunition) && ammunition.ValueKind == JsonValueKind.Array &&
                    doc.RootElement.TryGetProperty("Perks", out var perks) && perks.ValueKind == JsonValueKind.Array)
                {
                    string fileName = Path.GetFileName(file);
                    string guid = guidProp.GetString() ?? Path.GetFileNameWithoutExtension(file); // fallback for old files
                    string display = doc.RootElement.GetProperty("CharacterName").GetString() ?? "Unnamed";
                    display = string.IsNullOrWhiteSpace(display) ? "Unnamed" : display;
                    string level = doc.RootElement.GetProperty("Level").GetDouble().ToString() ?? "0";
                    level = string.IsNullOrWhiteSpace(level) ? "0" : level;
                    string origin = doc.RootElement.GetProperty("Origin").GetString() ?? "Unknown";
                    origin = string.IsNullOrWhiteSpace(origin) ? "Unknown" : origin;

                    CharacterFiles.Add(new CharacterFileItem
                    {
                        DisplayName = display,
                        DisplayLevel = level,
                        DisplayOrigin = origin,
                        FileName = fileName,
                        Guid = guid
                    });
                } else
                {
                    //panic
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse {Path.GetFileName(file)}: {ex.Message}");
            }
        }
    }

    private async void OnLoadSelectedClicked(object sender, EventArgs e)
    {
        if (_selectedItem == null) return;

        var sheet = await _dataService.LoadAsync(_selectedItem.Guid);

        if (sheet != null)
        {
            _selectedFrame.BackgroundColor = Color.FromHex("#1A1F26");
            _selectedFrame.BorderColor = Color.FromHex("#475867");
            _selectedItem = null;
            _selectedFrame = null;
            LoadButton.IsEnabled = _selectedItem != null;
            DeleteButton.IsEnabled = _selectedItem != null;
            ConfirmDeleteButton.IsVisible = false;
            await Navigation.PushAsync(new CharacterSheetPage(sheet));
        }
        else
        {
            await DisplayAlert("Error", "Failed to load character.", "OK");
        }
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (_selectedItem == null) return;

        ConfirmDeleteButton.IsVisible = true;
        DeleteButton.IsEnabled = false;
    }

    private async void OnConfirmDeleteClicked(object sender, EventArgs e)
    {
        if (_selectedItem == null) return;

        string fullPath = Path.Combine(_folderPath, $"{_selectedItem.Guid}.json");

        try
        {
            File.Delete(fullPath);
            await DisplayAlert("Deleted", $"Character '{_selectedItem.DisplayName}' deleted.", "OK");

            LoadCharacterFiles();
            _selectedItem = null;
            UpdateButtonStates(null);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Delete failed: {ex.Message}", "OK");
        }

        ConfirmDeleteButton.IsVisible = false;
        DeleteButton.IsEnabled = true;
    }

    private void OnButtonHoverEntered(object sender, PointerEventArgs e)
    {
        if (sender is Button button)
        {
            button.BackgroundColor = Color.FromHex("#006622");
        }
    }

    private void OnButtonHoverExited(object sender, PointerEventArgs e)
    {
        if (sender is Button button)
        {
            button.BackgroundColor = (Color)Application.Current.Resources["FalloutPrimaryGreenDark"];
        }
    }

    private void UpdateButtonStates(CharacterFileItem? selected)
    {
        LoadButton.IsEnabled = selected != null;
        DeleteButton.IsEnabled = selected != null;
        ConfirmDeleteButton.IsVisible = false;
    }

    private void OnItemHoverEntered(object sender, PointerEventArgs e)
    {
        if(sender is Frame frame)
        {
            if (frame.BindingContext is CharacterFileItem item && item == _selectedItem)
            {
                frame.BorderColor = (Color)Application.Current.Resources["FalloutBorder"];
            }
            else
            {
                frame.BackgroundColor = Color.FromHex("#2A3A3A");
                frame.BorderColor = (Color)Application.Current.Resources["FalloutBorder"];
            }
        }
    }

    private void OnItemHoverExited(object sender, PointerEventArgs e)
    {
        if (sender is Frame frame)
        {
            if (frame.BindingContext is CharacterFileItem item && item == _selectedItem)
            {
                frame.BackgroundColor = Color.FromHex("#005A22");
                frame.BorderColor = (Color)Application.Current.Resources["FalloutPrimaryGreen"];
            }
            else
            {
                frame.BackgroundColor = Color.FromHex("#1A1F26");
                frame.BorderColor = Color.FromHex("#475867");
            }
        }
    }

    private void OnItemTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is CharacterFileItem item)
        {
            if (_selectedItem != null && _selectedItem != item)
            {
                if (_selectedFrame != null)
                {
                    _selectedFrame.BackgroundColor = Color.FromHex("#1A1F26");
                    _selectedFrame.BorderColor = Color.FromHex("#475867");
                }
            }

            if (_selectedItem == item)
            {
                frame.BackgroundColor = Color.FromHex("#1A1F26");
                frame.BorderColor = Color.FromHex("#475867");
                _selectedItem = null;
                _selectedFrame = null;
            }
            else
            {
                frame.BackgroundColor = Color.FromHex("#005A22");
                frame.BorderColor = (Color)Application.Current.Resources["FalloutPrimaryGreen"];
                _selectedItem = item;
                _selectedFrame = frame;
            }

            LoadButton.IsEnabled = _selectedItem != null;
            DeleteButton.IsEnabled = _selectedItem != null;
            ConfirmDeleteButton.IsVisible = false;
        }
    }
}

