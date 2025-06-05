using VetZone.Models;
using VetZone.Services;

namespace VetZone;

public partial class CadastroClientePage : ContentPage
{
    private ClienteService _clienteService;
    private Cliente _clienteAtual;

    public CadastroClientePage()
    {
        InitializeComponent();
        _clienteService = new ClienteService();
    }

    public CadastroClientePage(Cliente cliente) : this() 
    {
        _clienteAtual = cliente;
        CarregarDadosCliente();
        Title = "Editar Cliente"; 
    }

    private void CarregarDadosCliente()
    {
        if (_clienteAtual != null)
        {
            NomeEntry.Text = _clienteAtual.Nome;
            CpfEntry.Text = _clienteAtual.CPF;
            EmailEntry.Text = _clienteAtual.Email;
            DataNascimentoPicker.Date = _clienteAtual.DataNascimento; 
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeEntry.Text))
        {
            await DisplayAlert("Erro", "O nome do cliente é obrigatório.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(CpfEntry.Text) || CpfEntry.Text.Length != 11)
        {
            await DisplayAlert("Erro", "O CPF deve ter 11 dígitos.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || !IsValidEmail(EmailEntry.Text))
        {
            await DisplayAlert("Erro", "O e-mail é obrigatório e deve ser válido.", "OK");
            return;
        }

        if (_clienteAtual == null)
        {
            _clienteAtual = new Cliente();
        }

        _clienteAtual.Nome = NomeEntry.Text;
        _clienteAtual.CPF = CpfEntry.Text;
        _clienteAtual.Email = EmailEntry.Text;
        _clienteAtual.DataNascimento = DataNascimentoPicker.Date;

        await _clienteService.SaveClienteAsync(_clienteAtual);

        await Navigation.PopAsync();
    }

    private async void OnDescartarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}