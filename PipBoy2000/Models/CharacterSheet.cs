using Microsoft.Maui.Controls;
using PipBoy2000.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PipBoy2000.Models;

public class CharacterSheet : INotifyPropertyChanged
{
    private string _characterName = "";
    private int _level = 1;
    private string _origin = "";
    private string _subOrigin = "";
    private int _xpEarned;
    private int _xpToNextLevel;
    private int _luckPoints;
    public string FileGuid { get; set; } = string.Empty;
    private double _ccw;
    private int _mcw;
    private int _caps;

    public string CharacterName
    {
        get => _characterName;
        set => SetField(ref _characterName, value);
    }

    public int Level
    {
        get => _level;
        set => SetField(ref _level, value);
    }

    public string Origin
    {
        get => _origin;
        set => SetField(ref _origin, value);
    }

    public string SubOrigin
    {
        get => _subOrigin;
        set => SetField(ref _subOrigin, value);
    }

    public int XpEarned
    {
        get => _xpEarned;
        set => SetField(ref _xpEarned, value);
    }

    public int XpToNextLevel
    {
        get => _xpToNextLevel;
        set => SetField(ref _xpToNextLevel, value);
    }

    public int LuckPoints
    {
        get => _luckPoints;
        set => SetField(ref _luckPoints, value);
    }

    public double CCW
    {
        get => _ccw;
        set => SetField(ref _ccw, value);
    }

    public int MCW
    {
        get => _mcw;
        set => SetField(ref _mcw, value);
    }

    public int Caps
    {
        get => _caps;
        set => SetField(ref _caps, value);
    }

    public int Strength { get => GetStat("Strength"); set => SetStat("Strength", value); }
    public int Perception { get => GetStat("Perception"); set => SetStat("Perception", value); }
    public int Endurance { get => GetStat("Endurance"); set => SetStat("Endurance", value); }
    public int Charisma { get => GetStat("Charisma"); set => SetStat("Charisma", value); }
    public int Intelligence { get => GetStat("Intelligence"); set => SetStat("Intelligence", value); }
    public int Agility { get => GetStat("Agility"); set => SetStat("Agility", value); }
    public int Luck { get => GetStat("Luck"); set => SetStat("Luck", value); }

    private readonly Dictionary<string, int> _special = new()
    {
        { "Strength", 5 }, { "Perception", 5 }, { "Endurance", 5 },
        { "Charisma", 5 }, { "Intelligence", 5 }, { "Agility", 5 }, { "Luck", 5 }
    };

    private int GetStat(string key) => _special.TryGetValue(key, out var val) ? val : 5;
    private void SetStat(string key, int value)
    {
        if (_special.ContainsKey(key) && _special[key] != value)
        {
            _special[key] = value;
            OnPropertyChanged(key);
        }
    }

    public ObservableCollection<Skill> Skills { get; set; } = new()
    {
        new Skill("Athletics", "STR"), new Skill("Barter", "CHA"), new Skill("Big Guns", "END"),
        new Skill("Energy Weapons", "PER"), new Skill("Explosives", "PER"), new Skill("Lockpick", "PER"),
        new Skill("Medicine", "INT"), new Skill("Melee Weapons", "STR"), new Skill("Pilot", "AGI"),
        new Skill("Repair", "INT"), new Skill("Science", "INT"), new Skill("Small Guns", "AGI"),
        new Skill("Sneak", "AGI"), new Skill("Speech", "CHA"), new Skill("Survival", "END"),
        new Skill("Throwing", "AGI"), new Skill("Unarmed", "STR")
    };

    public int MeleeDamage { get; set; }
    public int Defense { get; set; }
    public int Initiative { get; set; }
    public int MaxHp { get; set; }
    public int CurrentHp { get; set; }
    public int PoisonDr { get; set; }


    public ObservableCollection<BodyPart> BodyParts { get; set; } = new()
    {
        new BodyPart("Head", "1-2"),
        new BodyPart("Torso", "3-8"),
        new BodyPart("Left Arm", "9-11"),
        new BodyPart("Right Arm", "12-14"),
        new BodyPart("Left Leg", "15-17"),
        new BodyPart("Right Leg", "18-20")
    };

    public ObservableCollection<Weapon> Weapons { get; set; } = new();

    public ObservableCollection<Item> Inventory { get; set; } = new();

    public ObservableCollection<Item> Ammunition { get; set; } = new();

    public ObservableCollection<Perk> Perks { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}