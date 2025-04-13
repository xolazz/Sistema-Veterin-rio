namespace VetZone
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OpenFlyoutMenu(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
        }

        private async void GoToAnimais(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AnimaisPage");
        }

        private async void GoToClientes(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ClientesPage");
        }

        private async void GoToEspecies(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//EspeciesPage");
        }

        private async void GoToUsuarios(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//CadastroUsuario" +
                "Page");
        }
    }
}