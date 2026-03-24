using System.ComponentModel;

namespace PipBoy2000.Models;

public class BodyPart : INotifyPropertyChanged
{
    public string Name { get; }
    public string Range { get; }
    private int _physDr, _radDr, _enDr, _hp;

    public int PhysDr { get => _physDr; set => SetField(ref _physDr, value, nameof(PhysDr)); }
    public int RadDr { get => _radDr; set => SetField(ref _radDr, value, nameof(RadDr)); }
    public int EnDr { get => _enDr; set => SetField(ref _enDr, value, nameof(EnDr)); }
    public int Hp { get => _hp; set => SetField(ref _hp, value, nameof(Hp)); }
    public bool IsLastItem = false;


    public BodyPart(string name, string range)
    {
        Name = name;
        Range = range;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetField<T>(ref T field, T value, string propertyName)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}