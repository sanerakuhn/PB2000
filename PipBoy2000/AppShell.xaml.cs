using PipBoy2000.Views;

namespace PipBoy2000
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        protected override bool OnBackButtonPressed()
        {
            if (Shell.Current.CurrentPage is not MainPage)
            {
                Shell.Current.GoToAsync("///MainPage");
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
