namespace VetZone
{
    public partial class AnimaisPage : ContentPage
    {
        public AnimaisPage()
        {
            InitializeComponent();
        }

        private async void OnAnimalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroAnimalPage());
        }

        private async void OnAdicionarAnimalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroAnimalPage());
        }

        private async void OnEditarAnimalClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroAnimalPage());
        }

        private void OnExcluirAnimalClicked(object sender, EventArgs e)
        {
            // Código para excluir cliente futuramente
        }

        private void OpenFlyoutMenu(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
        }

    }
}
