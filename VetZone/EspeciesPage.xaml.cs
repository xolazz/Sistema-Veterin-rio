namespace VetZone
{
    public partial class EspeciesPage : ContentPage
    {
        public EspeciesPage()
        {
            InitializeComponent();
        }

        private async void OnEspecieClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroEspeciePage());
        }

        private async void OnAdicionarEspecieClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroEspeciePage());
        }

        private async void OnEditarEspecieClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroEspeciePage());
        }

        private void OnExcluirEspecieClicked(object sender, EventArgs e)
        {
            // Código para excluir cliente futuramente
        }

        private void OpenFlyoutMenu(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
        }

    }
}
