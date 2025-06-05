using VetZone.Models;
using VetZone.Services;

namespace VetZone;

public partial class CadastroEspeciePage : ContentPage
{
    private EspecieService _especieService;
    private Especie _especieAtual;

    public CadastroEspeciePage()
    {
        InitializeComponent();
        _especieService = new EspecieService();
    }

    // Construtor para EDIÇÃO:
    public CadastroEspeciePage(Especie especie) : this()
    {
        _especieAtual = especie;
        CarregarDadosEspecie();
        Title = "Editar Espécie";
    }

    private void CarregarDadosEspecie()
    {
        if (_especieAtual != null)
        {
            NomeEspecieEntry.Text = _especieAtual.Nome;
            // Preenche o Picker com o tipo salvo, se houver
            TipoPicker.SelectedItem = _especieAtual.Tipo;
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeEspecieEntry.Text))
        {
            await DisplayAlert("Erro", "O nome da espécie é obrigatório.", "OK");
            return;
        }

        if (TipoPicker.SelectedItem == null)
        {
            await DisplayAlert("Erro", "Selecione o tipo da espécie.", "OK");
            return;
        }

        if (_especieAtual == null)
        {
            _especieAtual = new Especie();
        }

        _especieAtual.Nome = NomeEspecieEntry.Text;
        _especieAtual.Tipo = TipoPicker.SelectedItem.ToString(); // Pega o item selecionado do Picker

        await _especieService.SaveEspecieAsync(_especieAtual);
        await Navigation.PopAsync(); // Volta para a página anterior (EspeciesPage)
    }

    private async void OnDescartarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}