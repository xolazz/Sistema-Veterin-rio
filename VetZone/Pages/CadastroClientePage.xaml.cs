using VetZone.Models;
using VetZone.Services;

namespace VetZone;

public partial class CadastroClientePage : ContentPage
{
    private ClienteService _clienteService;
    private Cliente _clienteAtual; // Usaremos para guardar o cliente sendo editado

    public CadastroClientePage()
    {
        InitializeComponent();
        _clienteService = new ClienteService();
    }

    // Construtor para EDI��O:
    // Recebe um objeto Cliente para preencher os campos.
    public CadastroClientePage(Cliente cliente) : this() // Chama o construtor padr�o primeiro
    {
        _clienteAtual = cliente;
        CarregarDadosCliente();
        Title = "Editar Cliente"; // Muda o t�tulo da p�gina para indicar edi��o
    }

    private void CarregarDadosCliente()
    {
        if (_clienteAtual != null)
        {
            NomeEntry.Text = _clienteAtual.Nome;
            CpfEntry.Text = _clienteAtual.CPF;
            EmailEntry.Text = _clienteAtual.Email;
            DataNascimentoPicker.Date = _clienteAtual.DataNascimento; // Preenche o DatePicker
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        // Valida��o b�sica dos campos
        if (string.IsNullOrWhiteSpace(NomeEntry.Text))
        {
            await DisplayAlert("Erro", "O nome do cliente � obrigat�rio.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(CpfEntry.Text) || CpfEntry.Text.Length != 11)
        {
            await DisplayAlert("Erro", "O CPF deve ter 11 d�gitos.", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(EmailEntry.Text) || !IsValidEmail(EmailEntry.Text))
        {
            await DisplayAlert("Erro", "O e-mail � obrigat�rio e deve ser v�lido.", "OK");
            return;
        }

        // Se _clienteAtual for nulo, � um novo cadastro
        if (_clienteAtual == null)
        {
            _clienteAtual = new Cliente();
        }

        // Popula o objeto Cliente com os dados dos campos
        _clienteAtual.Nome = NomeEntry.Text;
        _clienteAtual.CPF = CpfEntry.Text;
        _clienteAtual.Email = EmailEntry.Text;
        _clienteAtual.DataNascimento = DataNascimentoPicker.Date;

        // Salva (insere ou atualiza) o cliente no banco de dados
        await _clienteService.SaveClienteAsync(_clienteAtual);

        // Volta para a p�gina anterior (ClientesPage)
        await Navigation.PopAsync();
    }

    private async void OnDescartarClicked(object sender, EventArgs e)
    {
        // Simplesmente volta para a p�gina anterior sem salvar
        await Navigation.PopAsync();
    }

    // M�todo auxiliar para valida��o de e-mail simples
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