namespace VetZone;

public partial class ClientesPage : ContentPage
{
    public ClientesPage()
    {
        InitializeComponent();
    }

    private async void OnAdicionarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroClientePage());
    }
    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
    }

}
