using Microsoft.Maui.Controls.Platform.Compatibility;
using PipBoy2000.Models;
using PipBoy2000.Services;

namespace PipBoy2000.Views;

public partial class CharacterSheetPage : ContentPage
{
    private readonly JsonDataService _dataService = new();
    private CharacterSheet _sheet = new();
    public SimpleValue _ccw = new SimpleValue();
    private string _currentTab = "SPECIAL";

    public CharacterSheetPage(CharacterSheet sheet = null)
    {
        InitializeComponent();
        _sheet = sheet ?? new CharacterSheet();
        BindingContext = _sheet;

        _sheet.PropertyChanged += async (s, e) => await SaveIfNeeded();
        foreach (var skill in _sheet.Skills)
            skill.PropertyChanged += async (s, e) => await SaveIfNeeded();
        foreach (var part in _sheet.BodyParts)
            part.PropertyChanged += async (s, e) => await SaveIfNeeded();
        foreach (var weapon in _sheet.Weapons)
            weapon.PropertyChanged += async (s, e) => await SaveIfNeeded();
        foreach (var item in _sheet.Inventory)
            item.PropertyChanged += async (s, e) => await SaveIfNeeded();
        foreach (var item in _sheet.Ammunition)
            item.PropertyChanged += async (s, e) => await SaveIfNeeded();
        foreach (var perk in _sheet.Perks)
            perk.PropertyChanged += async (s, e) => await SaveIfNeeded();

        SwitchTab("SPECIAL");
    }

    private async Task SaveIfNeeded()
    {
        UpdateWeight();
        await _dataService.SaveAsync(_sheet);
    }

    private void UpdateWeight()
    {
        _sheet.CCW = _sheet.Ammunition.Sum(item => item.Qty * item.Weight) +
            _sheet.Inventory.Sum(item => item.Qty * item.Weight) +
            _sheet.Weapons.Sum(item => item.Weight);
    }

    private void OnTabTapped(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string tab)
        {
            SwitchTab(tab);
        }
    }

    private void SwitchTab(string tab)
    {
        _currentTab = tab;
        TabContent.Content = null;

        View tabView = tab switch
        {
            "SPECIAL" => BuildSpecialTab(),
            "Skills" => BuildSkillsTab(),
            "Combat" => BuildCombatTab(),
            "Weapons" => BuildWeaponsTab(),
            "Inventory" => BuildInventoryTab(),
            "Perks" => BuildPerksTab(),
            _ => new Label { Text = "Tab not found", HorizontalOptions = LayoutOptions.Center }
        };

        TabContent.Content = tabView;
    }

    private View BuildSpecialTab()
    {
        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            )
        };

        var strLabel = new Label { FontSize = 48, Text = "STR", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(strLabel);
        Grid.SetColumn(strLabel, 0);

        var perLabel = new Label { FontSize = 48, Text = "PER", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(perLabel);
        Grid.SetColumn(perLabel, 1);

        var endLabel = new Label { FontSize = 48, Text = "END", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(endLabel);
        Grid.SetColumn(endLabel, 2);

        var chaLabel = new Label { FontSize = 48, Text = "CHA", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(chaLabel);
        Grid.SetColumn(chaLabel, 3);

        var intLabel = new Label { FontSize = 48, Text = "INT", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(intLabel);
        Grid.SetColumn(intLabel, 4);

        var agiLabel = new Label { FontSize = 48, Text = "AGI", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(agiLabel);
        Grid.SetColumn(agiLabel, 5);

        var lckLabel = new Label { FontSize = 48, Text = "LCK", HorizontalOptions = LayoutOptions.Center };
        grid.Children.Add(lckLabel);
        Grid.SetColumn(lckLabel, 6);

        var strEntry = new Entry { FontSize=96, HorizontalTextAlignment = TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        strEntry.SetBinding(Entry.TextProperty, "Strength");
        grid.Children.Add(strEntry);
        Grid.SetRow(strEntry, 1);
        Grid.SetColumn(strEntry, 0);

        var perEntry = new Entry { FontSize = 96, HorizontalTextAlignment = TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        perEntry.SetBinding(Entry.TextProperty, "Perception");
        grid.Children.Add(perEntry);
        Grid.SetRow(perEntry, 1);
        Grid.SetColumn(perEntry, 1);

        var endEntry = new Entry { FontSize = 96, HorizontalTextAlignment = TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        endEntry.SetBinding(Entry.TextProperty, "Endurance");
        grid.Children.Add(endEntry);
        Grid.SetRow(endEntry, 1);
        Grid.SetColumn(endEntry, 2);

        var chaEntry = new Entry { FontSize = 96, HorizontalTextAlignment = TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        chaEntry.SetBinding(Entry.TextProperty, "Charisma");
        grid.Children.Add(chaEntry);
        Grid.SetRow(chaEntry, 1);
        Grid.SetColumn(chaEntry, 3);

        var intEntry = new Entry { FontSize = 96, HorizontalTextAlignment = TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        intEntry.SetBinding(Entry.TextProperty, "Intelligence");
        grid.Children.Add(intEntry);
        Grid.SetRow(intEntry, 1);
        Grid.SetColumn(intEntry, 4);

        var agiEntry = new Entry { FontSize = 96, HorizontalTextAlignment = TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        agiEntry.SetBinding(Entry.TextProperty, "Agility");
        grid.Children.Add(agiEntry);
        Grid.SetRow(agiEntry, 1);
        Grid.SetColumn(agiEntry, 5);

        var lckEntry = new Entry { FontSize = 96, HorizontalTextAlignment=TextAlignment.Center, Margin = new Thickness(3, 0, 3, 0), Keyboard = Keyboard.Numeric };
        lckEntry.SetBinding(Entry.TextProperty, "Luck");
        grid.Children.Add(lckEntry);
        Grid.SetRow(lckEntry, 1);
        Grid.SetColumn(lckEntry, 6);

        return new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "S.P.E.C.I.A.L.", FontSize = 48, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    grid
                }
            }
        };
    }

    private View BuildSkillsTab()
    {
        var outerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = GridLength.Star }
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star }
            ),
            RowSpacing = 8,
            ColumnSpacing = 12,
            Padding = 10
        };

        var leftGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },         // Name
                new ColumnDefinition { Width = new GridLength(100) },     // Attribute
                new ColumnDefinition { Width = new GridLength(80) },      // Rank
                new ColumnDefinition { Width = new GridLength(60) }       // Tagged checkbox
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto },  // Header row
                new RowDefinition { Height = GridLength.Auto },  // Athletics
                new RowDefinition { Height = GridLength.Auto },  // Barter
                new RowDefinition { Height = GridLength.Auto },  // Big Guns
                new RowDefinition { Height = GridLength.Auto },  // Energy Weapons
                new RowDefinition { Height = GridLength.Auto },  // Explosives
                new RowDefinition { Height = GridLength.Auto },  // Lockpick
                new RowDefinition { Height = GridLength.Auto },  // Medicine
                new RowDefinition { Height = GridLength.Auto },  // Melee Weapons
                new RowDefinition { Height = GridLength.Auto }   // Pilot
            ),
            RowSpacing = 8,
            ColumnSpacing = 12,
            Padding = 10
        };

        var rightGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },         // Name
                new ColumnDefinition { Width = new GridLength(100) },     // Attribute
                new ColumnDefinition { Width = new GridLength(80) },      // Rank
                new ColumnDefinition { Width = new GridLength(60) }       // Tagged checkbox
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto },  // Header row
                new RowDefinition { Height = GridLength.Auto },  // Repair
                new RowDefinition { Height = GridLength.Auto },  // Science
                new RowDefinition { Height = GridLength.Auto },  // Small Guns
                new RowDefinition { Height = GridLength.Auto },  // Sneak
                new RowDefinition { Height = GridLength.Auto },  // Speech
                new RowDefinition { Height = GridLength.Auto },  // Survival
                new RowDefinition { Height = GridLength.Auto },  // Throwing
                new RowDefinition { Height = GridLength.Auto }   // Unarmed
            ),
            RowSpacing = 8,
            ColumnSpacing = 12,
            Padding = 10
        };


        var headerLeftName = new Label
        {
            Text = "Name",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };
        leftGrid.Children.Add(headerLeftName);
        Grid.SetColumn(headerLeftName, 0);
        Grid.SetRow(headerLeftName, 0);

        var headerLeftAttr = new Label
        {
            Text = "Attribute",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        leftGrid.Children.Add(headerLeftAttr);
        Grid.SetColumn(headerLeftAttr, 1);
        Grid.SetRow(headerLeftAttr, 0);

        var headerLeftRank = new Label
        {
            Text = "Rank",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        leftGrid.Children.Add(headerLeftRank);
        Grid.SetColumn(headerLeftRank, 2);
        Grid.SetRow(headerLeftRank, 0);

        var headerLeftTag = new Label
        {
            Text = "Tag",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        leftGrid.Children.Add(headerLeftTag);
        Grid.SetColumn(headerLeftTag, 3);
        Grid.SetRow(headerLeftTag, 0);

        var headerRightName = new Label
        {
            Text = "Name",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };
        rightGrid.Children.Add(headerRightName);
        Grid.SetColumn(headerRightName, 0);
        Grid.SetRow(headerRightName, 0);

        var headerRightAttr = new Label
        {
            Text = "Attribute",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        rightGrid.Children.Add(headerRightAttr);
        Grid.SetColumn(headerRightAttr, 1);
        Grid.SetRow(headerRightAttr, 0);

        var headerRightRank = new Label
        {
            Text = "Rank",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        rightGrid.Children.Add(headerRightRank);
        Grid.SetColumn(headerRightRank, 2);
        Grid.SetRow(headerRightRank, 0);

        var headerRightTag = new Label
        {
            Text = "Tag",
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        rightGrid.Children.Add(headerRightTag);
        Grid.SetColumn(headerRightTag, 3);
        Grid.SetRow(headerRightTag, 0);


        AddSkillRow(leftGrid, 1, "Athletics", "STR", "Skills[0].Rank", "Skills[0].Tagged");

        AddSkillRow(leftGrid, 2, "Barter", "CHA", "Skills[1].Rank", "Skills[1].Tagged");

        AddSkillRow(leftGrid, 3, "Big Guns", "END", "Skills[2].Rank", "Skills[2].Tagged");

        AddSkillRow(leftGrid, 4, "Energy Weapons", "PER", "Skills[3].Rank", "Skills[3].Tagged");

        AddSkillRow(leftGrid, 5, "Explosives", "PER", "Skills[4].Rank", "Skills[4].Tagged");

        AddSkillRow(leftGrid, 6, "Lockpick", "PER", "Skills[5].Rank", "Skills[5].Tagged");

        AddSkillRow(leftGrid, 7, "Medicine", "INT", "Skills[6].Rank", "Skills[6].Tagged");

        AddSkillRow(leftGrid, 8, "Melee Weapons", "STR", "Skills[7].Rank", "Skills[7].Tagged");

        AddSkillRow(leftGrid, 9, "Pilot", "AGI", "Skills[8].Rank", "Skills[8].Tagged");

        AddSkillRow(rightGrid, 1, "Repair", "INT", "Skills[9].Rank", "Skills[9].Tagged");

        AddSkillRow(rightGrid, 2, "Science", "INT", "Skills[10].Rank", "Skills[10].Tagged");

        AddSkillRow(rightGrid, 3, "Small Guns", "AGI", "Skills[11].Rank", "Skills[11].Tagged");

        AddSkillRow(rightGrid, 4, "Sneak", "AGI", "Skills[12].Rank", "Skills[12].Tagged");

        AddSkillRow(rightGrid, 5, "Speech", "CHA", "Skills[13].Rank", "Skills[13].Tagged");

        AddSkillRow(rightGrid, 6, "Survival", "END", "Skills[14].Rank", "Skills[14].Tagged");

        AddSkillRow(rightGrid, 7, "Throwing", "AGI", "Skills[15].Rank", "Skills[15].Tagged");

        AddSkillRow(rightGrid, 8, "Unarmed", "STR", "Skills[16].Rank", "Skills[16].Tagged");

        outerGrid.Children.Add(leftGrid);
        Grid.SetColumn(leftGrid, 0);
        Grid.SetRow(leftGrid, 0);

        outerGrid.Children.Add(rightGrid);
        Grid.SetColumn(rightGrid, 1);
        Grid.SetRow(rightGrid, 0);

        return new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
            {
                new Label { Text = "Skills", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                outerGrid
            }
            }
        };
    }

    private void AddSkillRow(Grid grid, int rowIndex, string name, string attribute, string rankBinding, string taggedBinding)
    {
        var nameLbl = new Label { Text = name, VerticalOptions = LayoutOptions.Center };
        grid.Children.Add(nameLbl);
        Grid.SetColumn(nameLbl, 0);
        Grid.SetRow(nameLbl, rowIndex);

        var attrLbl = new Label { Text = attribute, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
        grid.Children.Add(attrLbl);
        Grid.SetColumn(attrLbl, 1);
        Grid.SetRow(attrLbl, rowIndex);

        var rankEnt = new Entry { Keyboard = Keyboard.Numeric, HorizontalTextAlignment = TextAlignment.Center };
        rankEnt.SetBinding(Entry.TextProperty, rankBinding);
        grid.Children.Add(rankEnt);
        Grid.SetColumn(rankEnt, 2);
        Grid.SetRow(rankEnt, rowIndex);

        var tagChk = new CheckBox { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
        tagChk.SetBinding(CheckBox.IsCheckedProperty, taggedBinding);
        grid.Children.Add(tagChk);
        Grid.SetColumn(tagChk, 3);
        Grid.SetRow(tagChk, rowIndex);
    }

    private View BuildCombatTab()
    {
        var combatGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Auto }, // Labels column - sizes to content
                new ColumnDefinition { Width = GridLength.Star }  // Entries column - fills remaining width
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto }, // Melee Dmg
                new RowDefinition { Height = GridLength.Auto }, // Defense
                new RowDefinition { Height = GridLength.Auto }, // Initiative
                new RowDefinition { Height = GridLength.Auto }, // Max HP
                new RowDefinition { Height = GridLength.Auto }, // Current HP
                new RowDefinition { Height = GridLength.Auto }  // Poison DR
            ),
            RowSpacing = 12,
            ColumnSpacing = 16,
            Padding = 10
        };

        // Row 0: Melee Dmg
        var meleeLabel = new Label { Text = "Melee Dmg:", VerticalOptions = LayoutOptions.Center };
        combatGrid.Children.Add(meleeLabel);
        Grid.SetColumn(meleeLabel, 0);
        Grid.SetRow(meleeLabel, 0);

        var meleeEntry = new Entry { WidthRequest = 60 };
        meleeEntry.SetBinding(Entry.TextProperty, "MeleeDamage");
        combatGrid.Children.Add(meleeEntry);
        Grid.SetColumn(meleeEntry, 1);
        Grid.SetRow(meleeEntry, 0);

        // Row 1: Defense
        var defenseLabel = new Label { Text = "Defense:", VerticalOptions = LayoutOptions.Center };
        combatGrid.Children.Add(defenseLabel);
        Grid.SetColumn(defenseLabel, 0);
        Grid.SetRow(defenseLabel, 1);

        var defenseEntry = new Entry { WidthRequest = 60 };
        defenseEntry.SetBinding(Entry.TextProperty, "Defense");
        combatGrid.Children.Add(defenseEntry);
        Grid.SetColumn(defenseEntry, 1);
        Grid.SetRow(defenseEntry, 1);

        // Row 2: Initiative
        var initiativeLabel = new Label { Text = "Initiative:", VerticalOptions = LayoutOptions.Center };
        combatGrid.Children.Add(initiativeLabel);
        Grid.SetColumn(initiativeLabel, 0);
        Grid.SetRow(initiativeLabel, 2);

        var initiativeEntry = new Entry { WidthRequest = 60 };
        initiativeEntry.SetBinding(Entry.TextProperty, "Initiative");
        combatGrid.Children.Add(initiativeEntry);
        Grid.SetColumn(initiativeEntry, 1);
        Grid.SetRow(initiativeEntry, 2);

        // Row 3: Max HP
        var maxHpLabel = new Label { Text = "Max HP:", VerticalOptions = LayoutOptions.Center };
        combatGrid.Children.Add(maxHpLabel);
        Grid.SetColumn(maxHpLabel, 0);
        Grid.SetRow(maxHpLabel, 3);

        var maxHpEntry = new Entry { WidthRequest = 60 };
        maxHpEntry.SetBinding(Entry.TextProperty, "MaxHp");
        combatGrid.Children.Add(maxHpEntry);
        Grid.SetColumn(maxHpEntry, 1);
        Grid.SetRow(maxHpEntry, 3);

        // Row 4: Current HP
        var currentHpLabel = new Label { Text = "Current HP:", VerticalOptions = LayoutOptions.Center };
        combatGrid.Children.Add(currentHpLabel);
        Grid.SetColumn(currentHpLabel, 0);
        Grid.SetRow(currentHpLabel, 4);

        var currentHpEntry = new Entry { WidthRequest = 60 };
        currentHpEntry.SetBinding(Entry.TextProperty, "CurrentHp");
        combatGrid.Children.Add(currentHpEntry);
        Grid.SetColumn(currentHpEntry, 1);
        Grid.SetRow(currentHpEntry, 4);

        // Row 5: Poison DR
        var poisonDrLabel = new Label { Text = "Poison DR:", VerticalOptions = LayoutOptions.Center };
        combatGrid.Children.Add(poisonDrLabel);
        Grid.SetColumn(poisonDrLabel, 0);
        Grid.SetRow(poisonDrLabel, 5);

        var poisonDrEntry = new Entry { WidthRequest = 60 };
        poisonDrEntry.SetBinding(Entry.TextProperty, "PoisonDr");
        combatGrid.Children.Add(poisonDrEntry);
        Grid.SetColumn(poisonDrEntry, 1);
        Grid.SetRow(poisonDrEntry, 5);

        var combatFrame = new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Margin = new Thickness(0, 0, 5, 0),
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "Combat", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    combatGrid,
                }
            }
        };

        string part1 = "";
        string part2 = "";
        string part3 = "";
        string part4 = "";
        string part5 = "";
        string part6 = "";

        if (_sheet.Origin == "Mister Handy")
        {
            part1 = "Optics";
            part2 = "Main Body";
            part3 = "Arm 1";
            part4 = "Arm 2";
            part5 = "Arm 3";
            part6 = "Thruster";
        } else
        {
            part1 = "Head";
            part2 = "Torso";
            part3 = "Left Arm";
            part4 = "Right Arm";
            part5 = "Left Leg";
            part6 = "Right Leg";
        }


        var bodyGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                           new ColumnDefinition { Width = new GridLength(120) },  // Name
                           new ColumnDefinition { Width = new GridLength(60) },   // Range
                           new ColumnDefinition { Width = new GridLength(60) },   // Phys. Dr
                           new ColumnDefinition { Width = new GridLength(60) },   // Rad. Dr
                           new ColumnDefinition { Width = new GridLength(60) }    // En. Dr
                       ),
            RowDefinitions = new RowDefinitionCollection(
                           new RowDefinition { Height = GridLength.Auto },  // Header
                           new RowDefinition { Height = GridLength.Auto },  // Head
                           new RowDefinition { Height = GridLength.Auto },  // Torso
                           new RowDefinition { Height = GridLength.Auto },  // Left Arm
                           new RowDefinition { Height = GridLength.Auto },  // Right Arm
                           new RowDefinition { Height = GridLength.Auto },  // Left Leg
                           new RowDefinition { Height = GridLength.Auto }   // Right Leg
                       ),
            RowSpacing = 8,
            ColumnSpacing = 12,
            Padding = 10
        };

        var headerName = new Label { Text = "Part", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Start };
        bodyGrid.Children.Add(headerName);
        Grid.SetColumn(headerName, 0);
        Grid.SetRow(headerName, 0);

        var headerRange = new Label { Text = "Range", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
        bodyGrid.Children.Add(headerRange);
        Grid.SetColumn(headerRange, 1);
        Grid.SetRow(headerRange, 0);

        var headerPhys = new Label { Text = "Phys. Dr", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
        bodyGrid.Children.Add(headerPhys);
        Grid.SetColumn(headerPhys, 2);
        Grid.SetRow(headerPhys, 0);

        var headerRad = new Label { Text = "Rad. Dr", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
        bodyGrid.Children.Add(headerRad);
        Grid.SetColumn(headerRad, 3);
        Grid.SetRow(headerRad, 0);

        var headerEn = new Label { Text = "En. Dr", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
        bodyGrid.Children.Add(headerEn);
        Grid.SetColumn(headerEn, 4);
        Grid.SetRow(headerEn, 0);

        AddBodyPartRow(bodyGrid, 1, 0, part1, "1-2", "BodyParts[0].PhysDr", "BodyParts[0].RadDr", "BodyParts[0].EnDr");
        AddBodyPartRow(bodyGrid, 2, 1, part2, "3-8", "BodyParts[1].PhysDr", "BodyParts[1].RadDr", "BodyParts[1].EnDr");
        AddBodyPartRow(bodyGrid, 3, 2, part3, "9-11", "BodyParts[2].PhysDr", "BodyParts[2].RadDr", "BodyParts[2].EnDr");
        AddBodyPartRow(bodyGrid, 4, 3, part4, "12-14", "BodyParts[3].PhysDr", "BodyParts[3].RadDr", "BodyParts[3].EnDr");
        AddBodyPartRow(bodyGrid, 5, 4, part5, "15-17", "BodyParts[4].PhysDr", "BodyParts[4].RadDr", "BodyParts[4].EnDr");
        AddBodyPartRow(bodyGrid, 6, 5, part6, "18-20", "BodyParts[5].PhysDr", "BodyParts[5].RadDr", "BodyParts[5].EnDr");

        var bodyFrame = new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Margin = new Thickness(5, 0, 0, 0),
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "Body", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    bodyGrid,
                }
            }
        };

        var outerGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            ),
            RowDefinitions = new RowDefinitionCollection { new RowDefinition { Height = GridLength.Auto } }
        };

        Grid.SetColumn(combatFrame, 0);
        Grid.SetColumn(bodyFrame, 1);

        outerGrid.Children.Add(combatFrame);
        outerGrid.Children.Add(bodyFrame);

        return outerGrid;
    }

    private void AddCombatRow(Grid grid, int rowIndex, string labelText, string bindingPath)
    {
        var label = new Label { Text = labelText, VerticalOptions = LayoutOptions.Center };
        grid.Children.Add(label);
        Grid.SetColumn(label, 0);
        Grid.SetRow(label, rowIndex);

        var entry = new Entry { WidthRequest = 60 };
        entry.SetBinding(Entry.TextProperty, bindingPath);
        grid.Children.Add(entry);
        Grid.SetColumn(entry, 1);
        Grid.SetRow(entry, rowIndex);
    }

    private void AddBodyPartRow(Grid grid, int rowIndex, int partIndex, string name, string range, string physBinding, string radBinding, string enBinding)
    {
        var nameLbl = new Label { Text = name, VerticalOptions = LayoutOptions.Center };
        grid.Children.Add(nameLbl);
        Grid.SetColumn(nameLbl, 0);
        Grid.SetRow(nameLbl, rowIndex);

        var rangeLbl = new Label { Text = range, FontSize = 12, VerticalOptions = LayoutOptions.Center };
        grid.Children.Add(rangeLbl);
        Grid.SetColumn(rangeLbl, 1);
        Grid.SetRow(rangeLbl, rowIndex);

        var physEntry = new Entry { Keyboard = Keyboard.Numeric, FontSize = 12 };
        physEntry.SetBinding(Entry.TextProperty, physBinding);
        grid.Children.Add(physEntry);
        Grid.SetColumn(physEntry, 2);
        Grid.SetRow(physEntry, rowIndex);

        var radEntry = new Entry { Keyboard = Keyboard.Numeric, FontSize = 12 };
        radEntry.SetBinding(Entry.TextProperty, radBinding);
        grid.Children.Add(radEntry);
        Grid.SetColumn(radEntry, 3);
        Grid.SetRow(radEntry, rowIndex);

        var enEntry = new Entry { Keyboard = Keyboard.Numeric, FontSize = 12 };
        enEntry.SetBinding(Entry.TextProperty, enBinding);
        grid.Children.Add(enEntry);
        Grid.SetColumn(enEntry, 4);
        Grid.SetRow(enEntry, rowIndex);
    }

    private View BuildWeaponsTab()
    {
        var mainGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = new GridLength(180) }, // Name
                new ColumnDefinition { Width = new GridLength(45) }, // Skill
                new ColumnDefinition { Width = new GridLength(35) },  // TN
                new ColumnDefinition { Width = new GridLength(40) },  // Tag (checkbox)
                new ColumnDefinition { Width = new GridLength(70) }, // Damage
                new ColumnDefinition { Width = new GridLength(180) }, // Effects
                new ColumnDefinition { Width = new GridLength(100) }, // Type
                new ColumnDefinition { Width = new GridLength(70) },  // Rate
                new ColumnDefinition { Width = new GridLength(70) }, // Range
                new ColumnDefinition { Width = GridLength.Star }, // Qualities
                new ColumnDefinition { Width = new GridLength(70) },  // Ammo
                new ColumnDefinition { Width = new GridLength(70) },  // Weight
                new ColumnDefinition { Width = new GridLength(80) }  // Remove
            ),
            RowDefinitions = new RowDefinitionCollection(
            ),
            RowSpacing = 8,
            ColumnSpacing = 8,
            Padding = 10
        };

        // Header row (row 0)
        if(_sheet.Weapons.Count > 0)
        {
            string[] headers = { "Name", "Skill", "TN", "Tag", "Damage", "Effects", "Type", "Rate", "Range", "Qualities", "Ammo", "Weight" };

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            for (int i = 0; i < headers.Length; i++)
            {
                var headerLabel = new Label
                {
                    Text = headers[i],
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                mainGrid.Children.Add(headerLabel);
                Grid.SetColumn(headerLabel, i);
                Grid.SetRow(headerLabel, 0);
            }

            // Add rows for existing weapons
            int rowIndex = 1;
            foreach (var weapon in _sheet.Weapons)
            {
                AddWeaponRow(mainGrid, rowIndex, _sheet.Weapons.IndexOf(weapon));
                weapon.PropertyChanged += async (s, e) => await SaveIfNeeded();
                rowIndex++;
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            }
        }

        // Add button (wired correctly)
        var addButton = new Button
        {
            Text = "Add Weapon",
            Margin = new Thickness(0, 16, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        addButton.Clicked += OnAddWeaponClicked;

        return new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
            {
                new Label { Text = "Weapons", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                mainGrid,
                addButton
            }
            }
        };
    }

    // Helper to add one weapon row
    private void AddWeaponRow(Grid grid, int rowIndex, int weaponIndex)
    {
        // Name
        var nameEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        nameEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Name");
        grid.Children.Add(nameEntry);
        Grid.SetColumn(nameEntry, 0);
        Grid.SetRow(nameEntry, rowIndex);

        // Skill
        var skillEntry = new Entry();
        skillEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Skill");
        grid.Children.Add(skillEntry);
        Grid.SetColumn(skillEntry, 1);
        Grid.SetRow(skillEntry, rowIndex);

        // TN
        var tnEntry = new Entry { Keyboard = Keyboard.Numeric };
        tnEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Tn");
        grid.Children.Add(tnEntry);
        Grid.SetColumn(tnEntry, 2);
        Grid.SetRow(tnEntry, rowIndex);

        // Tag
        var tagCheck = new CheckBox { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
        tagCheck.SetBinding(CheckBox.IsCheckedProperty, $"Weapons[{weaponIndex}].Tagged");
        grid.Children.Add(tagCheck);
        Grid.SetColumn(tagCheck, 3);
        Grid.SetRow(tagCheck, rowIndex);

        // Damage
        var damageEntry = new Entry();
        damageEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Damage");
        grid.Children.Add(damageEntry);
        Grid.SetColumn(damageEntry, 4);
        Grid.SetRow(damageEntry, rowIndex);

        // Effects
        var effectsEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        effectsEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Effects");
        grid.Children.Add(effectsEntry);
        Grid.SetColumn(effectsEntry, 5);
        Grid.SetRow(effectsEntry, rowIndex);

        // Type
        var typeEntry = new Entry();
        typeEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Type");
        grid.Children.Add(typeEntry);
        Grid.SetColumn(typeEntry, 6);
        Grid.SetRow(typeEntry, rowIndex);

        // Rate
        var rateEntry = new Entry { Keyboard = Keyboard.Numeric };
        rateEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Rate");
        grid.Children.Add(rateEntry);
        Grid.SetColumn(rateEntry, 7);
        Grid.SetRow(rateEntry, rowIndex);

        // Range
        var rangeEntry = new Entry();
        rangeEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Range");
        grid.Children.Add(rangeEntry);
        Grid.SetColumn(rangeEntry, 8);
        Grid.SetRow(rangeEntry, rowIndex);

        // Qualities
        var qualitiesEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        qualitiesEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Qualities");
        grid.Children.Add(qualitiesEntry);
        Grid.SetColumn(qualitiesEntry, 9);
        Grid.SetRow(qualitiesEntry, rowIndex);

        // Ammo
        var ammoEntry = new Entry { Keyboard = Keyboard.Numeric };
        ammoEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Ammo");
        grid.Children.Add(ammoEntry);
        Grid.SetColumn(ammoEntry, 10);
        Grid.SetRow(ammoEntry, rowIndex);

        // Weight
        var weightEntry = new Entry { Keyboard = Keyboard.Numeric };
        weightEntry.SetBinding(Entry.TextProperty, $"Weapons[{weaponIndex}].Weight");
        grid.Children.Add(weightEntry);
        Grid.SetColumn(weightEntry, 11);
        Grid.SetRow(weightEntry, rowIndex);

        // Remove
        var deleteButton = new Button
        {
            Text = "Delete",
            Margin = new Thickness(0, 0, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        deleteButton.CommandParameter = weaponIndex;
        deleteButton.Clicked += OnRemoveWeaponClicked;
        grid.Children.Add(deleteButton);
        Grid.SetColumn(deleteButton, 12);
        Grid.SetRow(deleteButton, rowIndex);
    }

    private void OnAddWeaponClicked(object sender, EventArgs e)
    {
        _sheet.Weapons.Add(new Weapon());
        SwitchTab("Weapons");
    }

    private void OnRemoveWeaponClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int index)
        {
            _sheet.Weapons.Remove(_sheet.Weapons[index]);
            SaveIfNeeded();
            SwitchTab("Weapons");
        }
    }

    private View BuildInventoryTab()
    {
        UpdateWeight();

        var summaryGrid = new Grid()
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Auto }
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto }
            ),
            RowSpacing = 8,
            ColumnSpacing = 8,
            Padding = 10
        };

        // Current carry weight label
        var ccwLabel = new Label { Text = "Current Carry Weight:", Margin = new Thickness(8, 0, 8, 0), VerticalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.End};
        summaryGrid.Children.Add(ccwLabel);
        Grid.SetColumn(ccwLabel, 0);
        Grid.SetRow(ccwLabel, 0);

        // Current carry weight entry
        var ccwEntry = new Entry { Keyboard = Keyboard.Numeric, IsReadOnly = true};
        ccwEntry.SetBinding(Entry.TextProperty, $"CCW");
        summaryGrid.Children.Add(ccwEntry);
        Grid.SetColumn(ccwEntry, 1);
        Grid.SetRow(ccwEntry, 0);

        // Current carry weight label
        var mcwLabel = new Label { Text = "Maximum Carry Weight:", Margin = new Thickness(8, 0, 8, 0), VerticalOptions = LayoutOptions.Center };
        summaryGrid.Children.Add(mcwLabel);
        Grid.SetColumn(mcwLabel, 2);
        Grid.SetRow(mcwLabel, 0);

        // Maximum carry weight entry
        var mcwEntry = new Entry { Keyboard = Keyboard.Numeric };
        mcwEntry.SetBinding(Entry.TextProperty, $"MCW");
        summaryGrid.Children.Add(mcwEntry);
        Grid.SetColumn(mcwEntry, 3);
        Grid.SetRow(mcwEntry, 0);

        // Caps label
        var capsLabel = new Label { Text = "Caps:", Margin = new Thickness(8, 0, 8, 0), VerticalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.End };
        summaryGrid.Children.Add(capsLabel);
        Grid.SetColumn(capsLabel, 4);
        Grid.SetRow(capsLabel, 0);

        // Maximum carry weight entry
        var capsEntry = new Entry { Keyboard = Keyboard.Numeric };
        capsEntry.SetBinding(Entry.TextProperty, $"Caps");
        summaryGrid.Children.Add(capsEntry);
        Grid.SetColumn(capsEntry, 5);
        Grid.SetRow(capsEntry, 0);

        var outerGrid = new Grid()
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            ),
            RowDefinitions = new RowDefinitionCollection(
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Star }
            ),
            RowSpacing = 8,
            ColumnSpacing = 8,
            Padding = 10
        };

        //Inventory grid
        var mainInvGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = new GridLength(180) }, // Name
                new ColumnDefinition { Width = new GridLength(70) }, // Qty
                new ColumnDefinition { Width = new GridLength(100) },  // Weight
                new ColumnDefinition { Width = new GridLength(80) }  // Delete
                ),
            RowDefinitions = new RowDefinitionCollection(),
            RowSpacing = 8,
            ColumnSpacing = 8,
            Padding = 10
        };

        // Header row (row 0)
        if (_sheet.Inventory.Count > 0)
        {
            string[] headers = { "Name", "Qty", "Weight" };

            mainInvGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            for (int i = 0; i < headers.Length; i++)
            {
                var headerLabel = new Label
                {
                    Text = headers[i],
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                mainInvGrid.Children.Add(headerLabel);
                Grid.SetColumn(headerLabel, i);
                Grid.SetRow(headerLabel, 0);
            }

            // Add rows for existing weapons
            int rowIndex = 1;
            foreach (var item in _sheet.Inventory)
            {
                AddInventoryRow(mainInvGrid, rowIndex, _sheet.Inventory.IndexOf(item));
                item.PropertyChanged += async (s, e) => await SaveIfNeeded();
                rowIndex++;
                mainInvGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            }
        }

        // Add Inventory button
        var addInvButton = new Button
        {
            Text = "Add Item",
            Margin = new Thickness(0, 16, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        addInvButton.Clicked += OnAddItemClicked;

        //Ammunition grid
        var mainAmmoGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = new GridLength(180) }, // Name
                new ColumnDefinition { Width = new GridLength(70) }, // Qty
                new ColumnDefinition { Width = new GridLength(100) },  // Weight
                new ColumnDefinition { Width = new GridLength(80) }  // Delete
                ),
            RowDefinitions = new RowDefinitionCollection(),
            RowSpacing = 8,
            ColumnSpacing = 8,
            Padding = 10
        };

        // Header row (row 0)
        if (_sheet.Ammunition.Count > 0)
        {
            string[] headers = { "Name", "Qty", "Weight"};

            mainAmmoGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            for (int i = 0; i < headers.Length; i++)
            {
                var headerLabel = new Label
                {
                    Text = headers[i],
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                mainAmmoGrid.Children.Add(headerLabel);
                Grid.SetColumn(headerLabel, i);
                Grid.SetRow(headerLabel, 0);
            }

            // Add rows for existing Ammunition
            int rowIndex = 1;
            foreach (var ammo in _sheet.Ammunition)
            {
                AddAmmunitionRow(mainAmmoGrid, rowIndex, _sheet.Ammunition.IndexOf(ammo));
                ammo.PropertyChanged += async (s, e) => await SaveIfNeeded();
                rowIndex++;
                mainAmmoGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            }
        }

        // Add Ammunition button
        var addAmmoButton = new Button
        {
            Text = "Add Ammunition",
            Margin = new Thickness(0, 16, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        addAmmoButton.Clicked += OnAddAmmunitionClicked;


        //Build Frames
        Frame Inv = new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "Items", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    mainInvGrid,
                    addInvButton
                }
            }
        };

        outerGrid.Children.Add(Inv);
        Grid.SetColumn(Inv, 0);
        Grid.SetRow(Inv, 0);

        Frame Ammo = new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "Ammunition", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    mainAmmoGrid,
                    addAmmoButton
                }
            }
        };

        outerGrid.Children.Add(Ammo);
        Grid.SetColumn(Ammo, 1);
        Grid.SetRow(Ammo, 0);

        Frame outerFrame = new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label { Text = "Inventory", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                    summaryGrid,
                    outerGrid,
                }
            }
        };

        return outerFrame;
    }

    // Helper to add one inventory item row
    private void AddInventoryRow(Grid grid, int rowIndex, int itemIndex)
    {
        // Item
        var nameEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        nameEntry.SetBinding(Entry.TextProperty, $"Inventory[{itemIndex}].Name");
        grid.Children.Add(nameEntry);
        Grid.SetColumn(nameEntry, 0);
        Grid.SetRow(nameEntry, rowIndex);

        // qty
        var qtyEntry = new Entry { Keyboard = Keyboard.Numeric };
        qtyEntry.SetBinding(Entry.TextProperty, $"Inventory[{itemIndex}].Qty");
        grid.Children.Add(qtyEntry);
        Grid.SetColumn(qtyEntry, 1);
        Grid.SetRow(qtyEntry, rowIndex);

        // Weight
        var weightEntry = new Entry { Keyboard = Keyboard.Numeric };
        weightEntry.SetBinding(Entry.TextProperty, $"Inventory[{itemIndex}].Weight");
        grid.Children.Add(weightEntry);
        Grid.SetColumn(weightEntry, 2);
        Grid.SetRow(weightEntry, rowIndex);

        // Remove
        var deleteButton = new Button
        {
            Text = "Delete",
            Margin = new Thickness(0, 0, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        deleteButton.CommandParameter = itemIndex;
        deleteButton.Clicked += OnRemoveItemClicked;
        grid.Children.Add(deleteButton);
        Grid.SetColumn(deleteButton, 3);
        Grid.SetRow(deleteButton, rowIndex);
    }

    private void OnAddItemClicked(object sender, EventArgs e)
    {
        _sheet.Inventory.Add(new Item());
        SwitchTab("Inventory");
    }

    private void OnRemoveItemClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int index)
        {
            _sheet.Inventory.Remove(_sheet.Inventory[index]);
            SaveIfNeeded();
            SwitchTab("Inventory");
        }
    }

    // Helper to add one inventory item row
    private void AddAmmunitionRow(Grid grid, int rowIndex, int itemIndex)
    {
        // Item
        var nameEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        nameEntry.SetBinding(Entry.TextProperty, $"Ammunition[{itemIndex}].Name");
        grid.Children.Add(nameEntry);
        Grid.SetColumn(nameEntry, 0);
        Grid.SetRow(nameEntry, rowIndex);

        // qty
        var qtyEntry = new Entry { Keyboard = Keyboard.Numeric };
        qtyEntry.SetBinding(Entry.TextProperty, $"Ammunition[{itemIndex}].Qty");
        grid.Children.Add(qtyEntry);
        Grid.SetColumn(qtyEntry, 1);
        Grid.SetRow(qtyEntry, rowIndex);

        // Weight
        var weightEntry = new Entry { Keyboard = Keyboard.Numeric };
        weightEntry.SetBinding(Entry.TextProperty, $"Ammunition[{itemIndex}].Weight");
        grid.Children.Add(weightEntry);
        Grid.SetColumn(weightEntry, 2);
        Grid.SetRow(weightEntry, rowIndex);

        // Remove
        var deleteButton = new Button
        {
            Text = "Delete",
            Margin = new Thickness(0, 0, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        deleteButton.CommandParameter = itemIndex;
        deleteButton.Clicked += OnRemoveAmmunitionClicked;
        grid.Children.Add(deleteButton);
        Grid.SetColumn(deleteButton, 3);
        Grid.SetRow(deleteButton, rowIndex);
    }

    private void OnAddAmmunitionClicked(object sender, EventArgs e)
    {
        _sheet.Ammunition.Add(new Item());
        SwitchTab("Inventory");
    }

    private void OnRemoveAmmunitionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int index)
        {
            _sheet.Ammunition.Remove(_sheet.Ammunition[index]);
            SaveIfNeeded();
            SwitchTab("Inventory");
        }
    }

    private double calculateCCW()
    {
        return _sheet.Ammunition.Sum(i => i.Qty * i.Weight) +
                             _sheet.Inventory.Sum(i => i.Qty * i.Weight);
    }

    //Perks
    private View BuildPerksTab()
    {
        var mainGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection(
                new ColumnDefinition { Width = new GridLength(180) }, // Name
                new ColumnDefinition { Width = new GridLength(70) }, // Rank
                new ColumnDefinition { Width = GridLength.Star },  // Effect
                new ColumnDefinition { Width = new GridLength(80)} //Delete
            ),
            RowDefinitions = new RowDefinitionCollection(
            ),
            RowSpacing = 8,
            ColumnSpacing = 8,
            Padding = 10
        };

        // Header row (row 0)
        if (_sheet.Perks.Count > 0)
        {
            string[] headers = { "Name", "Rank", "Effect" };

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });

            for (int i = 0; i < headers.Length; i++)
            {
                var headerLabel = new Label
                {
                    Text = headers[i],
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                mainGrid.Children.Add(headerLabel);
                Grid.SetColumn(headerLabel, i);
                Grid.SetRow(headerLabel, 0);
            }

            // Add rows for existing weapons
            int rowIndex = 1;
            foreach (var perk in _sheet.Perks)
            {
                AddPerkRow(mainGrid, rowIndex, _sheet.Perks.IndexOf(perk));
                perk.PropertyChanged += async (s, e) => await SaveIfNeeded();
                rowIndex++;
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            }
        }

        // Add button
        var addButton = new Button
        {
            Text = "Add Perk",
            Margin = new Thickness(0, 16, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        addButton.Clicked += OnAddPerkClicked;

        return new Frame
        {
            BorderColor = (Color)Application.Current.Resources["FalloutBorder"],
            Padding = 10,
            Content = new VerticalStackLayout
            {
                Children =
            {
                new Label { Text = "Perks", FontSize = 24, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
                mainGrid,
                addButton
            }
            }
        };
    }

    // Helper to add one weapon row
    private void AddPerkRow(Grid grid, int rowIndex, int perkIndex)
    {
        // Name
        var nameEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        nameEntry.SetBinding(Entry.TextProperty, $"Perks[{perkIndex}].Name");
        grid.Children.Add(nameEntry);
        Grid.SetColumn(nameEntry, 0);
        Grid.SetRow(nameEntry, rowIndex);

        // rank
        var rankEntry = new Entry { Keyboard = Keyboard.Numeric };
        rankEntry.SetBinding(Entry.TextProperty, $"Perks[{perkIndex}].Rank");
        grid.Children.Add(rankEntry);
        Grid.SetColumn(rankEntry, 1);
        Grid.SetRow(rankEntry, rowIndex);

        // effect
        var effectEntry = new Entry { HorizontalOptions = LayoutOptions.Fill };
        effectEntry.SetBinding(Entry.TextProperty, $"Perks[{perkIndex}].Effect");
        grid.Children.Add(effectEntry);
        Grid.SetColumn(effectEntry, 2);
        Grid.SetRow(effectEntry, rowIndex);

        // Remove
        var deleteButton = new Button
        {
            Text = "Delete",
            Margin = new Thickness(0, 0, 0, 0),
            Style = (Style)Application.Current.Resources["PipBoyButton"]
        };
        deleteButton.CommandParameter = perkIndex;
        deleteButton.Clicked += OnRemovePerkClicked;
        grid.Children.Add(deleteButton);
        Grid.SetColumn(deleteButton, 3);
        Grid.SetRow(deleteButton, rowIndex);
    }

    private void OnAddPerkClicked(object sender, EventArgs e)
    {
        _sheet.Perks.Add(new Perk());
        SwitchTab("Perks");
    }

    private void OnRemovePerkClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int index)
        {
            _sheet.Perks.Remove(_sheet.Perks[index]);
            SaveIfNeeded();
            SwitchTab("Perks");
        }
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
}

public class SimpleValue
{
    public double CCW { get; set; }
}