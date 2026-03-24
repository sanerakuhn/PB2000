using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PipBoy2000.Models;

public class Perk : INotifyPropertyChanged
{
    private string _name = "", _effect = "";
    private int _rank;

    public string Name { get => _name; set => SetField(ref _name, value); }
    public int Rank { get => _rank; set => SetField(ref _rank, value); }
    public string Effect { get => _effect; set => SetField(ref _effect, value); }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}