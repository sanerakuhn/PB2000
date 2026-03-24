using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PipBoy2000.Models;

public class Item : INotifyPropertyChanged
{
    private string _name = "";
    private int _qty;
    private double _weight;

    public string Name { get => _name; set => SetField(ref _name, value); }
    public int Qty { get => _qty; set => SetField(ref _qty, value); }
    public double Weight { get => _weight; set => SetField(ref _weight, value); }

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