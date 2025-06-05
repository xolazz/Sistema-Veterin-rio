// Pages/ClientesPage.xaml.cs
using VetZone.Models;
using VetZone.Services;
using System.Collections.ObjectModel; // Importar para usar ObservableCollection

namespace VetZone;

public partial class ClientesPage : ContentPage
{
    private ClienteService _clienteService;
    private ObservableCollection<Cliente> _clientes; // Usamos ObservableCollection para atualização automática da UI

    public ClientesPage()
    {
        InitializeComponent();
        _clienteService = new ClienteService();
        _clientes = new ObservableCollection<Cliente>();
        ClientesCollectionView.ItemsSource = _clientes; // Associa a coleção ao CollectionView
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadClientesAsync(); // Carrega os clientes toda vez que a página é exibida
    }

    private async Task LoadClientesAsync()
    {
        var clientes = await _clienteService.GetClientesAsync();
        _clientes.Clear(); // Limpa a coleção existente
        foreach (var cliente in clientes)
        {
            _clientes.Add(cliente); // Adiciona os clientes do banco na ObservableCollection
        }
    }

    private async void OnAdicionarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroClientePage());
    }

    private async void OnEditarClienteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Cliente cliente)
        {
            // Navega para a página de cadastro/edição, passando o cliente para edição
            await Navigation.PushAsync(new CadastroClientePage(cliente));
        }
    }

    private async void OnExcluirClienteClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Cliente cliente)
        {
            bool confirm = await DisplayAlert("Confirmação", $"Tem certeza que deseja excluir o cliente {cliente.Nome}?", "Sim", "Não");
            if (confirm)
            {
                await _clienteService.DeleteClienteAsync(cliente);
                await LoadClientesAsync(); // Recarrega a lista após a exclusão
            }
        }
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; // Abre o menu lateral
    }
}