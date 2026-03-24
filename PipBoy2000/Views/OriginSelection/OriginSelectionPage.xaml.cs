using PipBoy2000.Models;

namespace PipBoy2000.Views;

public partial class OriginSelectionPage : ContentPage
{
    public OriginSelectionPage()
    {
        InitializeComponent();
    }

    private async void OnOriginClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string origin)
        {
            switch(origin){
                case "Brotherhood Initiate":
                    await Navigation.PushAsync(new BrotherhoodSelectionPage(origin));
                    break;
                case "Super Mutant":
                    await Navigation.PushAsync(new SuperMutantSelectionPage(origin));
                    break;
                case "Ghoul":
                    await Navigation.PushAsync(new WastelanderSelectionPage(origin));
                    break;
                case "Mister Handy":
                    await Navigation.PushAsync(new MrHandySelectionPage(origin));
                    break;
                case "Survivor":
                    await Navigation.PushAsync(new WastelanderSelectionPage(origin));
                    break;
                case "Vault Dweller":
                    await Navigation.PushAsync(new VaultSelectionPage(origin));
                    break;
            }

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