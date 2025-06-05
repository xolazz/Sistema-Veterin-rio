using VetZone.Models;
using VetZone.Services;
using Microsoft.Maui.Media; // Para MediaPicker

namespace VetZone;

public partial class CadastroAnimalPage : ContentPage
{
    private AnimalService _animalService;
    private EspecieService _especieService;
    private Animal _animalAtual;
    private string _caminhoFotoTemporario; // Para armazenar o caminho da foto selecionada

    public CadastroAnimalPage()
    {
        InitializeComponent();
        _animalService = new AnimalService();
        _especieService = new EspecieService();
        LoadEspeciesPicker(); // Carrega as espécies no Picker
    }

    // Construtor para EDIÇÃO:
    public CadastroAnimalPage(Animal animal) : this()
    {
        _animalAtual = animal;
        CarregarDadosAnimal();
        Title = "Editar Animal";
    }

    private async void LoadEspeciesPicker()
    {
        var especies = await _especieService.GetEspeciesAsync();
        EspeciePicker.ItemsSource = especies;
        // Se estiver editando e o animal tiver uma espécie, selecione-a
        if (_animalAtual != null && _animalAtual.Especie != null)
        {
            EspeciePicker.SelectedItem = especies.FirstOrDefault(e => e.Id == _animalAtual.Especie.Id);
        }
    }

    private void CarregarDadosAnimal()
    {
        if (_animalAtual != null)
        {
            NomeAnimalEntry.Text = _animalAtual.Nome;
            IdadeAproximadaEntry.Text = _animalAtual.IdadeAproximada.ToString();
            SituacaoClinicaEditor.Text = _animalAtual.SituacaoClinica;

            // Carrega a foto, se houver
            if (!string.IsNullOrEmpty(_animalAtual.FotoPath))
            {
                AnimalImage.Source = _animalAtual.FotoPath;
                _caminhoFotoTemporario = _animalAtual.FotoPath; // Mantém o caminho atual para caso não mude
            }
        }
    }

    private async void OnSelecionarFotoClicked(object sender, EventArgs e)
    {
        if (MediaPicker.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.PickPhotoAsync(); // Ou CapturePhotoAsync() para tirar foto

            if (photo != null)
            {
                // Salvar a foto em um local persistente no armazenamento do aplicativo
                _caminhoFotoTemporario = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(_caminhoFotoTemporario))
                {
                    await stream.CopyToAsync(newStream);
                }

                AnimalImage.Source = _caminhoFotoTemporario; // Exibe a foto selecionada
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

        if (_animalAtual == null)
        {
            _animalAtual = new Animal();
        }

        _animalAtual.Nome = NomeAnimalEntry.Text;
        _animalAtual.Especie = EspeciePicker.SelectedItem as Especie; // Pega o objeto Espécie selecionado
        _animalAtual.EspecieId = _animalAtual.Especie.Id; // Salva apenas o ID da espécie
        _animalAtual.IdadeAproximada = int.TryParse(IdadeAproximadaEntry.Text, out int idade) ? idade : 0; // Tenta converter para int
        _animalAtual.SituacaoClinica = SituacaoClinicaEditor.Text;
        _animalAtual.FotoPath = _caminhoFotoTemporario; // Salva o caminho da foto

        await _animalService.SaveAnimalAsync(_animalAtual);
        await Navigation.PopAsync();
    }

    private async void OnDescartarClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}