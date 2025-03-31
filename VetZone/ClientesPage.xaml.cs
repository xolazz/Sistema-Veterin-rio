using Microsoft.Maui.Controls;

namespace VetZone;

public partial class ClientesPage : ContentPage
{   
        public ClientesPage()
        {
            InitializeComponent();
        }      

    private async void OnClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroClientePage());
    }

    private async void OnAdicionarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroClientePage());
    }

    private async void OnEditarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroClientePage());
    }

    private void OnExcluirClienteClicked(object sender, EventArgs e)
    {
        // Código para excluir cliente futuramente
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
    }

}
