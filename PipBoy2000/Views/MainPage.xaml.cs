

namespace PipBoy2000.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCreateNewClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OriginSelectionPage());
    }

    private async void OnLoadClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CharacterSelectionPage());
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