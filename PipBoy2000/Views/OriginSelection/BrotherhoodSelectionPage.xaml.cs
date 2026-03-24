using PipBoy2000.Models;

namespace PipBoy2000.Views;

public partial class BrotherhoodSelectionPage : ContentPage
{
    private string _origin;
    public BrotherhoodSelectionPage(string origin)
    {
        InitializeComponent();
        _origin = origin;
    }

    private async void OnOriginClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string suborigin)
        {
            var newSheet = new CharacterSheet
            {
                SubOrigin = suborigin,
                Origin = _origin,
                CharacterName = ""
            };

            await Navigation.PushAsync(new CharacterSheetPage(newSheet));
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