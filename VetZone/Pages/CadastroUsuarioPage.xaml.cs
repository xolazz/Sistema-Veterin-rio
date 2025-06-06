using VetZone.Models;
using VetZone.Services;
using System.Diagnostics;

namespace VetZone;

public partial class CadastroUsuarioPage : ContentPage
{
    private UsuarioService _usuarioService;
    private Usuario _usuarioEmEdicao;

    public CadastroUsuarioPage()
    {
        InitializeComponent();
        _usuarioService = new UsuarioService();
        LimparCampos(); 
    }

    
    public CadastroUsuarioPage(Usuario usuario) : this()
    {
        _usuarioEmEdicao = usuario; 
        CarregarDadosUsuario();
        Title = $"Editar Usu�rio: {usuario.Nome}"; 
    }

    private void LimparCampos()
    {
        _usuarioEmEdicao = null; 
        NomeUsuarioEntry.Text = string.Empty;
        SenhaUsuarioEntry.Text = string.Empty;
        Title = "Novo Usu�rio";
    }

    private void CarregarDadosUsuario()
    {
        if (_usuarioEmEdicao != null)
        {
            NomeUsuarioEntry.Text = _usuarioEmEdicao.Nome;
            SenhaUsuarioEntry.Text = _usuarioEmEdicao.Senha;
        }
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeUsuarioEntry.Text))
        {
            await DisplayAlert("Erro", "O nome do usu�rio � obrigat�rio.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(SenhaUsuarioEntry.Text))
        {
            await DisplayAlert("Erro", "A senha � obrigat�ria.", "OK");
            return;
        }

        if (_usuarioEmEdicao == null)
        {
            _usuarioEmEdicao = new Usuario();
        }

        _usuarioEmEdicao.Nome = NomeUsuarioEntry.Text;
        _usuarioEmEdicao.Senha = SenhaUsuarioEntry.Text;

        await _usuarioService.SaveUsuarioAsync(_usuarioEmEdicao); 

        Debug.WriteLine($"Usu�rio salvo! ID: {_usuarioEmEdicao.Id}, Nome: {_usuarioEmEdicao.Nome}");
        await DisplayAlert("Sucesso", $"Usu�rio '{_usuarioEmEdicao.Nome}' salvo com sucesso! C�digo: {_usuarioEmEdicao.Id}", "OK");

        await Navigation.PopAsync(); // Volta para a p�gina anterior (UsuariosPage) ap�s salvar
    }

    private void OnLimparClicked(object sender, EventArgs e)
    {
        LimparCampos();
    }

    private void OpenFlyoutMenu(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}