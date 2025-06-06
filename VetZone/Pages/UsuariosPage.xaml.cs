using VetZone.Models;
using VetZone.Services;
using System.Collections.ObjectModel;
using System.Linq;

namespace VetZone;

public partial class UsuariosPage : ContentPage
{
    private UsuarioService _usuarioService;
    private ObservableCollection<Usuario> _usuarios;
    private ObservableCollection<Usuario> _usuariosFiltrados;

    public UsuariosPage()
    {
        InitializeComponent();
        _usuarioService = new UsuarioService();
        _usuarios = new ObservableCollection<Usuario>();
        _usuariosFiltrados = new ObservableCollection<Usuario>();
        UsuariosCollectionView.ItemsSource = _usuariosFiltrados;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadUsuariosAsync(); // Recarrega a pagina
    }

    private async Task LoadUsuariosAsync()
    {
        var todosUsuarios = await _usuarioService.GetUsuariosAsync();
        _usuarios.Clear();
        foreach (var usuario in todosUsuarios)
        {
            _usuarios.Add(usuario);
        }
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        _usuariosFiltrados.Clear();
        string termoPesquisa = PesquisaUsuarioEntry.Text?.ToLower();

        if (string.IsNullOrWhiteSpace(termoPesquisa))
        {
            foreach (var usuario in _usuarios)
            {
                _usuariosFiltrados.Add(usuario);
            }
        }
        else
        {
            foreach (var usuario in _usuarios.Where(u =>
                u.Nome.ToLower().Contains(termoPesquisa) ||
                u.Id.ToString().Contains(termoPesquisa)))
            {
                _usuariosFiltrados.Add(usuario);
            }
        }
    }

    private async void OnAdicionarUsuarioClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CadastroUsuarioPage());
    }

    private async void OnPesquisarListaClicked(object sender, EventArgs e)
    {
        ApplyFilter();
    }

    private async void OnLimparPesquisaClicked(object sender, EventArgs e)
    {
        PesquisaUsuarioEntry.Text = string.Empty;
        ApplyFilter();
    }

    private async void OnEditarUsuarioClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Usuario usuario)
        {
            await Navigation.PushAsync(new CadastroUsuarioPage(usuario));
        }
    }

    private async void OnExcluirUsuarioClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Usuario usuario)
        {
            bool confirm = await DisplayAlert("Confirmação", $"Tem certeza que deseja excluir o usuário {usuario.Nome} (ID: {usuario.Id})?", "Sim", "Não");
            if (confirm)
            {
                await _usuarioService.DeleteUsuarioAsync(usuario);
                await LoadUsuariosAsync();
            }
        }
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}