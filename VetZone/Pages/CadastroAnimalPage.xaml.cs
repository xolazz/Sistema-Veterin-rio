using VetZone.Models;
using VetZone.Services;
using Microsoft.Maui.Media;
using System.Linq; 

namespace VetZone;

public partial class CadastroAnimalPage : ContentPage
{
    private AnimalService _animalService;
    private EspecieService _especieService;
    private ClienteService _clienteService; 
    private Animal _animalAtual;
    private string _caminhoFotoTemporario;

    public CadastroAnimalPage()
    {
        InitializeComponent();
        _animalService = new AnimalService();
        _especieService = new EspecieService();
        _clienteService = new ClienteService();
        LoadPickers(); 
    }

    public CadastroAnimalPage(Animal animal) : this()
    {
        _animalAtual = animal;
        Title = "Editar Animal";
        CarregarDadosAnimal(); 
    }

    private async void LoadPickers()
    {
        var especies = await _especieService.GetEspeciesAsync();
        EspeciePicker.ItemsSource = especies;

        var clientes = await _clienteService.GetClientesAsync();
        ClientePicker.ItemsSource = clientes;

        // Se estiver editando, pré-selecione os valores
        if (_animalAtual != null)
        {
            if (_animalAtual.Especie != null)
            {
                EspeciePicker.SelectedItem = especies.FirstOrDefault(e => e.Id == _animalAtual.Especie.Id);
            }
            if (_animalAtual.Cliente != null)
            {
                ClientePicker.SelectedItem = clientes.FirstOrDefault(c => c.Id == _animalAtual.Cliente.Id);
            }
        }
    }

    private void CarregarDadosAnimal()
    {
        if (_animalAtual != null)
        {
            NomeAnimalEntry.Text = _animalAtual.Nome;
            IdadeAproximadaEntry.Text = _animalAtual.IdadeAproximada.ToString();
            SituacaoClinicaEditor.Text = _animalAtual.SituacaoClinica;

            if (!string.IsNullOrEmpty(_animalAtual.FotoPath))
            {
                AnimalImage.Source = _animalAtual.FotoPath;
                _caminhoFotoTemporario = _animalAtual.FotoPath;
            }
        }
    }

    private async void OnSelecionarFotoClicked(object sender, EventArgs e)
    {
        if (MediaPicker.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();

            if (photo != null)
            {
                _caminhoFotoTemporario = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(_caminhoFotoTemporario))
                {
                    await stream.CopyToAsync(newStream);
                }

                AnimalImage.Source = _caminhoFotoTemporario;
            }
        }
        else
        {
            await DisplayAlert("Erro", "Captura de foto não suportada neste dispositivo.", "OK");
        }
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NomeAnimalEntry.Text))
        {
            await DisplayAlert("Erro", "O nome do animal é obrigatório.", "OK");
            return;
        }
        if (EspeciePicker.SelectedItem == null)
        {
            await DisplayAlert("Erro", "Selecione a espécie do animal.", "OK");
            return;
        }
        // Validação para Cliente
        if (ClientePicker.SelectedItem == null)
        {
            await DisplayAlert("Erro", "Selecione o cliente responsável pelo animal.", "OK");
            return;
        }

        if (_animalAtual == null)
        {
            _animalAtual = new Animal();
        }

        _animalAtual.Nome = NomeAnimalEntry.Text;
        _animalAtual.IdadeAproximada = int.TryParse(IdadeAproximadaEntry.Text, out int idade) ? idade : 0;
        _animalAtual.SituacaoClinica = SituacaoClinicaEditor.Text;
        _animalAtual.FotoPath = _caminhoFotoTemporario;

        // Atribuição de relacionamentos
        _animalAtual.Especie = EspeciePicker.SelectedItem as Especie;
        _animalAtual.EspecieId = _animalAtual.Especie.Id;

        // Atribuição do relacionamento com Cliente
        _animalAtual.Cliente = ClientePicker.SelectedItem as Cliente;
        _animalAtual.ClienteId = _animalAtual.Cliente.Id; 

        await _animalService.SaveAnimalAsync(_animalAtual);
        await Navigation.PopAsync();
    }

    private async void OnDescartarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}