using System.ComponentModel;

namespace PipBoy2000.Models;

public class Skill : INotifyPropertyChanged
{
    public string Name { get; }
    public string Attribute { get; }
    private bool _tagged;
    private int _rank;

    public bool Tagged
    {
        get => _tagged;
        set
        {
            if (SetField(ref _tagged, value, nameof(Tagged)))
                OnPropertyChanged(nameof(Tagged));
        }
    }

    public int Rank
    {
        get => _rank;
        set
        {
            if (SetField(ref _rank, value, nameof(Rank)))
                OnPropertyChanged(nameof(Rank));
        }
    }

    public Skill(string name, string attribute)
    {
        Name = name;
        Attribute = attribute;
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