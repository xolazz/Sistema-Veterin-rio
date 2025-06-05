namespace VetZone
{
    public partial class AnimaisPage : ContentPage
    {
        public AnimaisPage()
        {
            InitializeComponent();
        }

        private async void OnAdicionarAnimalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroAnimalPage());
        }

        private void OpenFlyoutMenu(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
        }

    }
}
