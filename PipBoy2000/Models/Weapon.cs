using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PipBoy2000.Models;

public class Weapon : INotifyPropertyChanged
{
    private string _name = "", _damage = "", _effects = "", _type = "", _range = "", _qualities = "";
    private int _tn, _rate, _ammo, _skill;
    private double _weight;
    private bool _tagged;

    public string Name { get => _name; set => SetField(ref _name, value); }
    public int Skill { get => _skill; set => SetField(ref _skill, value); }
    public int Tn { get => _tn; set => SetField(ref _tn, value); }
    public bool Tagged { get => _tagged; set => SetField(ref _tagged, value); }
    public string Damage { get => _damage; set => SetField(ref _damage, value); }
    public string Effects { get => _effects; set => SetField(ref _effects, value); }
    public string Type { get => _type; set => SetField(ref _type, value); }
    public int Rate { get => _rate; set => SetField(ref _rate, value); }
    public string Range { get => _range; set => SetField(ref _range, value); }
    public string Qualities { get => _qualities; set => SetField(ref _qualities, value); }
    public int Ammo { get => _ammo; set => SetField(ref _ammo, value); }
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